using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    [SerializeField] private StaminaHandler staminaHandler;
    [SerializeField] private DeckHandler deckHandler;

    public TEAM team;
    [SerializeField] private Transform _allyUnitHolder;
    [SerializeField] private Transform _enemyUnitHolder;

    public List<GameObject> allyUnits = new List<GameObject>();
    public List<GameObject> enemyUnits = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }


    public void SpawnUnit(Unit unit, TEAM team, GameObject cardGo)
    {
        switch (team)
        {
            case TEAM.Ally:
                if (staminaHandler.CheckStamina(staminaHandler.GetPlayerStaminaValue(), unit.staminaCost))
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, _allyUnitHolder);
                    allyUnits.Add(tmpUnit);
                    tmpUnit.GetComponent<AI>().SetUnit(unit, 1, 8);
                    deckHandler.OrganiseDeck(unit, cardGo);
                    staminaHandler.DecreaseStaminaValue(staminaHandler.GetPlayerStaminaValue(), unit.staminaCost, TEAM.Ally);
                }
                else
                {
                    cardGo.GetComponent<GameCardInteraction>().SendDefaultCardPosition(cardGo);
                }
                break;
            case TEAM.Enemy:
                if (unit.unitPrefab is not null && staminaHandler.CheckStamina(staminaHandler.GetEnemyStaminaValue(), unit.staminaCost))
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, _enemyUnitHolder);
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
        Enemy = 1,
        ALLY = 2
    }
}
