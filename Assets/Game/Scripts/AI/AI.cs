using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public TEAM team;
    private Animator _anim;
    private State _currentState;
    private Unit _unit;
    private AIPath _path;
    private AIDestinationSetter _destinationSetter;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _path = GetComponent<AIPath>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        _currentState = new Idle(gameObject, _anim, _destinationSetter.target, _unit, _path, this);
        GetTeam();
    }

    private void Update()
    {
        _currentState = _currentState.Process();
    }


    //private Transform GetTarget()
    //{
    //    switch (team)
    //    {
    //        case TEAM.ALLY:
    //            break;
    //        case TEAM.ENEMY:
    //            break;
    //    }
    //    return target;
    //}



    private TEAM GetTeam()
    {
        if (UnitManager.instance.allyUnits.Contains(gameObject))
        {
            Debug.LogWarning("Team ALLY");
            team = TEAM.ALLY;
            return TEAM.ALLY;
        }
        else
        {
            Debug.LogWarning("Team ENEMY");
            team = TEAM.ENEMY;
            return TEAM.ENEMY;
        }
    }

    [Serializable]
    public enum TEAM
    {
        ALLY, ENEMY
    }
}
