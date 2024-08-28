using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

namespace GameSettings.Quality
{
    public class LightSettings : GameSettingsClass
    {

        public Toggle dynamicLightToggle;

        
        private void Start()
        {
            CheckPlayerPrefsKeys(new Dictionary<string, bool> { {"LightSettings:IsDynamicLightAllowed", true} });
            SetToggleValueFromPlayerPrefs();
        }
        
        private void SetToggleValueFromPlayerPrefs()
        {
            dynamicLightToggle.isOn = PlayerPrefsBoolean.GetBool("LightSettings:IsDynamicLightAllowed");
        }
        
        public void SaveLightSettings()
        {
            WritePlayerPrefsKeys(new Dictionary<string, bool> { {"LightSettings:IsDynamicLightAllowed", dynamicLightToggle.isOn} });
        }
        
    }
}
