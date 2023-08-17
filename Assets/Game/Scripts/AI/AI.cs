using Pathfinding;
using RootMotion.Dynamics;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AI : MonoBehaviour, IDamageable
{
    public Unit unit;
    public Collider[] hitColliders = new Collider[10];
    public LayerMask layerMask;
    public TEAM team;
    private Animator _anim;
    private State _currentState;
    private AIPath _path;
    private AIDestinationSetter _destinationSetter;
    private Health _health;
    public Mana mana;
    public PuppetMaster puppetMaster;

    public Health Health => _health;


    private void Start()
    {
        _anim = GetComponent<Animator>();
        _path = GetComponent<AIPath>();
        _health = GetComponent<Health>();
        mana = GetComponent<Mana>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        //SetUnit(_unit, 1, 8);
        SetStateIdle();
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

    public void SetUnit(Unit unit, int teamIndex, int layer, string _layerMask)
    {
        this.unit = unit;
        team = (TEAM)teamIndex;
        gameObject.layer = layer;
        layerMask = LayerMask.GetMask(_layerMask);
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
        _currentState = new Dead(gameObject, _anim, _destinationSetter.target, unit, _path, this, puppetMaster);
    }

    public void SetStateSkill()
    {
        _currentState = new Skill(gameObject, _anim, _destinationSetter.target, unit, _path, this, puppetMaster);
    }

    public void SetStateIdle()
    {
        _currentState = new Idle(gameObject, _anim, _destinationSetter.target, unit, _path, this, puppetMaster);
    }

    public void SetStateChase()
    {
        _currentState = new Chase(gameObject, _anim, _destinationSetter.target, unit, _path, this, puppetMaster);
    }
}
