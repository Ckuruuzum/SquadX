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
    [SerializeField] private GameObject stagePanel;
    [SerializeField] private GameObject[] levelMaps;


    [Header("CharacterPanel")]
    [SerializeField] private Image unitIconImage;
    [SerializeField] private Image unitIconGlowImage;
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitLevelText;
    [SerializeField] private List<GameObject> StarsOn;
    [SerializeField] private Slider unitAttackSlider;
    [SerializeField] private Slider unitHealthSlider;
    [SerializeField] private TextMeshProUGUI unitAttackText;
    [SerializeField] private TextMeshProUGUI unitHealthText;
    [SerializeField] private TextMeshProUGUI unitTierText;
    [SerializeField] private Image unitTierGlow;

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

    public void OpenStagePanel()
    {
        if (stagePanel.activeSelf) return;
        CloseAllPanels();
        stagePanel.SetActive(true);
        titleText.text = "STAGES";
        ActivateButtonNormals();
    }

    public void OpenSelectedLevelMap(GameObject levelMap)
    {
        if (levelMap.activeSelf) return;
        levelMap.SetActive(true);
        titleText.text = "LevelMap";
    }

    private void CloseAllPanels()
    {
        squadPanel.SetActive(false);
        characterPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        stagePanel.SetActive(false);

        for (int i = 0; i < levelMaps.Length; i++)
        {
            levelMaps[i].SetActive(false);
        }

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
        unitHealthSlider.value = unit.unitBaseHealth;
        unitAttackText.text = unit.unitBaseDamage.ToString();
        unitHealthText.text = unit.unitBaseHealth.ToString();
        
        switch (unit.unitRace)
        {
            case Unit.UnitRace.HUMAN:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowHuman;
                unitTierText.color = unit.unitCardDatabase.tierTextColorHuman;
                unitTierGlow.color = unit.unitCardDatabase.tierTextGlowHuman;

                break;
            case Unit.UnitRace.DEMON:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowDemon;
                unitTierText.color = unit.unitCardDatabase.tierTextColorDemon;
                unitTierGlow.color = unit.unitCardDatabase.tierTextGlowDemon;
                break;
            case Unit.UnitRace.ZOMBIE:
                unitIconGlowImage.color = unit.unitCardDatabase.bottomGlowZombie;                   
                unitTierText.color = unit.unitCardDatabase.tierTextColorZombie;
                unitTierGlow.color = unit.unitCardDatabase.tierTextGlowZombie;
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
        switch (unit.unitTier)
        {
            case Unit.UnitTier.S:
                unitTierText.text = "S";
                break;
            case Unit.UnitTier.A:
                unitTierText.text = "A";
                break;
            case Unit.UnitTier.B:
                unitTierText.text = "B";
                break;
            case Unit.UnitTier.C:
                unitTierText.text = "C";
                break;
            case Unit.UnitTier.D:
                unitTierText.text = "D";
                break;
        }
    }
}