using UnityEngine;
using System.Collections;

public class EnviroDayNightSwitch : MonoBehaviour {

	private Light[] lightsArray;

	private void Start () {

		lightsArray = GetComponentsInChildren<Light> ();

        EnviroSkyMgr.instance.OnDayTime += () =>
		{
			Deactivate () ;
		};

		EnviroSkyMgr.instance.OnNightTime += () =>
		{
			Activate () ;
		};

		if (EnviroSkyMgr.instance.IsNight())
			Activate ();
		else
			Deactivate ();
	}


	private void Activate () 
	{
		for (var i = 0; i < lightsArray.Length; i++) {
			lightsArray [i].enabled = true;
		}

	}

	private void Deactivate () 
	{
		for (var i = 0; i < lightsArray.Length; i++) {
			lightsArray [i].enabled = false;
		}
	}

}
