using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class NetworkController : MonoBehaviour
    {
        
        public readonly Dictionary<long, ServerResponse> DiscoveredServers = new();
        
        private NetworkManager _networkManager;
        private NetworkDiscovery _networkDiscovery;
        public TMP_Text serversList;
        // public TMP_Text fastConnectButtonText;

        // private string ipAddress;
        
        
        private void Start()
        {
            _networkManager = FindObjectOfType<NetworkManager>();
            _networkDiscovery = FindObjectOfType<NetworkDiscovery>();
            
            _networkDiscovery.StartDiscovery();
            InvokeRepeating(nameof(FindServers), 0, 1);
            
            Application.quitting += StopNetwork;
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            DiscoveredServers[info.serverId] = info;
        }
        
        private void FindServers()
        {
            var ipAddress = "";
            foreach (var discoveredServersValue in DiscoveredServers.Values)
            {
                print(discoveredServersValue.EndPoint.Address);
                ipAddress += $"{discoveredServersValue.EndPoint.Address}\n";
            }
            
            // fastConnectButtonText.text = $"{ipAddress} - Подключиться";
            
            serversList.text = ipAddress;
            
            DiscoveredServers.Clear();
        }

        // public void OnFastConnectButtonClicked()
        // {
        //     StartClient(ipAddress);
        // }
        
        public void StartClient(string ip)
        {
            _networkManager.networkAddress = ip;
            _networkManager.StartClient();
        }

        public void StartHost()
        {
            _networkManager.StartHost();
            _networkDiscovery.AdvertiseServer();
        }
        
        public void StopNetwork()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
                _networkDiscovery.StopDiscovery();
            }
            
            if(NetworkClient.isConnected)
                NetworkManager.singleton.StopClient();
        }
        
    }
}
