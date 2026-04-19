using System.Threading.Tasks;
using UnityEngine;

public class PlayerService
{
    private static string BASE_URL = "http://127.0.0.1:5000";
    private static string GET_PLAYER = BASE_URL + "/player/{0}/{1}";

    public static async Task<Player> GetPlayer(string id, string gamerTag)
    {
        string response = await ServerRequest.GetRequest(string.Format(GET_PLAYER, id, gamerTag));
        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }

    public static async Task<Player> CreatePlayer(string gamerTag)
    {
        string response = await ServerRequest.GetRequest(string.Format(GET_PLAYER, "none", gamerTag));
        Player player = JsonUtility.FromJson<Player>(response);
        PlayerPrefs.SetString("GamerTag", player.GamerTag);
        return player;
    }
}
