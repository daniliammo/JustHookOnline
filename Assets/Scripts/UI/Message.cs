using UnityEngine;

namespace UI
{
    public class Message : MonoBehaviour
    {

        public float time;
        
        
        private void OnEnable()
        {
            Invoke(nameof(DisableMessage), time);
        }

        private void DisableMessage()
        {
            gameObject.SetActive(false);
        }

    }
}
