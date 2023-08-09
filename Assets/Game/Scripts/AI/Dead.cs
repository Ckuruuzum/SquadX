using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    public Dead(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai,PuppetMaster puppetMaster)
         : base(npc, anim, target, unit, path, ai, puppetMaster)
    {
        name = STATE.DEAD;
    }

    private float animationLength = 10;
    private bool timeAcquired = false;
    public override void Enter()
    {
        //Debug.Log("DeadEnter");
        anim.SetTrigger("isDead");
        puppetMaster.state = PuppetMaster.State.Dead;
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
            animationLength = GetAnimationLenght();
            //Debug.Log(animationLength + " Dead");
        }

        if (animationLength >= 0)
        {
            animationLength -= Time.deltaTime;
            if (animationLength < 0)
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
