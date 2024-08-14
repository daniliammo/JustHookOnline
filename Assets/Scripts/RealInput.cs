using System.Diagnostics.Contracts;
using UnityEngine;

public abstract class RealInput
{

	public static bool IsTouchSupported => GetIsTouchSupported();


	[Pure]
	private static bool GetIsTouchSupported()
	{
		#if UNITY_STANDALONE_LINUX
		return false;
		#endif
		return Input.touchSupported;
	}

}
