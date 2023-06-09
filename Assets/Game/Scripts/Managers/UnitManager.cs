using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    [SerializeField] private StaminaHandler staminaHandler;

    public TEAM team;
    public Transform allyUnitHolder;
    public Transform enemyUnitHolder;

    public List<GameObject> allyUnits = new List<GameObject>();
    public List<GameObject> enemyUnits = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }


    public void SpawnUnit(Unit unit, TEAM team)
    {
        switch (team)
        {
            case TEAM.Ally:
                if (/*unit.unitPrefab is not null &&*/ staminaHandler.CheckStamina(staminaHandler.GetPlayerStaminaValue(), unit.staminaCost))
                {
                    //GameObject tmpUnit = Instantiate(unit.unitPrefab, allyUnitHolder);
                    //allyUnits.Add(tmpUnit);
                    staminaHandler.DecreaseStaminaValue(staminaHandler.GetPlayerStaminaValue(), unit.staminaCost, TEAM.Ally);
                }
                break;
            case TEAM.Enemy:
                if (unit.unitPrefab is not null && staminaHandler.CheckStamina(staminaHandler.GetEnemyStaminaValue(), unit.staminaCost))
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, enemyUnitHolder);
                    enemyUnits.Add(tmpUnit);
                    staminaHandler.DecreaseStaminaValue(staminaHandler.GetPlayerStaminaValue(), unit.staminaCost, TEAM.Enemy);
                }
                break;
        }


    }

    [Serializable]
    public enum TEAM
    {
        Ally = 0,
        Enemy = 1
    }
}
