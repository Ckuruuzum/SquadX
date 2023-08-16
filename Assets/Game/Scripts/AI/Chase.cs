using Pathfinding;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Chase(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai, PuppetMaster puppetMaster)
        : base(npc, anim, target, unit, path, ai, puppetMaster)
    {
        name = STATE.CHASE;
    }

    private float dist;

    public override void Enter()
    {
        anim.SetTrigger("isChasing");
        //Debug.Log("ChaseEnter");
        path.canMove = true;
        base.Enter();
    }

    public override void Update()
    {
        if (DistanceBetweenTarget() < npc.GetComponent<AIPath>().endReachedDistance && ai.GetComponent<AIDestinationSetter>().target != null)
        {
            nextState = new Attack(npc, anim, target, unit, path, ai, puppetMaster);
            stage = EVENT.EXIT;
        }
        else
        {
            FindTarget();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isChasing");
        //Debug.Log("ChaseExit");
        base.Exit();
    }

    private void FindTarget()
    {
        Collider[] targets = npc.GetComponent<AI>().hitColliders;
        for (int i = 0; i < targets.Length; i++)
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

    private Transform FindClosestTarget()
    {
        if (ai.team == AI.TEAM.ALLY)
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
                    SetTarget(closestTarget.transform);
                    //Debug.LogWarning(target);
                    return target;
                }
            }
            return null;
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
                    SetTarget(closestTarget.transform);
                    //Debug.LogWarning(target);
                    return target;
                }
            }
            return null;
        }
        else
            return null;



    }

    private float DistanceBetweenTarget()
    {
        if (target != null)
        {
            dist = Vector3.Distance(npc.transform.position, target.position);
            return dist;
        }
        return 100;
    }

    private void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        ai.GetComponent<AIDestinationSetter>().target = target;
    }

}
