using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using GameSettings;

namespace Utils
{
	public class ErrorTracker : GameSettingsClass
	{

		public GameObject errorCanvas;
		
		public bool showWarning;
		public bool showError;
		public bool showException;
		public bool showAssert;

		public TMP_Text log;
		public TMP_Text logTypeText;
		public TMP_Text conditionText;
		public TMP_Text stackTraceText;

		private readonly string _path = Application.streamingAssetsPath + "/Log.txt";
		private string _toWrite;
		
		
		private void Awake()
		{
			CreateLogFile();
			
			CheckOrWritePlayerPrefsKeysString(new Dictionary<string, string>{{"ErrorTracker:Logs", ""}}, false);
			CheckOrWritePlayerPrefsKeysBoolean(new Dictionary<string, bool>
			{
				{"ErrorTracker:showWarning", false},
				{"ErrorTracker:showError", true},
				{"ErrorTracker:showException", true},
				{"ErrorTracker:showAssert", true}
			}, false);
			
			showWarning = PlayerPrefsBoolean.GetBool("ErrorTracker:showWarning");
			showError = PlayerPrefsBoolean.GetBool("ErrorTracker:showError");
			showException = PlayerPrefsBoolean.GetBool("ErrorTracker:showException");
			showAssert = PlayerPrefsBoolean.GetBool("ErrorTracker:showAssert");
			
			Application.logMessageReceived += LogMessage;
		}

		private void CreateLogFile()
		{
			#if !UNITY_ANDROID
			if(!File.Exists(_path))
				File.Create(_path);
			#endif
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
			#if UNITY_LINUX || UNITY_WINDOWS || UNITY_EDITOR
			using var writer = new StreamWriter(_path, true);
			writer.WriteLine(_toWrite);
			PlayerPrefs.SetString("Log:Logs", $"{PlayerPrefs.GetString("ErrorTracker:Logs")} \n {_toWrite}");
			#endif
			
			#if UNITY_ANDROID
			PlayerPrefs.SetString("Log:Logs", PlayerPrefs.GetString("ErrorTracker:Logs") + _toWrite);
			#endif
		}
		
	}
}
