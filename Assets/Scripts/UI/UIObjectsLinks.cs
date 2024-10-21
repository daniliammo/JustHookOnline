using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;

namespace UI
{
	public class UIObjectsLinks : MonoBehaviour
	{
		public Slider hpSlider;
		public TMP_Text ammoText;
		public TMP_Text fullAmmoText;
		public GameObject reloadText;
		public GameObject playerKilledGameObject;
		public TMP_Text playerKilledText;

		public GameObject hookCrosshair;
		public RectTransform hookCrosshairRectTransform;
		
		// Для класса SetButtonActions
		public Button spotlightButton;
		public Button hookButton;
		public Button reloadButton;
		public Button jumpButton;
		public Button shootButton;
		public GameObject cancelHookingButton;
		
		public GameObject hitMarker;
		public GameObject killMarker;
		public SliderValueChanger sliderValueChanger;
		public Menu menu;

		public GameObject simpleInteract;
		public TMP_Text simpleInteractNameText;
		public GameObject passwordEntryGameObject;
		public TMP_Text passwordEntry;
		public TMP_Text passwordEntryNameText;
		
        [HideInInspector]
		public Player.Player localPlayer;
        [HideInInspector]
		public Player.InteractController localInteractController;
		

		#region Interact

		public void OnInteractButtonClicked()
		{
			localInteractController.Interact();
		}

		#region Password
		public void Add0ToPassword()
		{
			localInteractController.passwordString += '0';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add1ToPassword()
		{
			localInteractController.passwordString += '1';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add2ToPassword()
		{
			localInteractController.passwordString += '2';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add3ToPassword()
		{
			localInteractController.passwordString += '3';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add4ToPassword()
		{
			localInteractController.passwordString += '4';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add5ToPassword()
		{
			localInteractController.passwordString += '5';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add6ToPassword()
		{
			localInteractController.passwordString += '6';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add7ToPassword()
		{
			localInteractController.passwordString += '7';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add8ToPassword()
		{
			localInteractController.passwordString += '8';
			localInteractController.OnPasswordUpdated();
		}
		
		public void Add9ToPassword()
		{
			localInteractController.passwordString += '9';
			localInteractController.OnPasswordUpdated();
		}

		public void RemoveLastSymbolInThePassword()
		{
			if (localInteractController.passwordString.Length <= 0) return;
			
			localInteractController.passwordString.Remove(localInteractController.passwordString.Length - 1);
			localInteractController.OnPasswordUpdated();
		}
		#endregion
		
		#endregion
		
	}
}
