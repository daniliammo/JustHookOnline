using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings.Quality
{
	public class TerrainQualitySettings : GameSettingsClass
	{

		public Slider pixelErrorSlider;
		public Slider baseMapDistanceSlider;
		public Slider detailDensityScaleSlider;
		public Slider detailDistanceSlider;
		public Slider treeDistanceSlider;
		public Slider billboardStartSlider;
		public Slider fadeLengthSlider;
		public Slider maxMeshTreesSlider;
		
		
		private void Awake()
		{
			CheckPlayerPrefsKeys(new Dictionary<string, float>
			{
				{ "QualitySettings:TerrainPixelError", 5 },
				{ "QualitySettings:BaseMapDistance", 1000 },
				{ "QualitySettings:DetailDensityScale", 1 },
				{ "QualitySettings:TreeDistance", 150 },
				{ "QualitySettings:BillboardStart", 75 },
				{ "QualitySettings:FadeLength", 1 },
				{ "QualitySettings:MaxMeshTrees", 250 }
			});
			SetSliderValuesFromPlayerPrefs();
		}

		private void SetSliderValuesFromPlayerPrefs()
		{
			pixelErrorSlider.value = PlayerPrefs.GetFloat("QualitySettings:TerrainPixelError");
			
			baseMapDistanceSlider.value = PlayerPrefs.GetFloat("QualitySettings:BaseMapDistance");
			
			detailDensityScaleSlider.value = PlayerPrefs.GetFloat("QualitySettings:DetailDensityScale");
			
			detailDistanceSlider.value = PlayerPrefs.GetFloat("QualitySettings:DetailDistance");
			
			treeDistanceSlider.value =PlayerPrefs.GetFloat("QualitySettings:TreeDistance");

			billboardStartSlider.value = PlayerPrefs.GetFloat("QualitySettings:BillboardStart");
			
			fadeLengthSlider.value = PlayerPrefs.GetFloat("QualitySettings:FadeLength");
			
			maxMeshTreesSlider.value = PlayerPrefs.GetFloat("QualitySettings:MaxMeshTrees");
		}
		
		public void SetQualitySettingsFromSlidersAndSaveToPlayerPrefs()
		{
			PlayerPrefs.SetFloat("QualitySettings:TerrainPixelError", pixelErrorSlider.value);
			QualitySettings.terrainPixelError = pixelErrorSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:BaseMapDistance", baseMapDistanceSlider.value);
			QualitySettings.terrainPixelError = baseMapDistanceSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:DetailDensityScale", detailDensityScaleSlider.value);
			QualitySettings.terrainPixelError = detailDensityScaleSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:DetailDistance", detailDistanceSlider.value);
			QualitySettings.terrainPixelError = detailDistanceSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:TreeDistance", treeDistanceSlider.value);
			QualitySettings.terrainPixelError = treeDistanceSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:BillboardStart", billboardStartSlider.value);
			QualitySettings.terrainPixelError = billboardStartSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:FadeLength", fadeLengthSlider.value);
			QualitySettings.terrainPixelError = fadeLengthSlider.value;
			
			PlayerPrefs.SetFloat("QualitySettings:MaxMeshTrees", maxMeshTreesSlider.value);
			QualitySettings.terrainPixelError = maxMeshTreesSlider.value;
		}
		
	}
}
