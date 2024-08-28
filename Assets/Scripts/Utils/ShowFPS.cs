using System.Globalization;
using TMPro;
using UnityEngine;

namespace Utils
{
	public class ShowFPS : MonoBehaviour 
	{
	
		public TMP_Text fpsText;
		private float _deltaTime;
		private short _fps;
		

		private void Update() 
		{
			_deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
			_fps = (byte)(1 / _deltaTime);
			fpsText.text = $"{_fps.ToString(CultureInfo.InvariantCulture)} / {Application.targetFrameRate}";
		}
	
	}
}
