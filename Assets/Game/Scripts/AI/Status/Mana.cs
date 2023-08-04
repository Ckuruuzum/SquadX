using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<AI>().unit;

        SetMaxMana();
    }

    private void SetMaxMana()
    {
        maxMana = _unit.unitBaseMana;
        currentMana = 0;
    }

    public void ConsumeMana(float amount)
    {
        currentMana -= amount;
    }

    public void RegenMana(float amount)
    {
        currentMana += amount;
    }
}
