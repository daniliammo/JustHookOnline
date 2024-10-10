using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShowSliderValue : MonoBehaviour
    {

        private TMP_Text _text;
        
        
        private void Start()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void OnSliderValueChanged(float value)
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();
            
            _text.text = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
        }
        
    }
}
