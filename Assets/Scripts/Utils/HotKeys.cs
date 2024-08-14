using UnityEngine;

namespace Utils
{
	public class HotKeys : MonoBehaviour
	{
	
		private Resolution _oldResolution;
		
		
		private void Update()
		{
			if(RealInput.IsTouchSupported) return;
			
			if (Input.GetKeyDown(KeyCode.F11) && !Screen.fullScreen)
			{
				_oldResolution.height = Screen.height;
				_oldResolution.width = Screen.width;
			
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			}

			if (Input.GetKeyDown(KeyCode.F11) && Screen.fullScreen) 
				Screen.SetResolution(_oldResolution.width, _oldResolution.height, false);
		}
	
	}
}
