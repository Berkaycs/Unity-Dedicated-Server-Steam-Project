using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Events
    public event Action<string> OnPlayerCreated;
    #endregion

    #region MainMenu
    [Header("Main Menu")]
    [SerializeField] private GameObject _createPlayerScreen;
    [SerializeField] private TMP_Text _gamerTagText;
    [SerializeField] private Button _submitGamerTagButton;
    #endregion

    private void Start()
    {
        _submitGamerTagButton.onClick.AddListener(OnSubmitGamerTag);
    }

    public void ShowCreatePlayerScreen()
    {
        _createPlayerScreen.SetActive(true);
    }

    private void OnSubmitGamerTag()
    {
        string gamerTag = _gamerTagText.text;
        OnPlayerCreated?.Invoke(gamerTag);
    }
}
