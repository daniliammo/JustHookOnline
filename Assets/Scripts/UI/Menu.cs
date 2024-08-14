using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Utils;
using Random = UnityEngine.Random;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        
        private NetworkController _networkController;
        
        public bool isPaused;
        
        public GameObject pause;
        public GameObject joiningToGamePanel;
        public GameObject disconnectPanel;
        public GameObject canvasGame;
        public TMP_Text joiningToIpAddress;
        public TMP_InputField ipAddressText;
        
        
        private void Start()
        {
            _networkController = FindObjectOfType<NetworkController>();
            
            NetworkEvents.OnClientStarted += OnClientStarted;
            NetworkEvents.OnClientStopped += OnClientStop;
        }

        private void OnClientStarted()
        {
            canvasGame.SetActive(true);
            joiningToGamePanel.SetActive(false);
        }
        
        private void OnClientStop()
        {
            CursorController.SetCursorLockState(CursorLockMode.None);
            canvasGame.SetActive(false);
            disconnectPanel.SetActive(true);
        }
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            Pause();
            LockOrUnlockCursor();
        }

        private void LockOrUnlockCursor()
        {
            switch (isPaused)
            {
                case true:
                    CursorController.SetCursorLockState(CursorLockMode.None);
                    break;
                case false:
                    LockCursor();
                    break;
            }
        }
        
        public void Pause()
        {
            isPaused = !isPaused;
            pause.SetActive(isPaused);
        }
        
        public static void LockCursor()
        {
            CursorController.SetCursorLockState(CursorLockMode.Locked);
        }
        
        public void OnStartHostButtonClicked()
        {
            _networkController.StartHost();
        }
        
        public void OnJoinButtonClicked()
        {
            var ip = ipAddressText.text.Length == 0 ? "localhost" : ipAddressText.text;
            
            _networkController.StartClient(ip);
            
            joiningToGamePanel.SetActive(true);
            joiningToIpAddress.text = ip;
        }

        public void OnJoinToRandomGameButtonClicked()
        {
            if (!_networkController.discoveredServers.TryGetValue(Random.Range(0, _networkController.discoveredServers.Count), out var server)) return;
            var address = server.EndPoint.Address.ToString();
            joiningToIpAddress.text = address;
            _networkController.StartClient(address);
        }
        
        public void OnExitGameButtonClicked()
        {
            Application.Quit();
        }

        public void OnRestartSceneButtonClicked()
        {
            _networkController.StopNetwork();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
