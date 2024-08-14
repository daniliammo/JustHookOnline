using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
	public class SliderValueChanger : MonoBehaviour
	{
		
		public void ChangeSliderValue(float value, Slider slider)
		{
			StartCoroutine(SmoothChangeSliderValue(value, slider));
		}

		[Pure]
		private static IEnumerator SmoothChangeSliderValue(float value, Slider slider)
		{
			var startValue = slider.value;
			var timeElapsed = 0f;

			while (timeElapsed < 0.2f)
			{
				timeElapsed += Time.deltaTime;
				slider.value = Mathf.Lerp(startValue, value, Mathf.Clamp01(timeElapsed / 0.2f));

				yield return null;
			}
		}

	}
}
