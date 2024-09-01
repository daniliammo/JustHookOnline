using Mirror;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class ShowConnectionQuality : MonoBehaviour
    {
        
        public TMP_Text connectionQuality;
        
        
        private void Start()
        {
            NetworkClient.onConnectionQualityChanged += OnConnectionQualityChanged;
        }

        private void OnConnectionQualityChanged(ConnectionQuality previous, ConnectionQuality current)
        {
            connectionQuality.text = current.ToString();
            connectionQuality.color = current.ColorCode();
        }
        
    }
}
