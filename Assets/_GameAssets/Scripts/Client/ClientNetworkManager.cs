using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ClientNetworkManager : NetworkManager
{
    private async void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;
        Debug.Log("Client Ready");
    }
}
