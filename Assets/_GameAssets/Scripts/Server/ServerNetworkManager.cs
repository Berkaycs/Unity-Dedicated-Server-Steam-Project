using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System;

public class ServerNetworkManager : NetworkManager
{
    private async void OnEnable()
    {
        await GameServerService.RegisterServer();

        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        OnServerStarted += ServerNetworkManager_OnServerStarted;
        OnClientConnectedCallback += ServerNetworkManager_OnClientConnected;
        OnClientDisconnectCallback += ServerNetworkManager_OnClientDisconnected;

        StartServer();

        await GameServerService.ReadyForPlayers();
    }

    private void ServerNetworkManager_OnServerStarted()
    {
        Console.WriteLine("Server listening for connections.");
    }

    private void ServerNetworkManager_OnClientConnected(ulong clientId)
    {
        Console.WriteLine("Client connected: " + clientId);
    }

    private void ServerNetworkManager_OnClientDisconnected(ulong clientId)
    {
        Console.WriteLine("Client disconnected: " + clientId);
    }
}
