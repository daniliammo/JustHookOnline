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
            "Говорящий хлеб",
            "Нагетс",
            "Патрик",
            "Еван Гамазоло",
            "Артур Партизанович",
            "Я злюсь"
        };


        private void Start()
        {
            CheckOrWritePlayerPrefsKeysString(new Dictionary<string, string>{{"Nickname", _randomNicknames[Random.Range(0, _randomNicknames.Length)]}}, false);
            SetNicknameTextFromPlayerPrefs();
        }
        
        public void SaveNickname()
        {
            var nickname = nicknameInputField.text;
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
