using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Utils
{
	public static class PlayerPrefsBoolean
	{

		public static void SetBool(string keyName, bool value)
		{
			switch (value)
			{
				case true:
					PlayerPrefs.SetInt(keyName, 1);
					break;
				
				case false:
					PlayerPrefs.SetInt(keyName, 0);
					break;
			}
		}

		[Pure]
		public static bool GetBool(string keyName)
		{
			if(PlayerPrefs.HasKey(keyName))
			{
				var value = PlayerPrefs.GetInt(keyName);
				
				if(value != 0 && value != 1)
					Debug.LogError($"Ключ {keyName} не boolean. {keyName} равен: {value}");
				
				switch (value)
				{
					case 1:
						return true;
					case 0:
						return false;
				}
			}
			
			if(!PlayerPrefs.HasKey(keyName))
				Debug.LogError($"Нету ключа '{keyName}'.");
			
			throw new Exception($"Произошла ошибка при обработке: {keyName}. Значение: {PlayerPrefs.GetInt(keyName)}");
		}
	
	}
}
