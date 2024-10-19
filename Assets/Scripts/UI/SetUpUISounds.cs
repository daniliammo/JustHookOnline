using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SetUpUISounds : MonoBehaviour
	{

		private Button[] _buttons;
		private TMP_InputField[] _inputFields;
		private Slider[] _sliders;
		private Toggle[] _toggles;

		private ButtonSounds _buttonSounds;
		

		private void Start()
		{
			GetComponents();
			SetUp();
			Destroy(this);
		}

		private void GetComponents()
		{
			_buttons = Resources.FindObjectsOfTypeAll<Button>();

			_inputFields = Resources.FindObjectsOfTypeAll<TMP_InputField>();

			_sliders = Resources.FindObjectsOfTypeAll<Slider>();

			_toggles = Resources.FindObjectsOfTypeAll<Toggle>();

			_buttonSounds = FindFirstObjectByType<ButtonSounds>();
		}
		
		private void SetUp()
		{
			foreach (var button in _buttons)
				SetSounds(button.gameObject);
			
			foreach (var slider in _sliders)
				SetSounds(slider.gameObject);
			
			foreach (var inputField in _inputFields)
				SetSounds(inputField.gameObject);
			
			foreach (var toggle in _toggles)
				SetSounds(toggle.gameObject);
		}
		
		private void SetSounds(GameObject target)
		{
			target.AddComponent<UISounds>().buttonSounds = _buttonSounds;
		}
		
	}
}
