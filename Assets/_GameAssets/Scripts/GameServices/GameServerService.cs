using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class GameServerService
{
    private static string BASE_URL = "http://127.0.0.1:5000";
    private static string POST_REGISTER_SERVER = BASE_URL + "/server/register";
    private static string UPDATE_SERVER_STATUS = BASE_URL + "/server/status";

    public static Server server;

    public static async Task<bool> RegisterServer()
    {
        string ip = GetIPAdress();

        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("IpAdress", ip);

        string response = await ServerRequest.PostRequest(POST_REGISTER_SERVER, data);
        server = JsonUtility.FromJson<Server>(response);

        Console.WriteLine($"Server registered with backend");
        Console.WriteLine($"Id: { server.Id}");
        Console.WriteLine($"Ip: {server.IpAdress}");
        Console.WriteLine($"Ip: {server.Status}");

        return true;
    }

    public static async Task<bool> ReadyForPlayers()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("Id", server.Id);
        data.Add("Status", "WAITING_FOR_PLAYERS");

        string response = await ServerRequest.PostRequest(UPDATE_SERVER_STATUS, data);
        server = JsonUtility.FromJson<Server>(response);

        return true;
    }

    private static string GetIPAdress()
    {
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || 
                networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.Address.ToString();
                    }
                }
            }
        }

        return "127.0.0.1";
    }
}
