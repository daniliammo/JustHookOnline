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
        
        public NetworkManager networkManager;
        public NetworkDiscovery networkDiscovery;
        public TMP_Text serversList;
        // public TMP_Text fastConnectButtonText;

        // private string ipAddress;
        
        
        private void Start()
        {
            networkDiscovery.StartDiscovery();
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
            networkManager.networkAddress = ip;
            networkManager.StartClient();
        }

        public void StartHost()
        {
            networkManager.StartHost();
            networkDiscovery.AdvertiseServer();
        }
        
        public void StopNetwork()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
                networkDiscovery.StopDiscovery();
            }
            
            if(NetworkClient.isConnected)
                NetworkManager.singleton.StopClient();
        }
        
    }
}
