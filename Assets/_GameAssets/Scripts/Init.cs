using Steamworks;
using TMPro;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            string gamerTag = SteamFriends.GetPersonaName();
            _playerNameText.text = gamerTag;
        }
    }
}
