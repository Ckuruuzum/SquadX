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
        Destroy(gameObject);
    }
}
