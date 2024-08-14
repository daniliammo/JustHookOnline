using UnityEngine;

namespace Utils
{
	public static class CursorController
	{
		public static void SetCursorLockState(CursorLockMode cursorLockMode)
		{
			if(!RealInput.IsTouchSupported)
				Cursor.lockState = cursorLockMode;
		}
	}
}
