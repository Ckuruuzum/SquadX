using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject lobbyMid;


    [SerializeField] private GameObject squadPanel;
    [SerializeField] private GameObject squadButtonNormal;
    [SerializeField] private GameObject squadButtonFocus;

    [SerializeField] private GameObject characterPanel;

    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private GameObject playButtonFocus;

    [SerializeField] private TextMeshProUGUI titleText;
    //[SerializeField] private GameObject 


    public void OpenSquadPanel()
    {
        titleText.text = "SQUAD";
        squadPanel.SetActive(true);
        squadButtonFocus.SetActive(true);
        squadButtonNormal.SetActive(false);
        lobbyMid.SetActive(false);
        characterPanel.SetActive(false);
        //lobbyPanel.SetActive(false);
        playButtonFocus.SetActive(false);
    }

    public void OpenLobby()
    {
        titleText.text = "LOBBY";
        lobbyMid.SetActive(true);
        playButtonFocus.SetActive(true);
        squadPanel.SetActive(false);
        squadButtonFocus.SetActive(false);
        squadButtonNormal.SetActive(true);
        characterPanel.SetActive(false);
    }
}
