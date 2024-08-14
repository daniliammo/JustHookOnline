using TMPro;
using UnityEngine;


namespace Utils
{
    public class ViewLogs : MonoBehaviour
    {

        public TMP_Text logsText;
        private string _path;


        private void Start()
        {
            SetPath();
        }

        private void SetPath()
        {
            #if UNITY_LINUX || UNITY_WINDOWS || UNITY_EDITOR
            _path = Application.dataPath + "/StreamingAssets";
            #endif
        }
        
        public void WriteLogsToText()
        {
            if(PlayerPrefs.GetString("Log:Logs") == "")
            {
                logsText.text = "ПУСТО!";
                return;
            }

            if (!PlayerPrefs.HasKey("Log:Logs"))
            {
                logsText.text = "Нету ключа";
                return;
            }

            logsText.text = PlayerPrefs.GetString("Log:Logs");
        }
        
    }
}
