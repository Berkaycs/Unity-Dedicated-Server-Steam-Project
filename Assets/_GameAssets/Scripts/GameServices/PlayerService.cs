using System.Threading.Tasks;
using UnityEngine;

public class PlayerService
{
    private static string BASE_URL = "http://127.0.0.1:5000";
    private static string GET_PLAYER = BASE_URL + "/player/{0}";

    public static async Task<Player> GetPlayer(string id)
    {
        string response = await ServerRequest.GetRequest(string.Format(GET_PLAYER, id));
        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }
}
