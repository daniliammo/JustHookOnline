using System;
using UnityEngine;


[Serializable]
public struct HourMinute
{
    [SerializeField]
    [Range(0, 24)]
    public int hour;
    
    [Range(0, 59)]
    [SerializeField]
    public int minute;
}
