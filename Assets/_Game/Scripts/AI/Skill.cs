using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : State
{
    public Skill(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.SKILL;
    }

    private float animationLength = 10;
    private bool timeAcquired = false;

    protected override void Enter()
    {
        //Debug.Log("EnteringSkill");
        anim.SetTrigger("isSkill");
        path.canMove = false;
        base.Enter();
    }

    protected override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") && !timeAcquired)
        {
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"));
            timeAcquired = true;
            animationLength = GetAnimationLenght();
            //Debug.Log(animationCooldown + " skill");
        }

        animationLength -= Time.deltaTime;
        if (animationLength < 0)
        {
            if (target == null)
            {
                nextState = new Chase(npc, anim, target, unit, path, ai);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new Attack(npc, anim, target, unit, path, ai);
                stage = EVENT.EXIT;
            }
        }
    }

    protected override void Exit()
    {
        //Debug.Log("ExitingSkill");
        anim.ResetTrigger("isSkill");
        base.Exit();
    }


    private float GetAnimationLenght()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        float oldLength = state.length;
        return oldLength;
    }
}