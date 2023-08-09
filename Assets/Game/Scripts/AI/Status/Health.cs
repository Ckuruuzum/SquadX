using Pathfinding;
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

    private void Start()
    {
        
        _unit = GetComponent<AI>().unit;

        SetMaxHealth();
    }
    public void Damage(float amount)
    {
        currentHealth -= amount;
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

    public void DestroyNPC()
    {
        Destroy(gameObject.transform.root.gameObject);
    }
    public void AE_Damage()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        if (_target != null && _target.TryGetComponent(out IDamageable damageable))
        {
            //Debug.Log("This GO: " + gameObject.name + "Target: " + _target.gameObject.name + "Damagable: " + damageable.health.gameObject.name);
            damageable.health.Damage(_target.gameObject.GetComponent<AI>().unit.unitBaseDamage);
            GetComponent<Mana>().IncreaseMana(_unit.unitBaseDamage * 2);
        }

    }
}
