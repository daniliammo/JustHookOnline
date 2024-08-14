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
            {"Kills", 0},
            {"Deads", 0}
        }, false);
    }

    private void OnEnable()
    {
        var kills = PlayerPrefs.GetInt("Kills");
        var deads = PlayerPrefs.GetInt("Deads");
        var kd = (float)kills / deads;

        killsText.text = kills.ToString();
        deadsText.text = deads.ToString();
        kdText.text = kd.ToString(CultureInfo.CurrentCulture);
        // TODO: Реализация
        hoursInGameText.text = PlayerPrefs.GetFloat("TotalHoursPlayed").ToString(CultureInfo.CurrentCulture);
    }
    
}
