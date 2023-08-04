using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : State
{
    public Skill(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.SKILL;
    }

    public override void Enter()
    {
        anim.SetTrigger("isSkill");
        path.canMove = false;
        base.Enter();
    }
    public override void Update()
    {

    }

    public override void Exit()
    {
        anim.ResetTrigger("isSkill");
        base.Exit();
    }
}
