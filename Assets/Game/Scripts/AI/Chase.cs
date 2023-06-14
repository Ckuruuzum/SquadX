using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Chase(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path)
        : base(npc, anim, target, unit, path)
    {
        name = STATE.CHASE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isChasing");
        path.canMove = true;
        base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(npc.transform.position, target.position) < 1)
        {
            Debug.Log("can");
            nextState = new Idle(npc, anim, target, unit, path);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isChasing");
        base.Exit();
    }
}
