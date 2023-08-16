using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSkill : Spell
{
    private Transform _target;
    private Unit _unit;
    private void Start()
    {
        _unit = GetComponent<AI>().unit;
    }
    public override void AE_CastSkill()
    {
        _target = GetComponent<AIDestinationSetter>().target;
        _target.GetComponent<AI>().Health.Damage(25);
        GetComponent<Mana>().currentMana = 0;
        CheckTargetStatus();
    }

    private void CheckTargetStatus()
    {
        if (_target.GetComponent<AI>().Health.isDead)
        {
            _target = null;
            GetComponent<AI>().SetStateChase();
        }
    }
}
