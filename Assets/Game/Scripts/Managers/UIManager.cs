using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject squadPanel;
    [SerializeField] private GameObject squadButtonNormal;
    [SerializeField] private GameObject squadButtonFocus;

    [SerializeField] private GameObject characterPanel;

    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private GameObject playButtonFocus;

    [SerializeField] private TextMeshProUGUI titleText;
    //[SerializeField] private GameObject 

    private void OnEnable()
    {
        GameEvents.OpenCharacterScreen += OpenCharacterPanel;
    }

    private void OnDisable()
    {
        GameEvents.OpenCharacterScreen -= OpenCharacterPanel;
    }

    public void OpenSquadPanel()
    {
        if (squadPanel.activeSelf) return;
        CloseAllPanels();

        squadPanel.SetActive(true);
        titleText.text = "SQUAD";
        squadButtonFocus.SetActive(true);

    }

    public void OpenLobby()
    {
        if (lobbyPanel.activeSelf) return;
        CloseAllPanels();
        titleText.text = "LOBBY";
        lobbyPanel.SetActive(true);
        playButtonFocus.SetActive(true);
        ActivateButtonNormals();

    }

    public void OpenCharacterPanel()
    {
        if (characterPanel.activeSelf) return;
        CloseAllPanels();
        characterPanel.SetActive(true);
        squadButtonFocus.SetActive(true);
        titleText.text = "X";

    }

    public void CloseAllPanels()
    {
        squadPanel.SetActive(false);
        characterPanel.SetActive(false);
        lobbyPanel.SetActive(false);

        squadButtonFocus.SetActive(false);
        squadButtonNormal.SetActive(false);
        playButtonFocus.SetActive(false);
    }

    private void ActivateButtonNormals()
    {
        squadButtonNormal.SetActive(true);
    }
}
