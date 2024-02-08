using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    [SerializeField] private StaminaHandler _staminaHandler;
    [SerializeField] private DeckHandler _deckHandler;
    [SerializeField] private TeamBuffController _teamBuffController;
    public LevelHandler levelHandler;

    public TEAM team;
    [SerializeField] private Transform _allyUnitHolder;
    [SerializeField] private Transform _enemyUnitHolder;

    public List<GameObject> allyUnits = new List<GameObject>();
    public List<GameObject> enemyUnits = new List<GameObject>();
    public List<Transform> allySpawnPoints = new List<Transform>();
    public List<Transform> enemySpawnPoints = new List<Transform>();

    [SerializeField] private TextMeshProUGUI ally_unitCountText;
    private void Awake()
    {
        instance = this;
    }



    public void SpawnUnit(Unit unit, TEAM team, GameObject cardGo, int spawnPointIndex)
    {
        switch (team)
        {
            case TEAM.Ally:
                if (_staminaHandler.CheckStamina(_staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost) && allyUnits.Count < levelHandler.ally_Level)
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, new Vector3(allySpawnPoints[spawnPointIndex].position.x, allySpawnPoints[spawnPointIndex].position.y, allySpawnPoints[spawnPointIndex].position.z + Random.Range(-2, 2)), Quaternion.identity, _allyUnitHolder);
                    allyUnits.Add(tmpUnit);
                    tmpUnit.transform.GetChild(2).GetComponent<AI>().SetUnit(unit, 1, 8, "EnemyUnit", new Color32(0,255,0,255));
                    _deckHandler.OrganiseDeck(unit, cardGo);
                    _staminaHandler.DecreaseStaminaValue(_staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost, TEAM.Ally);
                    SetCountText(allyUnits);
                    _teamBuffController.CheckForActivateTeamBuff(unit, TEAM.Ally);

                }
                else
                {
                    cardGo.GetComponent<GameCardInteraction>().SendDefaultCardPosition(cardGo);
                }
                break;
            case TEAM.Enemy:
                if (unit.unitPrefab != null && _staminaHandler.CheckStamina(_staminaHandler.GetEnemyStaminaValue(), unit.unitStaminaCost) && enemyUnits.Count < levelHandler.enemy_Level)
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, new Vector3(enemySpawnPoints[spawnPointIndex].position.x, enemySpawnPoints[spawnPointIndex].position.y, enemySpawnPoints[spawnPointIndex].position.z + Random.Range(-2, 2)), Quaternion.identity, _enemyUnitHolder);
                    enemyUnits.Add(tmpUnit);
                    tmpUnit.transform.GetChild(2).GetComponent<AI>().SetUnit(unit, 2, 7, "AllyUnit", new Color32(255, 0, 0, 255));
                    _staminaHandler.DecreaseStaminaValue(_staminaHandler.GetPlayerStaminaValue(), unit.unitStaminaCost, TEAM.Enemy);
                    _teamBuffController.CheckForActivateTeamBuff(unit, TEAM.Enemy);
                }
                break;
        }
    }

    public void RemoveUnit(List<GameObject> list, GameObject unitGameobject, TEAM team, Unit unit)
    {
        if (team == TEAM.Ally)
        {
            _teamBuffController.UndeadBuff(TEAM.Ally, unitGameobject.transform);
            levelHandler.GainExperience(1, LevelHandler.TEAM.Enemy);
            _teamBuffController.CheckForDeactivateTeamBuff(unit, TEAM.Ally);
            SetCountText(allyUnits);
        }
        else if (team == TEAM.Enemy)
        {
            _teamBuffController.UndeadBuff(TEAM.Enemy, unitGameobject.transform);
            levelHandler.GainExperience(1, LevelHandler.TEAM.Ally);
            _teamBuffController.CheckForDeactivateTeamBuff(unit, TEAM.Enemy);
        }
        list.Remove(unitGameobject);

    }

    public void SetCountText(List<GameObject> teamList)
    {
        ally_unitCountText.text = teamList.Count.ToString();
    }

    [Serializable]
    public enum TEAM
    {
        Ally = 0,
        Enemy = 1,
    }
}
