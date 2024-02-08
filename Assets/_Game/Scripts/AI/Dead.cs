using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    public Dead(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai,
        PuppetMaster puppetMaster)
        : base(npc, anim, target, unit, path, ai, puppetMaster)
    {
        name = STATE.DEAD;
    }

    public override void Enter()
    {
        //Debug.Log(npc.name + "DeadEnter");
        //anim.SetTrigger("isDead");
        puppetMaster.state = PuppetMaster.State.Dead;
        path.canMove = false;
        base.Enter();
        ai.Health.DestroyRootGo(2);
    }
}