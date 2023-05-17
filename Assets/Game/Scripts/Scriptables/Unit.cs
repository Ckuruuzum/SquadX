using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "SquadX/Unit")]
public class Unit : ScriptableObject
{
    [Header("General")]
    public string unitName;
    public string unitDescription;
    public Sprite unitIcon;
    public int unitID;

    [Header("Stats")]
    public UnitType unitType;
    public UnitRace unitRace;
    public float unitBaseDamage;
    public float unitBaseHealth;
    public int unitLevel;
    public int unitExperience;

    [Header("Label")]
    public UnitStarCount unitStarCount;
    public UnitTier unitTier;

    [Header("DataBase")]
    public CardDatabase unitCardDatabase;
    public enum UnitType { MELEE, RANGE }
    public enum UnitTier { S, A, B, C, D }
    public enum UnitStarCount { _1, _2, _3, _4, _5 }
    public enum UnitRace { HUMAN, DEMON, ZOMBIE}


}
