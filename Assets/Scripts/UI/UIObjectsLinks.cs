using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;

namespace UI
{
	public class UIObjectsLinks : MonoBehaviour
	{
		public Slider hpSlider;
		public TMP_Text hpText;
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
	}
}
