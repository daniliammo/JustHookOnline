using UnityEngine;
using UI;

namespace Player
{
    public class HitMarkerController : MonoBehaviour
    {

        private UIObjectsLinks _ui;


        private void Start()
        {
            _ui = FindFirstObjectByType<UIObjectsLinks>();
        }

        public void SetPlayerKilledText(string killedPlayerName)
        {
            _ui.playerKilledGameObject.SetActive(true);
            _ui.playerKilledText.text = $"{killedPlayerName} Ликвидирован!";
            Invoke(nameof(HidePlayerKilledText), 3);
        }
		
        private void HidePlayerKilledText()
        {
            _ui.playerKilledGameObject.SetActive(false);
        }
        
        public void EnableAndDisableMarker(bool isPlayerDead)
        {
            switch (isPlayerDead)
            {
                case false:
                    _ui.hitMarker.SetActive(true);
                    Invoke(nameof(DisableHitMarker), 0.15f);
                    break;
						
                case true:
                    _ui.killMarker.SetActive(true);
                    Invoke(nameof(DisableKillMarker), 0.15f);
                    break;
            }
        }

        private void DisableHitMarker()
        {
            _ui.hitMarker.SetActive(false);
        }
		
        private void DisableKillMarker()
        {
            _ui.killMarker.SetActive(false);
        }
        
    }
}
