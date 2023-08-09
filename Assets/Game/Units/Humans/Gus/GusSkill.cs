using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusSkill : Spell
{
    private Transform _target;

    private void Start()
    {
       
    }
    public override void CastSkill()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        _target.GetComponent<AI>().health.Damage(50);
        GetComponent<Mana>().currentMana = 0;
    }
}
