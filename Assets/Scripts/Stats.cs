using System.Collections.Generic;
using System.Globalization;
using GameSettings;
using TMPro;
using UnityEngine;


public class Stats : GameSettingsClass
{

    public TMP_Text killsText;
    public TMP_Text deadsText;
    public TMP_Text kdText;
    public TMP_Text hoursInGameText;
    
    
    private void Start()
    {
        CheckOrWritePlayerPrefsKeysInt(new Dictionary<string, int>
        {
            {"Stats:Kills", 0},
            {"Stats:Deads", 0},
            {"Stats:FrameCount", 0}
        }, false);
        
        CheckOrWritePlayerPrefsKeysFloat(new Dictionary<string, float>
        { {"Stats:TotalHoursPlayed", 0f} }, false);
    }

    private void OnEnable()
    {
        var kills = PlayerPrefs.GetInt("Stats:Kills");
        var deads = PlayerPrefs.GetInt("Stats:Deads");
        var kd = (float)kills / deads;

        killsText.text = kills.ToString();
        deadsText.text = deads.ToString();
        kdText.text = kd.ToString(CultureInfo.CurrentCulture);
        // TODO: Реализация
        hoursInGameText.text = PlayerPrefs.GetFloat("Stats:TotalHoursPlayed").ToString(CultureInfo.CurrentCulture);
    }
    
}
