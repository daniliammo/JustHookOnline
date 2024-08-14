using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
			Destroy(gameObject);
		}

		private void GetComponents()
		{
			_buttons = Resources.FindObjectsOfTypeAll<Button>();

			_inputFields = Resources.FindObjectsOfTypeAll<TMP_InputField>();

			_sliders = Resources.FindObjectsOfTypeAll<Slider>();

			_toggles = Resources.FindObjectsOfTypeAll<Toggle>();
			
			_buttonSounds = FindObjectOfType<ButtonSounds>();
		}
		
		private void SetUp()
		{
			foreach (var button in _buttons)
			{
				var eventTrigger = button.gameObject.AddComponent<EventTrigger>();
				var entry = new EventTrigger.Entry 
					{ eventID = EventTriggerType.PointerEnter };
				entry.callback.AddListener(_ => { _buttonSounds.PlayHoverSound1(); });
				eventTrigger.triggers.Add(entry);
			}
			
			foreach (var slider in _sliders)
			{
				var eventTrigger = slider.gameObject.AddComponent<EventTrigger>();
				var entry = new EventTrigger.Entry 
					{ eventID = EventTriggerType.PointerEnter };
				entry.callback.AddListener(_ => { _buttonSounds.PlayHoverSound1(); });
				eventTrigger.triggers.Add(entry);
			}
			
			foreach (var inputField in _inputFields)
			{
				var eventTrigger = inputField.gameObject.AddComponent<EventTrigger>();
				var entry = new EventTrigger.Entry 
					{ eventID = EventTriggerType.PointerEnter };
				entry.callback.AddListener(_ => { _buttonSounds.PlayHoverSound1(); });
				eventTrigger.triggers.Add(entry);
			}
			
			foreach (var toggle in _toggles)
			{
				var eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();
				var entry = new EventTrigger.Entry 
					{ eventID = EventTriggerType.PointerEnter };
				entry.callback.AddListener(_ => { _buttonSounds.PlayHoverSound1(); });
				eventTrigger.triggers.Add(entry);
			}
		}
		
	}
}
