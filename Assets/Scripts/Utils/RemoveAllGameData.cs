using System.IO;
using UnityEngine;

namespace Utils
{
    public class RemoveAllGameData : MonoBehaviour
    {

        public void RemoveAllPlayerPreferences()
        {
            PlayerPrefs.DeleteAll();
            
            #if !UNITY_ANDROID
            File.Delete(ErrorTracker.LOGPath);
            #endif
        }

    }
}
