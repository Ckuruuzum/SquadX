using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDead;
    private Unit _unit;
    private Transform _target;
    private PuppetMaster _puppetMaster;

    private void Start()
    {
        _unit = GetComponent<AI>().unit;
        _puppetMaster = GetComponent<AI>().puppetMaster;
        SetMaxHealth();
    }
    public void Damage(float amount)
    {
        currentHealth -= amount;
        AdjustPinWeight();
        if (currentHealth <= 0)
        {
            Kill();
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

    private void Kill()
    {
        isDead = true;
        Destroy(gameObject.GetComponent<Collider>());
        if (GetComponent<AI>().team == AI.TEAM.ALLY)
        {
            UnitManager.instance.allyUnits.Remove(gameObject);
        }
        else if (GetComponent<AI>().team == AI.TEAM.ENEMY)
        {
            UnitManager.instance.enemyUnits.Remove(gameObject);
        }

        GetComponent<AI>().SetStateDead();
    }

    public void DestroyRootGo()
    {
        Destroy(gameObject.transform.root.gameObject);
    }
    public void AE_Damage()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        if (_target != null && _target.TryGetComponent(out IDamageable damageable))
        {
            damageable.Health.Damage(_unit.unitBaseDamage);
            GetComponent<Mana>().IncreaseMana(_unit.unitBaseDamage * 2);
        }
        else if (_target != null && _target.TryGetComponent(out IDestructable destructable))
        {
            destructable.Health.Damage(_unit.unitBaseDamage);
            GetComponent<Mana>().IncreaseMana(_unit.unitBaseDamage * 2);
        }
    }

    private void AdjustPinWeight()
    {
        if (_puppetMaster == null) return;

        float tmpRatio = currentHealth / _unit.unitBaseHealth;
        //Debug.Log("Ratio" + tmpRatio);

        float pinWeightRatio = 0.3f * tmpRatio;
        //Debug.Log("pinWeightRatio" + pinWeightRatio);
        float pinWeightAmount = 0.3f - pinWeightRatio;
        //Debug.Log("pinWeightAmount" + pinWeightAmount);
        _puppetMaster.pinWeight = _puppetMaster.pinWeight - pinWeightAmount;

        if (_puppetMaster.pinWeight < 0.7f)
        {
            _puppetMaster.pinWeight = 0.7f; 
        }
    }
}
