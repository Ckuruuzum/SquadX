using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public TEAM team;
    [Header("Player")]
    public int ally_Level = 3;
    public float ally_Experience;
    [Space]
    [Header("Enemy")]
    public int enemy_Level = 3;
    public float enemy_Experience;
    private int maxLevelValue = 10;
    [SerializeField] private List<int> RequiredExpToLevelUp;



    public void GainExperience(float amount, TEAM team)
    {
        if (team == TEAM.Ally)
        {
            ally_Experience = amount + ally_Experience;
            if (ally_Experience >= RequiredExpToLevelUp[ally_Level-3])
            {
                GainLevel(TEAM.Ally);
                ally_Experience = 0;
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

    [Serializable]
    public enum TEAM
    {
        Default = 0, Ally = 1, Enemy = 2
    }
}
