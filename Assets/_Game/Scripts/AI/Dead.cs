using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    public Dead(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.DEAD;
    }

    protected override void Enter()
    {
        //Debug.Log(npc.name + "DeadEnter");
        //anim.SetTrigger("isDead");
        path.canMove = false;
        base.Enter();
        ai.Health.DestroyRootGo(2);
    }
}