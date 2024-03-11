using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.IDLE;
    }

    protected override void Enter()
    {
        anim.SetTrigger("isIdle");
        //Debug.Log("IdleEnter");
        path.canMove = false;
        base.Enter();

    }

    protected override void Update()
    {
        if (target != null) return;
        nextState = new Chase(npc, anim, target, unit, path, ai);
        stage = EVENT.EXIT;
    }

    protected override void Exit()
    {
        anim.ResetTrigger("isIdle");
        //Debug.Log("IdleExit");
        base.Exit();
    }
}
