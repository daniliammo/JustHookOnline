using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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
        public Dropdown shadowsDropdown;
        public Dropdown shadowResolutionDropdown;
        public Dropdown shadowProjectionDropdown;
        
        
        private void Awake()
        {
            CheckPlayerPrefsKeys(new Dictionary<string, int>
            {
                { "QualitySettings:QualityLevel", 0 },
                { "QualitySettings:AntiAliasing", 0 },
                { "QualitySettings:RealtimeGICPUUsage", 50 },
                { "QualitySettings:Shadows:ShadowDistance", 150 },
                { "QualitySettings:Shadows:ShadowNearPlaneOffset", 5 },
                { "QualitySettings:Shadows:ShadowCascades", 4 },
                { "QualitySettings:LOD:LODBias", 2 },
                { "QualitySettings:LOD:MaximumLODLevel", 0 }
            });
            
            CheckPlayerPrefsKeys(new Dictionary<string, bool>
            {
                { "QualitySettings:LOD:LODCrossFade", false }
            });

            CheckPlayerPrefsKeys(new Dictionary<string, string>
            {
                { "QualitySettings:AnisotropicTextures", "Forced On" },
                { "QualitySettings:Shadows:ShadowmaskMode", "Distance Shadowmask" },
                { "QualitySettings:Shadows:Shadows", "Hard and Soft Shadows" },
                { "QualitySettings:Shadows:ShadowResolution", "Very High Resolution" },
                { "QualitySettings:Shadows:ShadowProjection", "Stable Fit" }
            });
            
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

            QualitySettings.shadowDistance = PlayerPrefs.GetInt("QualitySettings:Shadows:ShadowDistance");

            QualitySettings.shadowNearPlaneOffset = PlayerPrefs.GetFloat("QualitySettings:Shadows:ShadowNearPlaneOffset");
            
            QualitySettings.shadowCascades = PlayerPrefs.GetInt("QualitySettings:Shadows:ShadowCascades");

            QualitySettings.lodBias = PlayerPrefs.GetFloat("QualitySettings:LOD:LODBias");

            QualitySettings.maximumLODLevel = PlayerPrefs.GetInt("QualitySettings:LOD:LODBias");

            QualitySettings.enableLODCrossFade = PlayerPrefsBoolean.GetBool("QualitySettings:LOD:LODCrossFade");
        }
        
        public void SaveQualitySettings()
        {
            WritePlayerPrefsKeys(new Dictionary<string, string>
            {
                { "QualitySettings:AnisotropicTextures", "Forced On" },
                { "QualitySettings:Shadows:ShadowmaskMode", "Distance Shadowmask" },
                { "QualitySettings:Shadows:Shadows", "Hard and Soft Shadows" },
                { "QualitySettings:Shadows:ShadowResolution", "Very High Resolution" },
                { "QualitySettings:Shadows:ShadowProjection", "Stable Fit" }
            });
            
            WritePlayerPrefsKeys(new Dictionary<string, int>
            {
                { "QualitySettings:QualityLevel", (int)qualityLevelSlider.value},
                { "QualitySettings:AntiAliasing", antiAliasingDropdown.value },
                { "QualitySettings:RealtimeGICPUUsage", (int)realtimeGICPUUsageSlider.value },
                { "QualitySettings:Shadows:ShadowCascades", (int)(shadowCascadesSlider.value * 40) },
                { "QualitySettings:LOD:MaximumLODLevel", (int)(maximumLODLevelSlider.value * 10)}
            });
            
            WritePlayerPrefsKeys(new Dictionary<string, float>
            {
                { "QualitySettings:Shadows:ShadowDistance", shadowDistanceSlider.value * 10 },
                { "QualitySettings:Shadows:ShadowNearPlaneOffset", shadowNearPlaneOffsetSlider.value * 10 },
                { "QualitySettings:LOD:LODBias", lodBiasSlider.value * 10},
            });
            
            WritePlayerPrefsKeys(new Dictionary<string, bool>
            {
                { "QualitySettings:LOD:LODCrossFade", lodCrossfadeToggle.isOn }
            });
            
            SetQualitySettingsFromPlayerPrefs();
        }
        
    }
}
