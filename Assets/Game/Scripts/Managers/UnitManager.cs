using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public TEAM team;
    public Transform allyUnitHolder;
    public Transform enemyUnitHolder;

    public List<GameObject> allyUnits = new List<GameObject>();
    public List<GameObject> enemyUnits = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }


    private void SpawnUnit(Unit unit,TEAM team)
    {
        switch (team)
        {
            case TEAM.Ally:
                if (unit.unitPrefab is not null)
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, allyUnitHolder);
                    allyUnits.Add(tmpUnit);
                }
                break;
            case TEAM.Enemy:
                if (unit.unitPrefab is not null)
                {
                    GameObject tmpUnit = Instantiate(unit.unitPrefab, enemyUnitHolder);
                    enemyUnits.Add(tmpUnit);
                }
                break;
        }

       
    }

    public enum TEAM
    {
        Ally, Enemy
    }
}
