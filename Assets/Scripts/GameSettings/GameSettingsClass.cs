using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace GameSettings
{
    public class GameSettingsClass : MonoBehaviour
    {
        
        protected static void CheckOrWritePlayerPrefsKeysBoolean(Dictionary<string, bool> keys, bool write)
        {
            switch (write)
            {
                case false:
                    foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                        PlayerPrefsBoolean.SetBool(key.Key, key.Value);
                    break;
                
                case true:
                    foreach (var key in keys)
                        PlayerPrefsBoolean.SetBool(key.Key, key.Value);
                    break;
            }
        }
        
        protected static void CheckOrWritePlayerPrefsKeysInt(Dictionary<string, int> keys, bool write)
        {
            switch (write)
            {
                case false:
                    foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                        PlayerPrefs.SetInt(key.Key, key.Value);
                    break;
                
                case true:
                    foreach (var key in keys)
                        PlayerPrefs.SetInt(key.Key, key.Value);
                    break;
            }
        }
        
        protected static void CheckOrWritePlayerPrefsKeysFloat(Dictionary<string, float> keys, bool write)
        {
            switch (write)
            {
                case false:
                    foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                        PlayerPrefs.SetFloat(key.Key, key.Value);
                    break;
                
                case true:
                    foreach (var key in keys)
                        PlayerPrefs.SetFloat(key.Key, key.Value);
                    break;
            }
        }
        
        protected static void CheckOrWritePlayerPrefsKeysString(Dictionary<string, string> keys, bool write)
        {
            switch (write)
            {
                case false:
                    foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                        PlayerPrefs.SetString(key.Key, key.Value);
                    break;
                
                case true:
                    foreach (var key in keys)
                        PlayerPrefs.SetString(key.Key, key.Value);
                    break;
            }
        }
        
    }
}
