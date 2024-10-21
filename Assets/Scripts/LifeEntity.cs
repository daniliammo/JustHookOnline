using Mirror;
using UnityEngine;


public class LifeEntity : NetworkBehaviour
{

    [Header("Живая сущность")]
    public EntityType entityType;

    [Header("Здоровье и регенерация")]
    public byte hp = 100;
    public byte maxHp = 255;
    public bool allowRegeneration = true;
    private bool _allowRegeneration = true;
    public float regenerationRepeatRate = 2.5f;
    public float allowRegenerationAfterDamageTime = 3.5f;
    public float regenerationAmount = 20;
    
    [Header("Возрождение")]
    public bool allowRevive = true;
    [Tooltip("Это поле нужно только тогда когда allowRevive = false")]
    public string gameObjectTagAfterDeath;
    public float reviveTime = 2.5f;

    [HideInInspector]
    public bool isDeath; // false по умолчанию.

    private NetworkStartPosition[] _spawnPoints;
    public FindObjectsInactive findSpawnPointsInactive;
    
    // Используйте эти ивенты для чистого и правильного кода.
    public delegate void HpChanged(string damagerName);
    public event HpChanged OnHpChanged;

    public delegate void OnEntityDied(string damagerName);
    public event OnEntityDied OnDeath;

    public delegate void OnEntityRevived();
    public event OnEntityRevived OnRevive;


    private void Start()
    {
        if(allowRegeneration)
            InvokeRepeating(nameof(Regeneration), 1, regenerationRepeatRate);

        if (entityType != EntityType.Player) return;
        GetComponent<Player.Player>().StartPlayer();
        _spawnPoints = FindObjectsByType<NetworkStartPosition>(findSpawnPointsInactive, FindObjectsSortMode.None);
    }

    public void Damage(byte damage, string damagerName)
    {
        if(isDeath) return;
        
        SetHp((byte)(hp - damage), damagerName);

        if (hp <= 0)
            Death(damagerName);
    }

    private void Death(string damagerName)
    {
        SetHp(0, damagerName);
        
        CancelInvoke(nameof(AllowRegeneration));
        Invoke(nameof(AllowRegeneration), 4);
        
        OnDeath?.Invoke(damagerName);
        isDeath = true;

        if (!allowRevive)
        {
            Destroy(this);
            gameObject.tag = gameObjectTagAfterDeath;
        }
        
        if(allowRevive)
            Invoke(nameof(Revive), reviveTime);
    }
    
    public void SetHp(byte newHp, string damagerName)
    {
        if(isDeath) return; // Если сущность мертва, то менять хп уже нельзя.
        
        if(newHp > maxHp)
            hp = maxHp;

        if (newHp < hp) // Если хп сносится, то регенерация, прерывается и начинается заново через allowRegenerationAfterDamageTime.
        {
            allowRegeneration = false;
            CancelInvoke(nameof(AllowRegeneration));
            Invoke(nameof(AllowRegeneration), allowRegenerationAfterDamageTime);
        }

        hp = newHp;
        OnHpChanged?.Invoke(damagerName);
    }
    
    private void Regeneration()
    {
        if (isDeath) return;
        if (!_allowRegeneration) return;

        SetHp((byte)(hp + regenerationAmount), string.Empty);
    }
    
    private void AllowRegeneration()
    {
        _allowRegeneration = true;
    }
    
    private void Revive()
    {
        transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
        SetHp(maxHp, string.Empty);

        isDeath = false;
        OnRevive?.Invoke();
    }
    
}