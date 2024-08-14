using System;
using System.IO;
using TMPro;
using UnityEngine;

namespace Utils
{
	public class ErrorTracker : MonoBehaviour
	{

		public GameObject errorCanvas;

		[Header("Что показывать")]
		public bool showWarning;
		public bool showError;
		public bool showException;
		public bool showAssert;

		public TMP_Text log;
		public TMP_Text logTypeText;
		public TMP_Text conditionText;
		public TMP_Text stackTraceText;

		private readonly string _path = Application.streamingAssetsPath + "/LogMessages.txt";
		private string _toWrite;
		

		private void Start()
		{
			if(Application.isEditor) return;

			CreateLogFile();
			CheckPlayerPrefs();
			
			// Подписка на событие logMessageReceived
			Application.logMessageReceivedThreaded += LogMessage;
			
			showWarning = PlayerPrefsBoolean.GetBool("ErrorTracker:showWarning");
			showError = PlayerPrefsBoolean.GetBool("ErrorTracker:showError");
			showException = PlayerPrefsBoolean.GetBool("ErrorTracker:showException");
			showAssert = PlayerPrefsBoolean.GetBool("ErrorTracker:showAssert");
		}

		private void CreateLogFile()
		{
			#if !UNITY_ANDROID
			if(!File.Exists(_path))
				File.Create(_path);
			#endif
		}
		
		private static void CheckPlayerPrefs()
		{
			if(!PlayerPrefs.HasKey("Log:Logs"))
				PlayerPrefs.SetString("Log:Logs", "");
            
			if(!PlayerPrefs.HasKey("ErrorTracker:showWarning"))
				PlayerPrefsBoolean.SetBool("ErrorTracker:showWarning", false);
			
			if(!PlayerPrefs.HasKey("ErrorTracker:showError"))
				PlayerPrefsBoolean.SetBool("ErrorTracker:showError", true);
			
			if(!PlayerPrefs.HasKey("ErrorTracker:showException"))
				PlayerPrefsBoolean.SetBool("ErrorTracker:showException", true);
			
			if(!PlayerPrefs.HasKey("ErrorTracker:showAssert"))
				PlayerPrefsBoolean.SetBool("ErrorTracker:showAssert", true);
		}
        
		public void DisableShowingAllMessages()
		{
			showWarning = false;
			showError = false;
			showException = false;
			showAssert = false;
			
			PlayerPrefsBoolean.SetBool("ErrorTracker:showWarning", showWarning);
			PlayerPrefsBoolean.SetBool("ErrorTracker:showError", showError);
			PlayerPrefsBoolean.SetBool("ErrorTracker:showException", showException);
			PlayerPrefsBoolean.SetBool("ErrorTracker:showAssert", showAssert);
		}
		
		private void LogMessage(string condition, string stackTrace, LogType type)
		{
			var currentTime = DateTime.Now;
			var stringToLog = $"{currentTime} {type}: {condition} {stackTrace}\n";
			_toWrite += stringToLog;
			WriteLogs();
			
			log.text = $"{currentTime.Hour}:{currentTime.Minute}:{currentTime.Second} - {condition}";
			
			// switch (type)
			// {
			// 	case LogType.Exception when !showException:
			// 	case LogType.Error when !showError:
			// 	case LogType.Warning when !showWarning:
			// 	case LogType.Assert when !showAssert:
			// 	case LogType.Log:
			// 		return;
			// }

			// var logType = type.ToString();
			//
			// CursorController.SetCursorLockState(CursorLockMode.Confined);
			//
			// if (errorCanvas.activeSelf)
			// 	logType += " (Повторно)";
			// 	
			// errorCanvas.SetActive(true);
			//
			// logTypeText.text = logType;
			// conditionText.text = condition;
			// stackTraceText.text = stackTrace;
		}

		private void WriteLogs()
		{
			#if UNITY_LINUX || UNITY_WINDOWS
			// Записываем сообщение в файл
			using var writer = new StreamWriter(_path, true);
			writer.WriteLine(_toWrite);
			#endif
			
			#if UNITY_ANDROID
			PlayerPrefs.SetString("Log:Logs", PlayerPrefs.GetString("Log:Logs") + _toWrite);
			#endif
		}
		
	}
}
