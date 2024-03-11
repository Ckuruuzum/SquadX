using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Chase(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.CHASE;
    }

    private float _dist;

    protected override void Enter()
    {
        anim.SetTrigger("isChasing");
        //Debug.Log("ChaseEnter");
        path.canMove = true;
        base.Enter();
    }

    protected override void Update()
    {
        if (DistanceBetweenTarget() <= npc.GetComponent<AIPath>().endReachedDistance &&
            ai.GetComponent<AIDestinationSetter>().target != null)
        {
            nextState = new Attack(npc, anim, target, unit, path, ai);
            stage = EVENT.EXIT;
        }
        else
        {
            FindTarget();
        }
    }

    protected override void Exit()
    {
        anim.ResetTrigger("isChasing");
        //Debug.Log("ChaseExit");
        base.Exit();
    }

    private void FindTarget()
    {
        Collider[] targets = npc.GetComponent<AI>().hitColliders;
        for (var i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
            {
                FindClosestTarget();
                break;
            }
            else
            {
                if (ai.team == AI.TEAM.ALLY)
                {
                    SetTarget(PlayManager.instance.enemyBase);
                }
                else if (ai.team == AI.TEAM.ENEMY)
                {
                    SetTarget(PlayManager.instance.allyBase);
                }
            }
        }
    }

    private void FindClosestTarget()
    {
        if (ai.team == AI.TEAM.ALLY)
        {
            Collider[] enemyUnits = npc.GetComponent<AI>().hitColliders;
            Collider closestTarget = null;
            const float closestDistance = Mathf.Infinity;

            for (int i = 0; i < enemyUnits.Length; i++)
            {
                if (enemyUnits[i] != null)
                {
                    float distance = Vector3.Distance(npc.transform.position, enemyUnits[i].transform.position);
                    if (distance < closestDistance)
                    {
                        //closestDistance = distance;
                        closestTarget = enemyUnits[i];
                    }

                    if (closestTarget != null) SetTarget(closestTarget.transform);
                    //Debug.LogWarning(target);
                }
            }
        }
        else if (ai.team == AI.TEAM.ENEMY)
        {
            Collider[] enemyUnits = npc.GetComponent<AI>().hitColliders;
            Collider closestTarget = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < enemyUnits.Length; i++)
            {
                if (enemyUnits[i] != null)
                {
                    float distance = Vector3.Distance(npc.transform.position, enemyUnits[i].transform.position);
                    if (distance < closestDistance)
                    {
                        //closestDistance = distance;
                        closestTarget = enemyUnits[i];
                    }

                    if (closestTarget != null) SetTarget(closestTarget.transform);
                }
            }
        }
    }

    private float DistanceBetweenTarget()
    {
        if (target != null)
        {
            _dist = Vector3.Distance(npc.transform.position, target.position);
            return _dist;
        }

        return 100;
    }

    private void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        ai.GetComponent<AIDestinationSetter>().target = target;
    }
}