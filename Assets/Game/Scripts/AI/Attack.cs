using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public Attack(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai, PuppetMaster puppetMaster)
        : base(npc, anim, target, unit, path, ai, puppetMaster)
    {
        name = STATE.ATTACK;
    }

    private bool _canAttack = true;
    private float _unitCooldown;
    private bool timeAcquired = false;
    private float animationLength = 10;
    private float refreshTimer = 00.25f;
    public override void Enter()
    {
        //Debug.Log("AttackEnter");
        //anim.SetTrigger("isAttacking");
        path.canMove = false;
        base.Enter();
    }

    public override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !timeAcquired)
        {
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"));
            timeAcquired = true;
            animationLength = GetAnimationLenght();
            //Debug.Log(animationLength + " skill");
        }


        if (_unitCooldown > 0)
        {
            _unitCooldown -= Time.deltaTime;
            if (_unitCooldown < 0)
            {
                _canAttack = true;
            }
        }

        if (target == null)
        {
            nextState = new Chase(npc, anim, target, unit, path, ai, puppetMaster);
            stage = EVENT.EXIT;
        }

        if (target != null && target.TryGetComponent(out IDamageable damageable) && _canAttack == true && !target.GetComponent<AI>().health.isDead)
        {
            anim.SetTrigger("isAttacking");
            _canAttack = false;
            _unitCooldown = unit.unitAttackCooldown;
            
        }

        if (refreshTimer >= 0)
        {
            refreshTimer -= Time.deltaTime;
            if (refreshTimer < 0)
            {
                refreshTimer = 1f;
                CheckTargetStatus();
            }
        }


    }

    private void CheckTargetStatus()
    {
        if (target.GetComponent<AI>().health.isDead)
        {
            target = null;
            nextState = new Chase(npc, anim, target, unit, path, ai, puppetMaster);
            stage = EVENT.EXIT;
        }
        else if (ai.mana.currentMana >= ai.mana.maxMana)
        {
            nextState = new Skill(npc, anim, target, unit, path, ai, puppetMaster);
            stage = EVENT.EXIT;
        }
    }


    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        //Debug.Log("AttackExit");
        path.canMove = true;
        base.Exit();
    }

    private float GetAnimationLenght()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        float oldLength = state.length;
        return oldLength;
    }
}
