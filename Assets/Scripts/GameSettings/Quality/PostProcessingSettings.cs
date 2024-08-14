using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using Utils;

namespace GameSettings.Quality
{
	public class PostProcessingSettings : GameSettingsClass
	{

		public Toggle postProcessingToggle;
		
		
		private void Start()
		{
			CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>{{"PostProcessSettings:IsEnabled", true}}, false);
			EnableOrDisablePostProcessing(PlayerPrefsBoolean.GetBool("PostProcessSettings:IsEnabled"));
			SetToggleValueFromPlayerPrefs();
		}

		private void SetToggleValueFromPlayerPrefs()
		{
			postProcessingToggle.isOn = PlayerPrefsBoolean.GetBool("PostProcessSettings:IsEnabled");
		}

		public void SavePostProcessingSettings()
		{
			CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>{{"PostProcessSettings:IsEnabled", postProcessingToggle.isOn}}, true);
			EnableOrDisablePostProcessing(PlayerPrefsBoolean.GetBool("PostProcessSettings:IsEnabled"));
		}
		
		private static void EnableOrDisablePostProcessing(bool enable)
		{
			Resources.FindObjectsOfTypeAll<PostProcessVolume>()[0].gameObject.SetActive(enable);
			Resources.FindObjectsOfTypeAll<FlareLayer>()[0].enabled = enable;
			Resources.FindObjectsOfTypeAll<PostProcessLayer>()[0].enabled = enable;
		}
	}
}
