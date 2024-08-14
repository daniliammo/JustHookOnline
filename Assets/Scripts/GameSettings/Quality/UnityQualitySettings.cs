using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings.Quality
{
    public class UnityQualitySettings : GameSettingsClass
    {

        public Slider qualityLevelSlider;
        public Slider realtimeGICPUUsageSlider;
        public Slider shadowDistanceSlider;
        public Slider shadowNearPlaneOffsetSlider;
        public Slider shadowCascadesSlider;
        public Slider lodBiasSlider;
        public Slider maximumLODLevelSlider;
        public Toggle lodCrossfadeToggle;
        public Dropdown antiAliasingDropdown;
        public Dropdown anisotropicTexturesDropdown;
        public Dropdown shadowmaskModeDropdown;
        
        
        private void Awake()
        {
            CheckOrWritePlayerPrefsKeysInt(new Dictionary<string, int>
            {
                { "QualitySettings:QualityLevel", 0 },
                { "QualitySettings:AntiAliasing", 0 },
                { "QualitySettings:RealtimeGICPUUsage", 25 },
                { "QualitySettings:Shadows:ShadowDistance", 150 },
                { "QualitySettings:Shadows:ShadowNearPlaneOffset", 5 },
                { "QualitySettings:Shadows:ShadowCascades", 4 },
                { "QualitySettings:LOD:LODBias", 2 },
                { "QualitySettings:LOD:MaximumLODLevel", 0 }
            }, false);
            
            CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>
            {
                { "QualitySettings:LOD:LODCrossFade", false }
            }, false);

            CheckOrWritePlayerPrefsKeysString(new Dictionary<string, string>
            {
                { "QualitySettings:AnisotropicTextures", "Forced On" },
                { "QualitySettings:Shadows:ShadowmaskMode", "Distance Shadowmask" },
                { "QualitySettings:Shadows:Shadows", "Hard and Soft Shadows" },
                { "QualitySettings:Shadows:ShadowResolution", "Very High Resolution" },
                { "QualitySettings:Shadows:ShadowProjection", "Stable Fit" }
            }, false);
            
            SetSliderValuesFromPlayerPrefs();
            SetQualitySettingsFromPlayerPrefs();
        }

        private void SetSliderValuesFromPlayerPrefs()
        {
            qualityLevelSlider.value = PlayerPrefs.GetInt("QualitySettings:QualityLevel");
            
            realtimeGICPUUsageSlider.value = PlayerPrefs.GetInt("QualitySettings:RealtimeGICPUUsage");
        }
        
        private static void SetQualitySettingsFromPlayerPrefs()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettings:QualityLevel"));
            
            QualitySettings.antiAliasing = PlayerPrefs.GetInt("QualitySettings:AntiAliasing");
            
            QualitySettings.realtimeGICPUUsage = PlayerPrefs.GetInt("QualitySettings:RealtimeGICPUUsage");
        }

        public void SaveQualitySettings()
        {
            CheckOrWritePlayerPrefsKeysInt(new Dictionary<string, int>
            {
                { "QualitySettings:QualityLevel", (int)(qualityLevelSlider.value * 10)},
                { "QualitySettings:AntiAliasing", 0 },
                { "QualitySettings:RealtimeGICPUUsage", (int)(realtimeGICPUUsageSlider.value * 10) },
                { "QualitySettings:Shadows:ShadowDistance", (int)(shadowDistanceSlider.value * 10) },
                { "QualitySettings:Shadows:ShadowNearPlaneOffset", (int)(shadowNearPlaneOffsetSlider.value * 10) },
                { "QualitySettings:Shadows:ShadowCascades", (int)(shadowCascadesSlider.value * 40) },
                { "QualitySettings:LOD:LODBias", (int)(lodBiasSlider.value * 10)},
                { "QualitySettings:LOD:MaximumLODLevel", (int)(maximumLODLevelSlider.value * 10)}
            }, true);
            
            CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>
            {
                { "QualitySettings:LOD:LODCrossFade", lodCrossfadeToggle.isOn }
            }, true);
            
            CheckOrWritePlayerPrefsKeysString(new Dictionary<string, string>
            {
                { "QualitySettings:AnisotropicTextures", "Forced On" },
                { "QualitySettings:Shadows:ShadowmaskMode", "Distance Shadowmask" },
                { "QualitySettings:Shadows:Shadows", "Hard and Soft Shadows" },
                { "QualitySettings:Shadows:ShadowResolution", "Very High Resolution" },
                { "QualitySettings:Shadows:ShadowProjection", "Stable Fit" }
            }, true);
        }
        
    }
}
