using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace GameSettings
{
    public class GameSettingsClass : MonoBehaviour
    {

        #region Boolean
        protected static void CheckPlayerPrefsKeys(Dictionary<string, bool> keys)
        {
            foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                PlayerPrefsBoolean.SetBool(key.Key, key.Value);
        }
        
        protected static void WritePlayerPrefsKeys(Dictionary<string, bool> keys)
        {
            foreach (var key in keys) 
                PlayerPrefsBoolean.SetBool(key.Key, key.Value);
        }
        #endregion
        
        #region Int
        protected static void WritePlayerPrefsKeys(Dictionary<string, int> keys)
        {
            foreach (var key in keys) 
                PlayerPrefs.SetInt(key.Key, key.Value);
        }
        
        protected static void CheckPlayerPrefsKeys(Dictionary<string, int> keys)
        {
            foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                PlayerPrefs.SetInt(key.Key, key.Value);
        }
        #endregion
        
        #region Float
        protected static void WritePlayerPrefsKeys(Dictionary<string, float> keys)
        {
            foreach (var key in keys) 
                PlayerPrefs.SetFloat(key.Key, key.Value);
        }
        
        protected static void CheckPlayerPrefsKeys(Dictionary<string, float> keys)
        {
            foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                PlayerPrefs.SetFloat(key.Key, key.Value);
        }
        #endregion
        
        #region String
        protected static void WritePlayerPrefsKeys(Dictionary<string, string> keys)
        {
            foreach (var key in keys) 
                PlayerPrefs.SetString(key.Key, key.Value);
        }
        
        protected static void CheckPlayerPrefsKeys(Dictionary<string, string> keys)
        {
            foreach (var key in keys.Where(key => !PlayerPrefs.HasKey(key.Key)))
                PlayerPrefs.SetString(key.Key, key.Value);
        }
        #endregion
        
    }
}
