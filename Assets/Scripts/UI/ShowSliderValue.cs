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

        public void OnSliderSensitivityValueChanged(float value)
        {
            _text.text = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
        }
        
    }
}
