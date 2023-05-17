using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject squadPanel;
    [SerializeField] private GameObject squadButtonNormal;
    [SerializeField] private GameObject squadButtonFocus;
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject playButtonFocus;
    [SerializeField] private TextMeshProUGUI titleText;


    [Header("CharacterPanel")]
    [SerializeField] private Image unitIconImage;
    [SerializeField] private Image unitIconGlowImage;
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitLevelText;
    [SerializeField] private List<GameObject> StarsOn;
    [SerializeField] private Slider unitAttackSlider;
    [SerializeField] private Slider unithealthSlider;
    [SerializeField] private TextMeshProUGUI unitAttackText;
    [SerializeField] private TextMeshProUGUI unitHealthText;
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

    public void OpenCharacterPanel(Unit unit)
    {
        if (characterPanel.activeSelf) return;
        CloseAllPanels();
        characterPanel.SetActive(true);
        squadButtonFocus.SetActive(true);
        titleText.text = "X";
        SetCharacterPanel(unit);
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

    private void SetCharacterPanel(Unit unit)
    {
        unitIconImage.sprite = unit.unitIcon;
        unitNameText.text = unit.unitName;
        unitLevelText.text = "LEVEL" + "<size=50> " + unit.unitLevel;
        unitAttackSlider.value = unit.unitBaseDamage;
        unitAttackSlider.value = unit.unitBaseHealth;
        unitAttackText.text = unit.unitBaseDamage.ToString();
        unitHealthText.text = unit.unitBaseHealth.ToString();
        switch (unit.unitRace)
        {
            case Unit.UnitRace.HUMAN:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowHuman;
                break;
            case Unit.UnitRace.DEMON:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowDemon;
                break;
            case Unit.UnitRace.ZOMBIE:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowZombie;             
                break;
        }

        for (int i = 0; i < StarsOn.Count; i++)
        {
            StarsOn[i].SetActive(false);
        }
        switch (unit.unitStarCount)
        {

            case Unit.UnitStarCount._1:
                for (int i = 0; i < 1; i++)
                {
                    StarsOn[i].SetActive(true);
                }
                break;
            case Unit.UnitStarCount._2:
                for (int i = 0; i < 2; i++)
                {
                    StarsOn[i].SetActive(true);
                }
                break;
            case Unit.UnitStarCount._3:
                for (int i = 0; i < 3; i++)
                {
                    StarsOn[i].SetActive(true);
                }
                break;
            case Unit.UnitStarCount._4:
                for (int i = 0; i < 4; i++)
                {
                    StarsOn[i].SetActive(true);
                }
                break;
            case Unit.UnitStarCount._5:
                for (int i = 0; i < 5; i++)
                {
                    StarsOn[i].SetActive(true);
                }
                break;
        }
    }
}