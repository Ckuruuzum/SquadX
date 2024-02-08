using System;
using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDead;
    private Unit _unit;
    private Transform _target;
    private PuppetMaster _puppetMaster;
    private AI _ai;

    private void Start()
    {
        _ai = GetComponent<AI>();
        _unit = _ai.unit;
        _puppetMaster = _ai.puppetMaster;
        SetMaxHealth();
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        AdjustPinWeight();
        if (currentHealth <= 0)
        {
            Kill(_unit);
        }
    }

    public void HealthRegen(float amount)
    {
        currentHealth += amount;
    }

    private void SetMaxHealth()
    {
        maxHealth = _unit.unitBaseHealth;
        currentHealth = maxHealth;
    }

    private void Kill(Unit unit)
    {
        isDead = true;
        Destroy(gameObject.GetComponent<Collider>());
        
        switch (_ai.team)
        {
            case AI.TEAM.ALLY:
                UnitManager.instance.RemoveUnit(UnitManager.instance.allyUnits, gameObject.transform.parent.gameObject,
                    UnitManager.TEAM.Ally, unit);
                break;
            case AI.TEAM.ENEMY:
                UnitManager.instance.RemoveUnit(UnitManager.instance.enemyUnits, gameObject.transform.parent.gameObject,
                    UnitManager.TEAM.Enemy, unit);
                break;
            case AI.TEAM.DEFAULT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _ai.SetStateDead();
    }


    public void DestroyRootGo(float delay)
    {
        Destroy(gameObject.transform.parent.gameObject, delay);
    }

    public void AE_Damage()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        if (_target != null && _target.TryGetComponent(out IDamageable damageable))
        {
            damageable.Health.ReceiveDamage(_unit.unitBaseDamage);
            GetComponent<Mana>().IncreaseMana(_unit.unitBaseDamage * 2);
            CheckMana();
        }
        else if (_target != null && _target.TryGetComponent(out IDestructable destructable))
        {
            destructable.Health.ReceiveDamage(_unit.unitBaseDamage);
            GetComponent<Mana>().IncreaseMana(_unit.unitBaseDamage * 2);
        }
    }

    private void AdjustPinWeight()
    {
        if (_puppetMaster == null) return;

        var tmpRatio = currentHealth / _unit.unitBaseHealth;
        //Debug.Log("Ratio" + tmpRatio);

        var pinWeightRatio = 0.5f * tmpRatio;
        //Debug.Log("pinWeightRatio" + pinWeightRatio);
        var pinWeightAmount = 0.5f - pinWeightRatio;
        //Debug.Log("pinWeightAmount" + pinWeightAmount);
        _puppetMaster.pinWeight = 1 - pinWeightAmount;

        if (_puppetMaster.pinWeight < 0.5f)
        {
            _puppetMaster.pinWeight = 0.5f;
        }
    }

    private void CheckMana()
    {
        if (_ai.mana.currentMana >= _ai.mana.maxMana)
        {
            _ai.SetStateSkill();
        }
    }
}