using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public TEAM team;
    [Header("Player")]
    public int ally_Level = 3;
    public float ally_Experience;
    public TextMeshProUGUI ally_LevelText;
    [SerializeField] private Slider ally_Exp_Slider;
    [Space]
    [Header("Enemy")]
    public int enemy_Level = 3;
    public float enemy_Experience;
    private int maxLevelValue = 10;
    [SerializeField] private List<int> RequiredExpToLevelUp;

    private void Start()
    {
        SetSliderMaxValue(RequiredExpToLevelUp[ally_Level - 3]);
    }

    public void GainExperience(float amount, TEAM team)
    {
        if (team == TEAM.Ally)
        {
            ally_Experience = amount + ally_Experience;
            SetSliderExp(ally_Experience);

            if (ally_Experience >= RequiredExpToLevelUp[ally_Level-3])
            {
                GainLevel(TEAM.Ally);
                ally_Experience = 0;
                SetSliderExp(ally_Experience);
                
            }
        }
        else if (team == TEAM.Enemy)
        {
            enemy_Experience = amount + enemy_Experience;
            if (enemy_Experience >= RequiredExpToLevelUp[enemy_Level-3])
            {
                GainLevel(TEAM.Enemy);
                enemy_Experience = 0;
            }
        }
    }

    public void GainLevel(TEAM team)
    {
        if (team == TEAM.Ally)
        {
            ally_Level = ally_Level + 1;
            if (ally_Level > maxLevelValue)
            {
                ally_Level = maxLevelValue;
            }
            SetSliderMaxValue(RequiredExpToLevelUp[ally_Level - 3]);
            SetCapacityText();
        }
        else if (team == TEAM.Enemy)
        {
            enemy_Level = enemy_Level + 1;
            if (enemy_Level > maxLevelValue)
            {
                enemy_Level = maxLevelValue;
            }
        }

    }

    private void SetCapacityText()
    {
        ally_LevelText.text = ally_Level.ToString();
    }

    [Serializable]
    public enum TEAM
    {
        Default = 0, Ally = 1, Enemy = 2
    }

    private void SetSliderExp(float amount)
    {
        ally_Exp_Slider.value = amount;
    }
    private void SetSliderMaxValue(float amount)
    {
        ally_Exp_Slider.maxValue = amount;
    }
}
