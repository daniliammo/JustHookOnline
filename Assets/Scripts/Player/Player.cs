using Cars;
using GameSettings;
using Mirror;
using UI;
using UnityEngine;

namespace Player
{
	public class Player : LifeEntity
	{

		[Header("Игрок")]
		public TextMesh nickname;

		[SyncVar(hook = nameof(DisplayPlayerName))]
		public string playerDisplayName;
		
		public delegate void PlayerGotIntoTheVehicle(Vehicle vehicle);
		public event PlayerGotIntoTheVehicle OnGotIntoTheVehicle;
		
		public delegate void PlayerExitOutOfVehicle();
		public event PlayerExitOutOfVehicle OnExitOutOfVehicle;

		private UIObjectsLinks _ui;
		private Animator _animator;
		private Hook _hook;
		private WeaponController _weaponController;

		private PlayerJoinMessages _playerJoinMessages;
		
		private NicknameSetter _nicknameSetter;

		private KillMessages _killMessages;


		public Player()
		{
			OnEntityStarted += StartPlayer;
		}

		private void StartPlayer()
		{
			GetComponents();

			OnDeath += Death;
			OnRevive += Revive;
			OnHpChanged += UpdateHpText;
			
			if (isLocalPlayer)
			{
				CmdSendName(PlayerPrefs.GetString("Nickname"));
				_nicknameSetter.OnNicknameChanged += CmdSendName;
				_playerJoinMessages.CmdSendPlayerJoinMessage(PlayerPrefs.GetString("Nickname"));
				FindFirstObjectByType<TimeController>().CmdSyncTime();
				FindFirstObjectByType<Match>().CmdSyncConfig();
				_ui.localPlayer = this;
			}
			
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
			_animator = GetComponent<Animator>();
			_hook = GetComponent<Hook>();
			_weaponController = GetComponent<WeaponController>();
			_killMessages = FindFirstObjectByType<KillMessages>();
		}

		private void UpdateHpText(string unused)
		{
			CmdInvokeRpcMethod(nameof(TargetRpcUpdateHpText), 0);
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
		
		[TargetRpc]
		private void TargetRpcUpdateHpText()
		{
			_ui.sliderValueChanger.ChangeSliderValue(hp, _ui.hpSlider);
		}
		
		private void Death(string killerName)
		{
			_animator.Play("Death");

			if (!isLocalPlayer) return;
			PlayerPrefs.SetInt("Deads", PlayerPrefs.GetInt("Deads") + 1);
			print("Deads:" + PlayerPrefs.GetInt("Deads"));
			
			_killMessages.CmdSendKillMessage(killerName, playerDisplayName);
		}
		
		private void Revive()
		{
			_animator.Play("idle");

			_weaponController.StartWeapon();
			_hook.Stop();
			
			CmdInvokeRpcMethod(nameof(TargetRpcUpdateHpText), 0);
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
