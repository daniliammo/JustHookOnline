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
		
		
		private void Start()
		{
			
			var ui = FindObjectOfType<UIObjectsLinks>();

			var weapon = GetComponent<WeaponController>();
			
			// Fire button
			var entry = new EventTrigger.Entry
			{ eventID = EventTriggerType.PointerDown };
			entry.callback.AddListener(_ => { weapon.OnFireButtonDown(); });
			var shootButoonEventTrigger = ui.shootButton.gameObject.AddComponent<EventTrigger>();
			shootButoonEventTrigger.triggers.Add(entry);
			
			
			var entry2 = new EventTrigger.Entry
			{ eventID = EventTriggerType.PointerUp };
			entry2.callback.AddListener(_ => { weapon.OnFireButtonUp(); });
			shootButoonEventTrigger.triggers.Add(entry2);

			// Reload button
			ui.reloadButton.onClick.AddListener(weapon.Reload);
			
			
			// Hook
			var hook = GetComponent<Hook>();
			// Start Hook button
			ui.hookButton.onClick.AddListener(hook.JustHook);
			// Cancel Hook button
			ui.cancelHookingButton.GetComponent<Button>().onClick.AddListener(hook.StopGrapple);

			// Jump
			var jump = GetComponent<Jump>();
			ui.jumpButton.onClick.AddListener(jump.AddForce);
			
			// Spotlight
			ui.spotlightButton.onClick.AddListener(firstPersonLook.CmdEnableFlashlight);
			
			Destroy(this);
			
		}
    
	}
}
