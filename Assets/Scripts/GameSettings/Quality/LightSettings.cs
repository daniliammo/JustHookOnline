using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

namespace GameSettings.Quality
{
    public class LightSettings : GameSettingsClass
    {

        public Toggle flashLightToggle;
        public Toggle fireLightToggle;

        
        private void Start()
        {
            CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>
            {
                {"LightSettings:IsFlashLightAllowed", true},
                {"LightSettings:IsFireLightAllowed", true}
            }, false);
            SetToggleValueFromPlayerPrefs();
        }
        
        private void SetToggleValueFromPlayerPrefs()
        {
            flashLightToggle.isOn = PlayerPrefsBoolean.GetBool("LightSettings:IsFlashLightAllowed");
            fireLightToggle.isOn = PlayerPrefsBoolean.GetBool("LightSettings:IsFireLightAllowed");
        }
        
        public void SaveLightSettings()
        {
            CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>
            {
                {"LightSettings:IsFlashLightAllowed", flashLightToggle.isOn},
                {"LightSettings:IsFireLightAllowed", fireLightToggle.isOn}
            }, true);
        }
        
    }
}
