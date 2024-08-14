using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;

namespace Utils
{
    public class NetworkController : MonoBehaviour
    {
        
        public readonly Dictionary<long, ServerResponse> discoveredServers = new();
        
        private NetworkManager _networkManager;
        private NetworkDiscovery _networkDiscovery;
        
        
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
            discoveredServers[info.serverId] = info;
        }
        
        private void FindServers()
        {
            foreach (var i in discoveredServers.Values)
                print(i.EndPoint.Address);
            
            discoveredServers.Clear();
        }
        
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
