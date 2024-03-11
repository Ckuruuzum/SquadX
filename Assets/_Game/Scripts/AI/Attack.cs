using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attack : State
{
    public Attack(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.ATTACK;
    }

    private bool _canAttack = true;
    private float _unitCooldown;
    private bool _timeAcquired;
    private float _animationLength = 10;

    protected override void Enter()
    {
        //Debug.Log("AttackEnter");
        //anim.SetTrigger("isAttacking");
        path.canMove = false;
        base.Enter();
    }

    protected override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !_timeAcquired)
        {
            _timeAcquired = true;
            _animationLength = GetAnimationLenght();
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
            nextState = new Chase(npc, anim, target, unit, path, ai);
            stage = EVENT.EXIT;
        }
        else
        {
            FaceTarget();
            if (_canAttack)
            {
                if (target.TryGetComponent(out IDamageable damageable) && !target.GetComponent<AI>().Health.isDead)
                {
                    anim.SetTrigger("isAttacking");
                    _canAttack = false;
                    _unitCooldown = unit.unitAttackCooldown;
                }
                else if (target.TryGetComponent(out IDestructable destructable) && !target.GetComponent<BaseController>().Health.isDestroyed)
                {
                    anim.SetTrigger("isAttacking");
                    _canAttack = false;
                    _unitCooldown = unit.unitAttackCooldown;
                }
            }
        }
    }

    private void FaceTarget()
    {
        var position = target.position;
        npc.transform.LookAt(new Vector3(position.x, npc.transform.position.y, position.z));
    }

    protected override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        //Debug.Log("AttackExit");
        path.canMove = true;
        base.Exit();
    }

    private float GetAnimationLenght()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        var oldLength = state.length;
        return oldLength;
    }
}
