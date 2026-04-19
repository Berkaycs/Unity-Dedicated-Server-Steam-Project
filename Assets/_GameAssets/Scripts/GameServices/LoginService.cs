using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoginService
{
    private static string BASE_URL = "http://127.0.0.1:5000";
    private static string POST_LOGIN_PLAYER = BASE_URL + "/login/steam";

    public static async Task<Player> LoginPlayer(string sessionTicket)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("SessionTicket", sessionTicket);

        string response = await ServerRequest.PostRequest(POST_LOGIN_PLAYER, data);

        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }
}
