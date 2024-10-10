using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings.Quality
{
	public class TerrainQualitySettings : GameSettingsClass, IGameSettings
	{

		public Slider pixelErrorSlider;
		public Slider baseMapDistanceSlider;
		public Slider detailDensityScaleSlider;
		public Slider detailDistanceSlider;
		public Slider treeDistanceSlider;
		public Slider billboardStartSlider;
		public Slider fadeLengthSlider;
		public Slider maxMeshTreesSlider;
		
		
		private void Start()
		{
			CheckPlayerPrefsKeys(new Dictionary<string, float>
			{
				{ "QualitySettings:TerrainPixelError", 10 },
				{ "QualitySettings:BaseMapDistance", 1000 },
				{ "QualitySettings:DetailDensityScale", 1 },
				{ "QualitySettings:TreeDistance", 150 },
				{ "QualitySettings:BillboardStart", 75 },
				{ "QualitySettings:FadeLength", 1 },
				{ "QualitySettings:MaxMeshTrees", 250 }
			});
			SetUIValuesFromPlayerPrefs();
			LoadSettingsFromUI();
		}

		public void SetUIValuesFromPlayerPrefs()
		{
			pixelErrorSlider.value = PlayerPrefs.GetFloat("QualitySettings:TerrainPixelError");
			
			baseMapDistanceSlider.value = PlayerPrefs.GetFloat("QualitySettings:BaseMapDistance");
			
			detailDensityScaleSlider.value = PlayerPrefs.GetFloat("QualitySettings:DetailDensityScale");
			
			detailDistanceSlider.value = PlayerPrefs.GetFloat("QualitySettings:DetailDistance");
			
			treeDistanceSlider.value = PlayerPrefs.GetFloat("QualitySettings:TreeDistance");

			billboardStartSlider.value = PlayerPrefs.GetFloat("QualitySettings:BillboardStart");
			
			fadeLengthSlider.value = PlayerPrefs.GetFloat("QualitySettings:FadeLength");
			
			maxMeshTreesSlider.value = PlayerPrefs.GetFloat("QualitySettings:MaxMeshTrees");
		}

		public void LoadSettingsFromUI()
		{
			QualitySettings.terrainPixelError = pixelErrorSlider.value;
			QualitySettings.terrainBasemapDistance = baseMapDistanceSlider.value;
			QualitySettings.terrainDetailDensityScale = detailDensityScaleSlider.value;
			QualitySettings.terrainDetailDistance = detailDistanceSlider.value;
			QualitySettings.terrainTreeDistance = treeDistanceSlider.value;
			QualitySettings.terrainBillboardStart = billboardStartSlider.value;
			QualitySettings.terrainFadeLength = fadeLengthSlider.value;
			QualitySettings.terrainMaxTrees = maxMeshTreesSlider.value;
		}
		
		public void SaveSettings()
		{
			PlayerPrefs.SetFloat("QualitySettings:TerrainPixelError", pixelErrorSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:BaseMapDistance", baseMapDistanceSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:DetailDensityScale", detailDensityScaleSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:DetailDistance", detailDistanceSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:TreeDistance", treeDistanceSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:BillboardStart", billboardStartSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:FadeLength", fadeLengthSlider.value);
			PlayerPrefs.SetFloat("QualitySettings:MaxMeshTrees", maxMeshTreesSlider.value);
		}
		
	}
}
