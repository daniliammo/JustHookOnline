using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GameSettings
{
    public class NicknameSetter : GameSettingsClass
    {
        
        public delegate void NicknameChanged(string nickname);
        public event NicknameChanged OnNicknameChanged;
        
        public TMP_InputField nicknameInputField;
        private readonly string[] _randomNicknames = {
            "Томас Шелби",
            "Кольт Кольтович",
            "Пищ",
            "Абдула",
            "Самандарчик",
            "Сигма",
            "Сигмачевский",
            "Маяковский",
            "Обдолбачевский",
            "Атуик",
            "Ирина виганска",
            "Бебрачевский",
            "Блиски",
            "Близкий",
            "Булитос Чипулитосович",
            "Пщух",
            "Пщи",
            "Антонио Вивальди",
            "Чиво",
            "Але",
            "Данилияммо чипулитосович",
            "МамаЯЛюблюКокаин",
            "Белис Мавида",
            "Фелис Навида",
            "Говорящий хлеб",
            "Нагетс",
            "Патрик",
            "Еван Гамазоло",
            "Бебраковский",
            "Артур Партизанович",
            "Манама",
            "Намана",
            "Дональд Пидар",
            "Томас Пидар",
            "Я злюсь",
            "Станислав Бачинский",
            "Антонио Челентано",
            "MrVladus52",
            "Влад с первого этажа",
            "Руслан",
            "Пачиму"
        };


        private void Start()
        {
            var nickname = PlayerPrefs.GetString("Nickname");
            
            if (_randomNicknames.Contains(nickname) ||
                nickname.Length < 1 ||
                nickname.All(char.IsWhiteSpace))
                PlayerPrefs.SetString("Nickname", _randomNicknames[Random.Range(0, _randomNicknames.Length)]);
            
            SetNicknameTextFromPlayerPrefs();
        }
        
        public void SaveNickname()
        {
            var nickname = nicknameInputField.text;
            if(nickname.Length == 0) return;
            OnNicknameChanged?.Invoke(nickname);
            PlayerPrefs.SetString("Nickname", nickname);
        }

        private void SetNicknameTextFromPlayerPrefs()
        {
            var nickname = PlayerPrefs.GetString("Nickname");
            if (!_randomNicknames.Contains(nickname))
                nicknameInputField.text = nickname;
        }
        
    }
}
