using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [Header("Player")]
    public int player_Level = 3;
    public float player_Experience;
    [Space]
    [Header("Enemy")]
    public int enemy_Level = 3;
    public float enemy_Experience;
    private int maxLevelValue = 10;

    public void GainExperience(float amount, float experience)
    {
        experience = amount + experience;
    }

    public void GainLevel(int level)
    {
        level = level + 1;
        if (level > maxLevelValue)
        {
            level = maxLevelValue;
        }
    }
}
