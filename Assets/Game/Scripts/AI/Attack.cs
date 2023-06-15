using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public Attack(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        Debug.Log("AttackEnter");
        anim.SetTrigger("isAttacking");
        path.canMove = false;
        base.Enter();
    }

    public override void Update()
    {

        Debug.LogError(path.remainingDistance);
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnitManager.instance.enemyUnits.Remove(target.gameObject);
            target = null;
            nextState = new Idle(npc, anim, target, unit, path, ai);
            stage = EVENT.EXIT;

        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        Debug.Log("AttackExit");
        path.canMove = true;
        base.Exit();
    }
}
