using UnityEngine;

namespace Utils
{
    public class RemoveAllPlayerPrefs : MonoBehaviour
    {

        public void RemoveAllPlayerPreferences()
        {
            PlayerPrefs.DeleteAll();
        }

    }
}
