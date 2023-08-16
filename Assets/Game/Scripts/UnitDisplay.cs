using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    public Unit Unit
    {
        get
        {
            return _unit;
        }
        set
        {
            _unit = value;
            SetCardValues();
        }
    }



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
    public Image StaminaBackGround;

    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitLevelText;
    public TextMeshProUGUI unitStaminaText;
    public Slider unitExperienceAmount;
    public List<GameObject> StarsOn;


    private void OnEnable()
    {
        SetCardValues();
    }


    public void SetCardValues()
    {
        if (Unit is null) return;

        gameObject.name = Unit.name;
        unitNameText.text = Unit.unitName;
        cardIconImage.sprite = Unit.unitIcon;
        unitLevelText.text = "LEVEL" + "<size=50> " + Unit.unitLevel;
        unitStaminaText.text = Unit.unitStaminaCost.ToString();
        StaminaBackGround.sprite = Unit.unitCardDatabase.staminaBackGround;

        switch (Unit.unitRace)
        {
            case Unit.UnitRace.HUMAN:
                cardCardBack.sprite = Unit.unitCardDatabase.cardBackHuman;
                cardbottomGlow.color = Unit.unitCardDatabase.bottomGlowHuman;
                cardTierText.color = Unit.unitCardDatabase.tierTextColorHuman;
                cardTierTextGlow.color = Unit.unitCardDatabase.tierTextGlowHuman;
                cardBottomBar.color = Unit.unitCardDatabase.bottomBarColorHuman;
                break;

            case Unit.UnitRace.DEMON:
                cardCardBack.sprite = Unit.unitCardDatabase.cardBackDemon;
                cardbottomGlow.color = Unit.unitCardDatabase.bottomGlowDemon;
                cardTierText.color = Unit.unitCardDatabase.tierTextColorDemon;
                cardTierTextGlow.color = Unit.unitCardDatabase.tierTextGlowDemon;
                cardBottomBar.color = Unit.unitCardDatabase.bottomBarColorDemon;
                break;

            case Unit.UnitRace.ZOMBIE:
                cardCardBack.sprite = Unit.unitCardDatabase.cardBackZombie;
                cardbottomGlow.color = Unit.unitCardDatabase.bottomGlowZombie;
                cardTierText.color = Unit.unitCardDatabase.tierTextColorZombie;
                cardTierTextGlow.color = Unit.unitCardDatabase.tierTextGlowZombie;
                cardBottomBar.color = Unit.unitCardDatabase.bottomBarColorZombie;
                break;
        }

        switch (Unit.unitTier)
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
        switch (Unit.unitStarCount)
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

    public void SetUnitNull()
    {
        Unit = null;
    }

    public void SetUnit(Unit appointedUnit)
    {
        Unit = appointedUnit;
    }

}
