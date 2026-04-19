using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequest
{
    public static async Task<string> GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Couldnt connect to backend server");
                return null;
            }

            return webRequest.downloadHandler.text;
        }
    }

    public static async Task<string> PostRequest(string url, Dictionary<string, string> data)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, data))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Couldnt connect to backend server");
                return null;
            }

            return webRequest.downloadHandler.text;
        }
    }
}
