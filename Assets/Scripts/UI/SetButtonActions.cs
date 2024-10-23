using Player;
using Player.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class SetButtonActions : MonoBehaviour
	{

		public FirstPersonLook firstPersonLook;
		private UIObjectsLinks _ui;
		private WeaponController _weaponController;
		private InteractController _interactController;
		private Jump _jump;
		private Hook _hookController;
		
		
		private void Start()
		{
			GetComponents();
			SetButtonsActions();
			Destroy(this);
		}

		private void SetButtonsActions()
		{
			// Fire button
			var entry = new EventTrigger.Entry
				{ eventID = EventTriggerType.PointerDown };
			entry.callback.AddListener(_ => { _weaponController.OnFireButtonDown(); });
			var shootButoonEventTrigger = _ui.shootButton.gameObject.AddComponent<EventTrigger>();
			shootButoonEventTrigger.triggers.Add(entry);
			
			
			var entry2 = new EventTrigger.Entry
				{ eventID = EventTriggerType.PointerUp };
			entry2.callback.AddListener(_ => { _weaponController.OnFireButtonUp(); });
			shootButoonEventTrigger.triggers.Add(entry2);

			// CmdReload button
			_ui.reloadButton.onClick.AddListener(_weaponController.CmdReload);
			
			
			// Hook
			// Start Hook button
			_ui.hookButton.onClick.AddListener(_hookController.JustHook);
			// Cancel Hook button
			_ui.cancelHookingButton.GetComponent<Button>().onClick.AddListener(_hookController.StopGrapple);

			// Jump
			_ui.jumpButton.onClick.AddListener(_jump.AddForce);
			
			// Spotlight
			_ui.spotlightButton.onClick.AddListener(firstPersonLook.CmdEnableFlashlight);
			
			_ui.simpleInteract.GetComponent<Button>().onClick.AddListener(_interactController.Interact);
		}
		
		private void GetComponents()
		{
			_ui = FindFirstObjectByType<UIObjectsLinks>();
			_weaponController = GetComponent<WeaponController>();
			_interactController = GetComponent<InteractController>();
			_jump = GetComponent<Jump>();
			_hookController = GetComponent<Hook>();
		}
        
	}
}
