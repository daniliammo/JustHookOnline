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

		public static readonly string LOGPath = $"{Application.streamingAssetsPath}/Log.JHLog";
		private string _toWrite;
		
		
		private void Awake()
		{
			CreateLogFile();
			
			CheckPlayerPrefsKeys(new Dictionary<string, string>{{"ErrorTracker:Logs", ""}});
			CheckPlayerPrefsKeys(new Dictionary<string, bool>
			{
				{"ErrorTracker:showWarning", false},
				{"ErrorTracker:showError", true},
				{"ErrorTracker:showException", true},
				{"ErrorTracker:showAssert", true}
			});
			
			showWarning = PlayerPrefsBoolean.GetBool("ErrorTracker:showWarning");
			showError = PlayerPrefsBoolean.GetBool("ErrorTracker:showError");
			showException = PlayerPrefsBoolean.GetBool("ErrorTracker:showException");
			showAssert = PlayerPrefsBoolean.GetBool("ErrorTracker:showAssert");
			
			Application.logMessageReceived += LogMessage;
			Application.quitting += WriteLogs;
		}

		private static void CreateLogFile()
		{
			#if !UNITY_ANDROID
			if(!File.Exists(LOGPath))
				File.Create(LOGPath);
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
			
			// Запись логов во время игры отключена для производительности
			// WriteLogs();
			
			log.text = $"{type}: {condition}";
		}

		private void WriteLogs()
		{
			#if UNITY_LINUX || UNITY_WINDOWS || UNITY_EDITOR
			using var writer = new StreamWriter(LOGPath, true);
			writer.WriteLine(_toWrite);
			PlayerPrefs.SetString("Log:Logs", $"{PlayerPrefs.GetString("ErrorTracker:Logs")} \n {_toWrite}");
			#endif
			
			#if UNITY_ANDROID
			PlayerPrefs.SetString("Log:Logs", PlayerPrefs.GetString("ErrorTracker:Logs") + _toWrite);
			#endif
		}
		
	}
}
