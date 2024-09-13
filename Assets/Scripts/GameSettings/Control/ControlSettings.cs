using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameSettings.Control
{
    public class ControlSettings : GameSettingsClass
    {
        
        public delegate void SensitivityChanged(float sensitivity);
        public event SensitivityChanged OnSensitivityChanged;
        
        public Slider sensitivitySlider;
        
        
        private void Start()
        {
            CheckPlayerPrefsKeys(new Dictionary<string, float>{{"ControlSettings:Sensitivity", 2}});
            SetSliderValuesFromPlayerPrefs();
        }

        private void SetSliderValuesFromPlayerPrefs()
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("ControlSettings:Sensitivity");
        }
        
        public void SaveControlSettings()
        {
            var sensitivity = sensitivitySlider.value;
            PlayerPrefs.SetFloat("ControlSettings:Sensitivity", sensitivity);
            OnSensitivityChanged?.Invoke(sensitivity);
        }
        
    }
}
