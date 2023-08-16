using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDestroyed;
    private Base _base;

    private void Start()
    {
        _base = GetComponent<BaseController>().baseSO;
        SetMaxHealth();
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destruct();
        }
    }
    private void SetMaxHealth()
    {
        maxHealth = _base.baseHealth;
        currentHealth = maxHealth;
    }

    private void Destruct()
    {
        isDestroyed = true;
        Destroy(gameObject.GetComponent<Collider>());
        if (GetComponent<BaseController>().team == BaseController.TEAM.ALLY)
        {
            UnitManager.instance.allyUnits.Remove(gameObject);
        }
        else if (GetComponent<BaseController>().team == BaseController.TEAM.ENEMY)
        {
            UnitManager.instance.enemyUnits.Remove(gameObject);
        }
        Destroy(gameObject, 2);
    }

    public void DestroyRootGo()
    {
        Destroy(gameObject.transform.root.gameObject);
    }
}
