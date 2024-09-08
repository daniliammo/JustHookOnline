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
		
		public Player.Player localPlayer;
		public Player.InteractController localInteractController;
		

		#region Interact

		public void OnInteractButtonClicked()
		{
			localInteractController.Interact();
		}

		#region Password
		public void Add0ToPassword()
		{
			localInteractController.password.Add(0);
			localInteractController.ProcessPassword();
		}
		
		public void Add1ToPassword()
		{
			localInteractController.password.Add(1);
			localInteractController.ProcessPassword();
		}
		
		public void Add2ToPassword()
		{
			localInteractController.password.Add(2);
			localInteractController.ProcessPassword();
		}
		
		public void Add3ToPassword()
		{
			localInteractController.password.Add(3);
			localInteractController.ProcessPassword();
		}
		
		public void Add4ToPassword()
		{
			localInteractController.password.Add(4);
			localInteractController.ProcessPassword();
		}
		
		public void Add5ToPassword()
		{
			localInteractController.password.Add(5);
			localInteractController.ProcessPassword();
		}
		
		public void Add6ToPassword()
		{
			localInteractController.password.Add(6);
			localInteractController.ProcessPassword();
		}
		
		public void Add7ToPassword()
		{
			localInteractController.password.Add(7);
			localInteractController.ProcessPassword();
		}
		
		public void Add8ToPassword()
		{
			localInteractController.password.Add(8);
			localInteractController.ProcessPassword();
		}
		
		public void Add9ToPassword()
		{
			localInteractController.password.Add(9);
			localInteractController.ProcessPassword();
		}

		public void RemoveLastSymbolInThePassword()
		{
			if(localInteractController.password.Count > 0)
				localInteractController.password.RemoveAt(localInteractController.password.Count - 1);
		}
		#endregion
		
		#endregion
		
	}
}
