using System.Linq;
using Cars;
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

		public delegate void PlayerDied(Transform killer);
		public event PlayerDied OnDeath;
		
		public delegate void PlayerGotIntoTheVehicle(Vehicle vehicle);
		public event PlayerGotIntoTheVehicle OnGotIntoTheVehicle;
		
		public delegate void PlayerExitOutOfVehicle();
		public event PlayerExitOutOfVehicle OnExitOutOfVehicle;
		
		public delegate void PlayerRevived();
		public event PlayerRevived OnRevive;

		[SyncVar]
		private bool _allowRegeneration = true;

		public float regenerationRepeatRate;
		
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
				FindFirstObjectByType<TimeController>().CmdSyncTime();
				_ui.localPlayer = this;
			}

			InvokeRepeating(nameof(Regeneration), 1, regenerationRepeatRate); // Вызываем регенерацию каждую секунду
			CheckPlayerPrefsKeys();
		}
		
		private static void CheckPlayerPrefsKeys()
		{
			if (!PlayerPrefs.HasKey("Deads"))
				PlayerPrefs.SetInt("Deads", 0);
		}

		private void GetComponents()
		{
			_playerJoinMessages = FindFirstObjectByType<PlayerJoinMessages>();
			_nicknameSetter = FindFirstObjectByType<NicknameSetter>();
			_ui = FindFirstObjectByType<UIObjectsLinks>();
			_spawnPoints = FindObjectsByType<NetworkStartPosition>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			_animator = GetComponent<Animator>();
			_hook = GetComponent<Hook>();
			_weaponController = GetComponent<WeaponController>();
			_killMessages = FindFirstObjectByType<KillMessages>();
		}
		
		[Command (requiresAuthority = false)]
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

		[Command (requiresAuthority = false)]
		public void CmdChangeHp(byte amount, Transform damager, string damagerName)
		{
			if (isDeath) return;

			if(hp <= 0) return;
			_allowRegeneration = false;
			
			RpcChangeHp(amount, damager, damagerName);
			
			CancelInvoke(nameof(AllowRegeneration)); // Останавливаем вызовы метода AllowRegeneration
			Invoke(nameof(AllowRegeneration), 4); // Разрешаем регенерацию через 4 секунды
			
			UpdateHpText();
		}

		[ClientRpc] // Никогда не вызывайте этот метод без CmdChangeHp так как измененное здоровье не будет отображаться в UI.
		private void RpcChangeHp(byte amount, Transform damager, string damagerName)
		{
			hp -= amount;
			if (hp <= 0 && !isDeath)
				Death(damagerName, damager);
		}
		
		[TargetRpc]
		private void UpdateHpText()
		{
			_ui.sliderValueChanger.ChangeSliderValue(hp, _ui.hpSlider);
		}
		
		public void Death(string killerName, Transform killer = null)
		{
			OnDeath?.Invoke(killer);
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
			OnRevive?.Invoke();
		}

		#region Vehicle

		public void GotIntoVehicle(Vehicle vehicle)
		{
			OnGotIntoTheVehicle?.Invoke(vehicle);
		}
		
		public void ExitOutOfVehicle()
		{
			OnExitOutOfVehicle?.Invoke();
		}

		#endregion
		

		
		[Command (requiresAuthority = false)]
		private void CmdInvokeRpcMethod(string methodName, float time)
		{
			Invoke(methodName, time);
		}
		
	}
}
