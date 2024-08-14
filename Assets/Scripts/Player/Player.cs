using GameSettings;
using Mirror;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
	public class Player : NetworkBehaviour
	{

		public TextMesh nickname;

		[SyncVar(hook = nameof(DisplayPlayerName))]
		public string playerDisplayName;

		[SyncVar] 
		public int hp;

		[SyncVar] // Увеличение максимального хп в дальнейших обновлениях
		public int maxHp = 100;

		[SyncVar] 
		public bool isDeath;

		// public delegate void PlayerDied(Transform killer);
		// public event PlayerDied OnDeath;
		//
		// public delegate void PlayerRevived();
		// public event PlayerRevived OnRevive;

		[SyncVar]
		public bool isAlreadyDeath;

		[SyncVar]
		private bool _allowRegeneration = true;
		
		private NetworkStartPosition[] _spawnPoints;

		private UIObjectsLinks _ui;
		private Animator _animator;
		private Hook _hook;
		private WeaponController _weaponController;

		private PlayerJoinMessages _playerJoinMessages;
		
		private NicknameSetter _nicknameSetter;

		private KillMessages _killMessages;
		

		private void Start()
		{
			GetComponents();

			if (isLocalPlayer)
			{
				CmdSendName(PlayerPrefs.GetString("Nickname"));
				_nicknameSetter.OnNicknameChanged += CmdSendName;
				_playerJoinMessages.CmdSendPlayerJoinMessage(PlayerPrefs.GetString("Nickname"));
			}

			InvokeRepeating(nameof(Regeneration), 1, 1);
			CheckPlayerPrefsKeys();
		}
		
		private static void CheckPlayerPrefsKeys()
		{
			if (!PlayerPrefs.HasKey("Deads"))
				PlayerPrefs.SetInt("Deads", 0);
		}

		private void GetComponents()
		{
			_playerJoinMessages = FindObjectOfType<PlayerJoinMessages>();
			_nicknameSetter = FindObjectOfType<NicknameSetter>();
			_ui = FindObjectOfType<UIObjectsLinks>();
			_spawnPoints = FindObjectsOfType<NetworkStartPosition>();
			_animator = GetComponent<Animator>();
			_hook = GetComponent<Hook>();
			_weaponController = GetComponent<WeaponController>();
			_killMessages = FindObjectOfType<KillMessages>();
		}
		
		[Command]
		private void CmdSendName(string playerNickname)
		{
			playerDisplayName = playerNickname;
		}
		
		public void DisplayPlayerName(string oldName, string newName)
		{
			nickname.text = newName;
		}

		[Command (requiresAuthority = false)]
		private void Regeneration()
		{
			if(isDeath) return;
			if(!_allowRegeneration) return;
			
			var x = hp + 25;
			hp = x > maxHp ? maxHp : x;
			
			UpdateHpText();
		}
		
		private void AllowRegeneration()
		{
			_allowRegeneration = true;
		}

		[Command(requiresAuthority = false)]
		public void CmdChangeHp(byte amount, Transform damager, string damagerName)
		{
			if (isDeath)
			{
				isAlreadyDeath = true;
				return;
			}

			if(hp <= 0) return;
			_allowRegeneration = false;
			
			RpcChangeHp(amount, damager, damagerName);
			
			CancelInvoke(nameof(AllowRegeneration)); // Останавливаем вызовы метода AllowRegeneration
			Invoke(nameof(AllowRegeneration), 4);
			
			UpdateHpText();
		}

		[ClientRpc]
		private void RpcChangeHp(byte amount, Transform damager, string damagerName)
		{
			hp -= amount;
			if (hp <= 0 && !isDeath)
				Death(damagerName, damager);
		}
		
		[TargetRpc]
		private void UpdateHpText()
		{
			_ui.sliderValueChanger.ChangeSliderValue((float)hp / maxHp, _ui.hpSlider);
			_ui.hpText.text = hp.ToString();
		}
		
		public void Death(string killerName, Transform killer = null)
		{
			// OnDeath?.Invoke(killer);
			isDeath = true;
			_animator.Play("Death");
			Invoke(nameof(Revive), 2);

			if (!isLocalPlayer) return;
			PlayerPrefs.SetInt("Deads", PlayerPrefs.GetInt("Deads") + 1);
			print("Deads:" + PlayerPrefs.GetInt("Deads"));
			
			_killMessages.CmdSendKillMessage(killerName, playerDisplayName);
		}
		
		private void Revive()
		{
			transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
			
			_animator.Play("idle");
			
			hp = maxHp;

			_weaponController.StartWeapon();
			_hook.Stop();
			
			CmdInvokeRpcMethod(nameof(UpdateHpText), 0);
			
			isDeath = false;
			isAlreadyDeath = false;
			// OnRevive?.Invoke();
		}
		
		[Command (requiresAuthority = false)]
		private void CmdInvokeRpcMethod(string methodName, float time)
		{
			Invoke(methodName, time);
		}
		
	}
}
