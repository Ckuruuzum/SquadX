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
    public List<Transform> allySpawnPoints = new List<Transform>();
    public List<Transform> enemySpawnPoints = new List<Transform>();


    private void Awake()
    {
        instance = this;
    }


    public void SpawnUnit(Unit unit, TEAM team, GameObject cardGo, int spawnPointIndex)
    {
        switch (team)
        {
            case TEAM.Ally:
                if (staminaHandler.CheckStamina(staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost))
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, allySpawnPoints[spawnPointIndex].position, Quaternion.identity, _allyUnitHolder);
                    allyUnits.Add(tmpUnit);
                    tmpUnit.transform.GetChild(2).GetComponent<AI>().SetUnit(unit, 1, 8, "EnemyUnit");
                    deckHandler.OrganiseDeck(unit, cardGo);
                    staminaHandler.DecreaseStaminaValue(staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost, TEAM.Ally);
                }
                else
                {
                    cardGo.GetComponent<GameCardInteraction>().SendDefaultCardPosition(cardGo);
                }
                break;
            case TEAM.Enemy:
                if (unit.unitPrefab != null && staminaHandler.CheckStamina(staminaHandler.GetEnemyStaminaValue(), unit.unitStaminaCost))
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, enemySpawnPoints[spawnPointIndex].position, Quaternion.identity, _enemyUnitHolder);
                    enemyUnits.Add(tmpUnit);
                    tmpUnit.transform.GetChild(2).GetComponent<AI>().SetUnit(unit, 2, 7, "AllyUnit");
                    staminaHandler.DecreaseStaminaValue(staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost, TEAM.Enemy);
                }
                break;
        }


    }

    [Serializable]
    public enum TEAM
    {
        Ally = 0,
        Enemy = 1,
    }
}
