using System;
using Pathfinding;
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
    private AI _ai;

    private void Start()
    {
        _ai = GetComponent<AI>();
        _unit = _ai.unit;
        SetMaxHealth();
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Kill(_unit);
        }
    }

    private void SetMaxHealth()
    {
        maxHealth = _unit.unitBaseHealth;
        currentHealth = maxHealth;
    }

    private void Kill(Unit unit)
    {
        isDead = true;
        
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
        Destroy(gameObject.GetComponent<Collider>());
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

    private void CheckMana()
    {
        if (_ai.mana.currentMana >= _ai.mana.maxMana)
        {
            _ai.SetStateSkill();
        }
    }
}