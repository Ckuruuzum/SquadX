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

    private float _destroyTimer = 5;

    public override void Enter()
    {
        Debug.Log("DeadEnter");
        anim.SetTrigger("isDead");
        path.canMove = false;
        base.Enter();
    }

    public override void Update()
    {
        if (_destroyTimer > 0)
        {
            _destroyTimer -= Time.deltaTime;
            if (_destroyTimer < 0)
            {
                ai.health.DestroyNPC();
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isDead");
        Debug.Log("DeadExit");
        base.Exit();
    }
}
