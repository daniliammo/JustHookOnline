using UnityEngine;

namespace Utils
{
    public class NoUnityTelemetry : MonoBehaviour
    {
        
        private void Start()
        {
            RemoveTelemetryKeys();
            InvokeRepeating(nameof(RemoveTelemetryKeys), 1, 15);
        }
        
        private void OnApplicationQuit()
        {
            RemoveTelemetryKeys();
        }

        private void RemoveTelemetryKeys()
        {
            PlayerPrefs.SetString("unity.player_session_count", RandomString.GetRandomString(4));
            PlayerPrefs.SetString("unity.player_sessionid", RandomString.GetRandomString(24));
        }
        
    }
}
