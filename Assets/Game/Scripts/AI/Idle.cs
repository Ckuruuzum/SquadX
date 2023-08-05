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

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        //Debug.Log("IdleEnter");
        path.canMove = false;
        base.Enter();

    }

    public override void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    nextState = new Chase(npc, anim, target, unit, path, ai);
        //    stage = EVENT.EXIT;
        //}
        if (target == null)
        {
            nextState = new Chase(npc, anim, target, unit, path, ai);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        //Debug.Log("IdleExit");
        base.Exit();
    }
}
