using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path)
        : base(npc, anim, target, unit, path)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        path.canMove = false;
        base.Enter();

    }

    public override void Update()
    {
        Debug.Log(target);
        //if (Random.Range(0, 10000) < 10)
        //{
        //    nextState = new Chase(npc, anim, target, unit, path);
        //    stage = EVENT.EXIT;
        //}
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        Debug.Log("IDLEExit");
        base.Exit();
    }
}
