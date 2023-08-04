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

    private bool _canAttack = true;
    private float _unitCooldown;
    public override void Enter()
    {
        Debug.Log("AttackEnter");
        //anim.SetTrigger("isAttacking");
        path.canMove = false;
        base.Enter();
    }

    public override void Update()
    {

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

        if (target != null && target.TryGetComponent(out IDamageable damageable) && _canAttack == true)
        {
            damageable.health.Damage(unit.unitBaseDamage);
            anim.SetTrigger("isAttacking");
            _canAttack = false;
            _unitCooldown = unit.unitAttackCooldown;
            CheckTargetStatus();
        }


    }

    private void CheckTargetStatus()
    {
        if (target.GetComponent<AI>().health.isDead)
        {
            target = null;
            nextState = new Chase(npc, anim, target, unit, path, ai);
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
