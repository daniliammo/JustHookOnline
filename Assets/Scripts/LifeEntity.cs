using Mirror;
using UnityEngine;


public class LifeEntity : NetworkBehaviour
{

    [Header("Живая сущность")]
    public EntityType entityType;

    [Header("Здоровье и регенерация")]
    public int hp = 100;
    public int maxHp = 255;
    public bool allowRegeneration = true;
    private bool _allowRegeneration = true;
    // Регенерация
    public float regenerationRepeatRate = 2.5f;
    public float allowRegenerationAfterDamageTime = 3.5f;
    public int regenerationAmount = 20;
    
    [Header("Возрождение")]
    public bool allowRevive = true;
    
    [Tooltip("Это поле нужно только тогда когда allowRevive = false")]
    public string gameObjectTagAfterDeath;
    public float reviveTime = 2.5f;
    
    public bool IsDeath { get; private set; } // false по умолчанию.

    private NetworkStartPosition[] _spawnPoints;
    public FindObjectsInactive findSpawnPointsInactive;
    
    // Используйте эти ивенты для чистого и правильного кода.
    public delegate void HpChanged(string damagerName);
    public event HpChanged OnHpChanged;

    public delegate void OnEntityDied(string damagerName);
    public event OnEntityDied OnDeath;

    public delegate void OnEntityRevived();
    public event OnEntityRevived OnRevive;


    [Server]
    private void Start()
    {
        if (allowRegeneration)
            InvokeRepeating(nameof(Regeneration), 1, regenerationRepeatRate);

        if (entityType != EntityType.Player) return;
        _spawnPoints = FindObjectsByType<NetworkStartPosition>(findSpawnPointsInactive, FindObjectsSortMode.None);
    }

    private void Death(string damagerName)
    {
        IsDeath = true;
        
        CancelInvoke(nameof(AllowRegeneration));
        CancelInvoke(nameof(Regeneration));
        Invoke(nameof(AllowRegeneration), reviveTime);
        
        OnDeath?.Invoke(damagerName);
        
        switch (allowRevive)
        {
            case false:
                gameObject.tag = gameObjectTagAfterDeath;
                // Destroy(this);
                break;
            case true:
                Invoke(nameof(Revive), reviveTime);
                break;
        }
    }

    [Command (requiresAuthority = false)]
    public void CmdSetHp(int newHp)
    {
        CmdSetHp(newHp, string.Empty);
    }
    
    [Command (requiresAuthority = false)]
    public void CmdSetHp(int newHp, string damagerName)
    {
        if (IsDeath)
        {
            Debug.LogWarning("Сущность мертва. Хп не будет изменено");
            return; // Если сущность мертва, то менять хп уже нельзя.
        }

        if (newHp == hp) return;
        
        if (newHp >= maxHp)
        {
            hp = maxHp;
            OnHpChanged?.Invoke(damagerName);
            return;
        }

        if (newHp < hp) // Если хп сносится, то регенерация, прерывается и начинается заново через allowRegenerationAfterDamageTime.
        {
            allowRegeneration = false;
            CancelInvoke(nameof(AllowRegeneration));
            Invoke(nameof(AllowRegeneration), allowRegenerationAfterDamageTime);
        }

        if (hp <= 0)
            Death(damagerName);
        
        hp = newHp;
        OnHpChanged?.Invoke(damagerName);
    }
    
    private void Regeneration()
    {
        if (IsDeath) return;
        if (!_allowRegeneration) return;

        CmdSetHp(hp + regenerationAmount);
    }
    
    private void AllowRegeneration()
    {
        _allowRegeneration = true;
    }
    
    private void Revive()
    {
        IsDeath = false;
        transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
        CmdSetHp(maxHp);
        OnRevive?.Invoke();
    }
    
}