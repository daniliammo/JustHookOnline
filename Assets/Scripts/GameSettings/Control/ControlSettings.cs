using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameSettings.Control
{
    public class ControlSettings : GameSettingsClass
    {
        
        public delegate void SensivityChanged(float sensivity);
        public event SensivityChanged OnSensivityChanged;
        
        public Slider sensivitySlider;
        public TMP_Text sensivityValue;
        
        
        private void Start()
        {
            CheckOrWritePlayerPrefsKeysFloat(new Dictionary<string, float>{{"ControlSettings:Sensivity", 2}}, false);
            SetSliderValuesFromPlayerPrefs();
        }

        private void SetSliderValuesFromPlayerPrefs()
        {
            sensivitySlider.value = PlayerPrefs.GetFloat("ControlSettings:Sensivity") / 15;
        }

        public void OnSliderSensivityValueChanged()
        {
            sensivityValue.text = Math.Round(sensivitySlider.value * 15, 2).ToString(CultureInfo.InvariantCulture);
        }
        
        public void SaveControlSettings()
        {
            var sensivity = sensivitySlider.value * 15;
            PlayerPrefs.SetFloat("ControlSettings:Sensivity", sensivity);
            OnSensivityChanged?.Invoke(sensivity);
        }
        
    }
}
