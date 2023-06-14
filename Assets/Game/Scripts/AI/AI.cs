using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TEAM _team;
    private Animator _anim;
    [SerializeField] private Transform _target;
    private State _currentState;
    private Unit _unit;
    private AIPath _path;
    private UnitManager _unitManager;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _path = GetComponent<AIPath>();
        _unitManager = UnitManager.instance;
        _currentState = new Idle(gameObject, _anim, _target, _unit, _path);
    }

    private void Update()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        _currentState = _currentState.Process();
    }


    public Transform GetTarget()
    {
        switch (_team)
        {
            case TEAM.ALLY:
                break;
            case TEAM.ENEMY:
                break;
        }
        return _target;
    }

    private TEAM GetTeam()
    {
        if (_unitManager.allyUnits.Contains(gameObject))
        {
            Debug.LogWarning("Team ALLY");
            _team = TEAM.ALLY;
            return TEAM.ALLY;
        }
        else
        {
            Debug.LogWarning("Team ENEMY");
            _team = TEAM.ENEMY;
            return TEAM.ENEMY;
        }
    }

    private Transform GetClosestTarget()
    {
        switch (_team)
        {
            case TEAM.ALLY:
                if (_target != null)
                {
                    for (int i = 0; i < _unitManager.enemyUnits.Count; i++)
                    {
                        if (Vector3.Distance(gameObject.transform.position, _unitManager.enemyUnits[i].transform.position) < Vector3.Distance(gameObject.transform.position, _target.position))
                        {
                            _target = _unitManager.enemyUnits[i].transform;
                            return _target;
                        }
                    }
                }
                return _target;

            case TEAM.ENEMY:
                if (_target != null)
                {
                    for (int i = 0; i < _unitManager.enemyUnits.Count; i++)
                    {
                        if (Vector3.Distance(gameObject.transform.position, _unitManager.allyUnits[i].transform.position) < Vector3.Distance(gameObject.transform.position, _target.position))
                        {
                            _target = _unitManager.enemyUnits[i].transform;
                            return _target;
                        }
                    }
                }
                return _target;
        }
        return null;
    }



    private enum TEAM
    {
        ALLY, ENEMY
    }
}
