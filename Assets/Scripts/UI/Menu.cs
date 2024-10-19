using System.Collections.Generic;
using GameSettings;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using Utils;
using Random = UnityEngine.Random;

namespace UI
{
    public class Menu : GameSettingsClass
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
            _networkController = FindFirstObjectByType<NetworkController>();
            
            CheckPlayerPrefsKeys(new Dictionary<string, bool>{{"NetworkSettings:CloseGameOnDisconnect", false}});
            
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
            if(PlayerPrefsBoolean.GetBool("NetworkSettings:CloseGameOnDisconnect"))
            {
                Application.Quit();
                #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
                #endif
            }

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
            var ip = ipAddressText.text.Length == 0 ? "127.0.0.1" : ipAddressText.text;
            
            _networkController.StartClient(ip);
            
            joiningToGamePanel.SetActive(true);
            joiningToIpAddress.text = ip;
        }

        public void OnCancelConnectionButtonClicked()
        {
            _networkController.StopNetwork();
        }
        
        public void OnJoinToRandomGameButtonClicked()
        {
            if(_networkController.DiscoveredServers.Count == 0) 
                return;

            var server =
                _networkController.DiscoveredServers[Random.Range(0, _networkController.DiscoveredServers.Count)];
            
            var address = server.EndPoint.Address.ToString();
            
            joiningToGamePanel.SetActive(true);
            joiningToIpAddress.text = address;
            
            _networkController.StartClient(address);
        }
        
        public void OnExitGameButtonClicked()
        {
            Application.Quit();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }

        public void OnRestartSceneButtonClicked()
        {
            _networkController.StopNetwork();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
