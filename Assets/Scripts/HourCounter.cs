using UnityEngine;


public class HourCounter : MonoBehaviour
{

    private float _totalHoursPlayed;
    private float _sessionStartTime;

    
    private void Start()
    {
        _sessionStartTime = Time.realtimeSinceStartup; // Запоминаем время начала сессии
        _totalHoursPlayed = PlayerPrefs.GetFloat("TotalHoursPlayed", 0f); // Получаем сохраненное значение времени игры
    }

    private void OnApplicationQuit()
    {
        var sessionDuration = Time.realtimeSinceStartup - _sessionStartTime; // Вычисляем продолжительность текущей сессии
        _totalHoursPlayed += sessionDuration / 3600; // Обновляем общее время игры
        PlayerPrefs.SetFloat("TotalHoursPlayed", _totalHoursPlayed); // Сохраняем обновленное значение в PlayerPref
        PlayerPrefs.Save(); // Сохраняем изменения
        print(_totalHoursPlayed);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            var sessionDuration = Time.realtimeSinceStartup - _sessionStartTime; // Вычисляем продолжительность текущей сессии
            _totalHoursPlayed += sessionDuration / 3600; // Обновляем общее время игры
            PlayerPrefs.SetFloat("TotalHoursPlayed", _totalHoursPlayed); // Сохраняем обновленное значение в PlayerPref
            PlayerPrefs.Save(); // Сохраняем изменения
            print(_totalHoursPlayed);
        }
    }
    
}
