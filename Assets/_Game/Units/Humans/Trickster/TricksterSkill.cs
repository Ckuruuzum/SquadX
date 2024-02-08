using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterSkill : Spell
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
        _target.GetComponent<AI>().Health.ReceiveDamage(50);
        GetComponent<Mana>().currentMana = 0;
    }
    public void AE_ToggleRootMotion()
    {
        Animator anim = GetComponent<Animator>();
        anim.applyRootMotion = !anim.applyRootMotion;
    }
}
