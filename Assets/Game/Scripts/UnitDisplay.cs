using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    public Unit unit;

    public float unitDamage;
    public float unitHealth;
    public int unitLevel;
    public int unitExperience;

    [Space]

    public Image cardIconImage;
    public Image cardCardBack;
    public Image cardbottomGlow;
    public TextMeshProUGUI cardTierText;
    public Image cardTierTextGlow;
    public Image cardBottomBar;

    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitLevelText;
    public Slider unitExperienceAmount;
    public List<GameObject> StarsOn;



    private void OnEnable()
    {
        SetCardValues();
    }


    public void SetCardValues()
    {
        gameObject.name = unit.name;
        unitNameText.text = unit.unitName;
        cardIconImage.sprite = unit.unitIcon;
        unitLevelText.text = "LEVEL" + "<size=50> " + unit.unitLevel;

        switch (unit.unitRace)
        {
            case Unit.UnitRace.HUMAN:
                cardCardBack.sprite = unit.unitCardDatabase.cardBackHuman;
                cardbottomGlow.color = unit.unitCardDatabase.bottomGlowHuman;
                cardTierText.color = unit.unitCardDatabase.tierTextColorHuman;
                cardTierTextGlow.color = unit.unitCardDatabase.tierTextGlowHuman;
                cardBottomBar.color = unit.unitCardDatabase.bottomBarColorHuman;
                break;

            case Unit.UnitRace.DEMON:
                cardCardBack.sprite = unit.unitCardDatabase.cardBackDemon;
                cardbottomGlow.color = unit.unitCardDatabase.bottomGlowDemon;
                cardTierText.color = unit.unitCardDatabase.tierTextColorDemon;
                cardTierTextGlow.color = unit.unitCardDatabase.tierTextGlowDemon;
                cardBottomBar.color = unit.unitCardDatabase.bottomBarColorDemon;
                break;

            case Unit.UnitRace.ZOMBIE:
                cardCardBack.sprite = unit.unitCardDatabase.cardBackZombie;
                cardbottomGlow.color = unit.unitCardDatabase.bottomGlowZombie;
                cardTierText.color = unit.unitCardDatabase.tierTextColorZombie;
                cardTierTextGlow.color = unit.unitCardDatabase.tierTextGlowZombie;
                cardBottomBar.color = unit.unitCardDatabase.bottomBarColorZombie;
                break;
        }

        switch (unit.unitTier)
        {
            case Unit.UnitTier.S:
                cardTierText.text = "S";
                break;
            case Unit.UnitTier.A:
                cardTierText.text = "A";
                break;
            case Unit.UnitTier.B:
                cardTierText.text = "B";
                break;
            case Unit.UnitTier.C:
                cardTierText.text = "C";
                break;
            case Unit.UnitTier.D:
                cardTierText.text = "D";
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
