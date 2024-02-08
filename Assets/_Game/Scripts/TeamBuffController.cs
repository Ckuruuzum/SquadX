using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBuffController : MonoBehaviour
{
    [Header("Undead")]
    public int undeadRequiredCount;
    [SerializeField] private Unit _tourmentedSoul;
    [SerializeField] private int undeadAllyCount;
    public bool isUndeadAllyBuffActive;
    [SerializeField] private int undeadEnemyCount;
    public bool isUndeadEnemyBuffActive;

    public void CheckForActivateTeamBuff(Unit unit, UnitManager.TEAM team)
    {
        switch (team)
        {
            case UnitManager.TEAM.Ally:
                switch (unit.unitRace)
                {
                    case Unit.UnitRace.HUMAN:
                        break;
                    case Unit.UnitRace.DEMON:
                        break;
                    case Unit.UnitRace.ZOMBIE:
                        break;
                    case Unit.UnitRace.UNDEAD:
                        undeadAllyCount++;
                        if (undeadAllyCount >= undeadRequiredCount)
                        {
                            isUndeadAllyBuffActive = true;
                        }
                        break;
                }
                break;
            case UnitManager.TEAM.Enemy:
                switch (unit.unitRace)
                {
                    case Unit.UnitRace.HUMAN:
                        break;
                    case Unit.UnitRace.DEMON:
                        break;
                    case Unit.UnitRace.ZOMBIE:
                        break;
                    case Unit.UnitRace.UNDEAD:
                        undeadEnemyCount++;
                        if (undeadEnemyCount >= undeadRequiredCount)
                        {
                            isUndeadEnemyBuffActive = true;
                        }
                        break;
                }
                break;
        }


    }

    public void CheckForDeactivateTeamBuff(Unit unit, UnitManager.TEAM team)
    {
        switch (team)
        {
            case UnitManager.TEAM.Ally:
                switch (unit.unitRace)
                {
                    case Unit.UnitRace.HUMAN:
                        break;
                    case Unit.UnitRace.DEMON:
                        break;
                    case Unit.UnitRace.ZOMBIE:
                        break;
                    case Unit.UnitRace.UNDEAD:
                        undeadAllyCount--;
                        if (undeadAllyCount < undeadRequiredCount)
                        {
                            isUndeadAllyBuffActive = false;
                        }
                        break;
                }
                break;
            case UnitManager.TEAM.Enemy:
                switch (unit.unitRace)
                {
                    case Unit.UnitRace.HUMAN:
                        break;
                    case Unit.UnitRace.DEMON:
                        break;
                    case Unit.UnitRace.ZOMBIE:
                        break;
                    case Unit.UnitRace.UNDEAD:
                        undeadEnemyCount--;
                        if (undeadEnemyCount < undeadRequiredCount)
                        {
                            isUndeadEnemyBuffActive = false;
                        }
                        break;
                }
                break;
        }
    }

    public void UndeadBuff(UnitManager.TEAM team, Transform unitPos)
    {
        if (unitPos.GetChild(2).GetComponent<AI>().unit == _tourmentedSoul) return;

        switch (team)
        {
            case UnitManager.TEAM.Ally:
                if (isUndeadAllyBuffActive)
                {
                    GameObject tmpUnit = Instantiate(_tourmentedSoul.unitPrefab, unitPos.GetChild(2).position, Quaternion.identity);
                    tmpUnit.transform.GetChild(2).GetComponent<AI>().SetUnit(_tourmentedSoul, 1, 8, "EnemyUnit", new Color32(0, 255, 0, 255));
                }
                break;
            case UnitManager.TEAM.Enemy:
                if (isUndeadEnemyBuffActive)
                {
                    GameObject tmpUnitEnemy = Instantiate(_tourmentedSoul.unitPrefab, unitPos.GetChild(2).position, Quaternion.identity);
                    tmpUnitEnemy.transform.GetChild(2).GetComponent<AI>().SetUnit(_tourmentedSoul, 2, 7, "AllyUnit", new Color32(255, 0, 0, 255));
                }
                break;
        }
    }

}
