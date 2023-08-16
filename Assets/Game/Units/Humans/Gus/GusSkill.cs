using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static RootMotion.Demos.CharacterMeleeDemo.Action;
using static State;
using static UnityEngine.GraphicsBuffer;

public class GusSkill : Spell
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
        _target.GetComponent<AI>().Health.Damage(50);
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
