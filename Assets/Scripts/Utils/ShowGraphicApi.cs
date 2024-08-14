using TMPro;
using UnityEngine;

namespace Utils
{
	public class ShowGraphicApi : MonoBehaviour
	{

		public TMP_Text graphicApiText;


		private void Start()
		{
			graphicApiText.text = SystemInfo.graphicsDeviceType.ToString();
			Destroy(gameObject);
		}
	}
}
