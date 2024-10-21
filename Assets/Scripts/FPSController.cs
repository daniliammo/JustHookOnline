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
    
}
