using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AI : MonoBehaviour, IDamageable
{
    public Unit unit;
    public Collider[] hitColliders = new Collider[10];
    public LayerMask layerMask;
    public TEAM team;
    public float radius;
    private Animator _anim;
    private State _currentState;
    private AIPath _path;
    private AIDestinationSetter _destinationSetter;
    private Health _health;
    private Mana _mana;

    public Health health => _health;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _path = GetComponent<AIPath>();
        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        //SetUnit(_unit, 1, 8);
        _currentState = new Idle(gameObject, _anim, _destinationSetter.target, unit, _path, this);
    }

    private void Update()
    {
        _currentState = _currentState.Process();
        GetTargetUnits(gameObject.transform.position, 5);
    }

    [Serializable]
    public enum TEAM
    {
        DEFAULT = 0, ALLY = 1, ENEMY = 2
    }

    public void SetUnit(Unit unit, int teamIndex, int layer)
    {
        this.unit = unit;
        team = (TEAM)teamIndex;
        gameObject.layer = layer;

    }

    private void GetTargetUnits(Vector3 center, float radius)
    {
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders, layerMask);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 5);
    }

    public void SetStateDead()
    {
        _currentState = new Dead(gameObject, _anim, _destinationSetter.target, unit, _path, this);
    }
}
