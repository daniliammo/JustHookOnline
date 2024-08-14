using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Utils
{
	public static class PlayerPrefsBoolean
	{

		public static void SetBool(string key, bool boolean)
		{
			switch (boolean)
			{
				case true:
					PlayerPrefs.SetInt(key, 1);
					break;
				
				case false:
					PlayerPrefs.SetInt(key, 0);
					break;
			}
		}

		[Pure]
		public static bool GetBool(string key)
		{
			if(PlayerPrefs.HasKey(key))
			{
				if (PlayerPrefs.GetInt(key) == 1)
					return true;
				
				if (PlayerPrefs.GetInt(key) == 0)
					return false;
			}
			
			if(!PlayerPrefs.HasKey(key))
				Debug.LogError($"Нету ключа '{key}'");

			throw new NullReferenceException();
		}
	
	}
}
