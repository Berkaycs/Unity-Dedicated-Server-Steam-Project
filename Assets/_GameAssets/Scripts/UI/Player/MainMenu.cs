#nullable enable
using Steamworks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    async void Start()
    {
        Player? player = await ConnectToBackend();

        if (player != null)
            Debug.Log($"Player logged in: {player.GamerTag}");
        else
            Debug.LogWarning("ConnectToBackend returned no player (Steam not initialized or login failed).");

        /*
        if (PlayerPrefs.HasKey("GamerTag"))
        {
            string gamerTag = PlayerPrefs.GetString("GamerTag");
            Player player = await PlayerService.GetPlayer("steam_id", gamerTag);
            Debug.Log($"Player retrieved: {player.GamerTag}");
        }
        else
        {
            _uiManager.ShowCreatePlayerScreen();
            _uiManager.OnPlayerCreated += UIManager_OnPlayerCreated;
        }
        */
    }

    private async void UIManager_OnPlayerCreated(string gamerTag)
    {
        Debug.Log($"Creating player: {gamerTag}");

        Player player = await PlayerService.CreatePlayer(gamerTag);

        Debug.Log($"Player created: {player.GamerTag}");
        PlayerPrefs.SetString("GamerTag", gamerTag);
    }

    private async Task<Player?> ConnectToBackend()
    {
        if (SteamManager.Initialized)
        {
            Byte[] ticket = new Byte[1024];
            uint sessionTicketSize;

            SteamNetworkingIdentity networkingIdentity = default;
            networkingIdentity.Clear();

            HAuthTicket ticketRequest = SteamUser.GetAuthSessionTicket(ticket, 1024, out sessionTicketSize, ref networkingIdentity);
            Array.Resize(ref ticket, (int)sessionTicketSize);

            string hex = BitConverter.ToString(ticket).Replace("-", "");
            return await LoginService.LoginPlayer(hex);
        }

        return null;
    }
}
