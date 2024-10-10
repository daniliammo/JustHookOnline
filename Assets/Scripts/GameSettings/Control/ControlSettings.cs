using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameSettings.Control
{
    public class ControlSettings : GameSettingsClass, IGameSettings
    {
        
        public delegate void SensitivityChanged(float sensitivity);
        public event SensitivityChanged OnSensitivityChanged;
        
        public Slider sensitivitySlider;


        public void Awake()
        {
            CheckPlayerPrefsKeys(new Dictionary<string, float>{{"ControlSettings:Sensitivity", 2}});
            SetUIValuesFromPlayerPrefs();
        }

        public void SetUIValuesFromPlayerPrefs()
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("ControlSettings:Sensitivity");
        }
        
        public void SaveSettings()
        {
            var sensitivity = sensitivitySlider.value;
            PlayerPrefs.SetFloat("ControlSettings:Sensitivity", sensitivity);
            OnSensitivityChanged?.Invoke(sensitivity);
        }
        
    }
}
