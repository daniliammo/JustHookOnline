using System;
using UnityEngine;


[Serializable]
public struct MatchConfig
{

    public string matchName;
    
    public bool allowPolice;
    public bool infiniteAmmo;
    public Vector3 gravity;
    public bool allowWalkOnAir;
    public bool allowGrenades;

}
