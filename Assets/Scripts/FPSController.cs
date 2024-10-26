using UnityEngine;


public class FPSController : MonoBehaviour
{
    
    private void Start()
    {
        // #if !UNITY_ANDROID
        // QualitySettings.vSyncCount = 1;
        // #endif
        
        SetFPS();
    }

    private static void SetFPS()
    {
        #if !UNITY_EDITOR
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        print(Screen.currentResolution.refreshRateRatio);
        #endif
        
        #if UNITY_EDITOR
        Application.targetFrameRate = 165;
	    #endif
    }

    #if !UNITY_EDITOR
    private void OnApplicationPause(bool pauseStatus)
    {
        switch (pauseStatus)
        {
            case true:
                Application.targetFrameRate = 1;
                break;
            case false:
                SetFPS();
                break;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        switch (hasFocus)
        {
            case true:
                SetFPS();
                break;
            case false:
                Application.targetFrameRate = 1;
                break;
        }
    }
    #endif
    
}
