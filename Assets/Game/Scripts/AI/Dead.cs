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

    private float animationCooldown = 10;
    private bool timeAcquired = false;
    public override void Enter()
    {
        //Debug.Log("DeadEnter");
        anim.SetTrigger("isDead");
        //Debug.Log(animationCooldown + " asdasdasdasdas");
        path.canMove = false;
        base.Enter();
    }

    public override void Update()
    {
        //Debug.Log(GetAnimationLenght());
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") && !timeAcquired)
        {
            
            timeAcquired = true;
            animationCooldown = GetAnimationLenght();
            Debug.Log(animationCooldown + " Dead");
        }

        if (animationCooldown >= 0)
        {
            animationCooldown -= Time.deltaTime;
            if (animationCooldown < 0)
            {
                ai.health.DestroyNPC();
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isDead");
        //Debug.Log("DeadExit");
        base.Exit();
    }

    private float GetAnimationLenght()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        float oldLength = state.length;
        return oldLength;
    }
}
