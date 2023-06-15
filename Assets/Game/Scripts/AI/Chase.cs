using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnitManager;

public class Chase : State
{
    public Chase(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
        : base(npc, anim, target, unit, path, ai)
    {
        name = STATE.CHASE;
    }

    private float dist;

    public override void Enter()
    {
        anim.SetTrigger("isChasing");
        Debug.Log("ChaseEnter");
        path.canMove = true;
        base.Enter();
    }

    public override void Update()
    {
        //Debug.Log(npc.GetComponent<AIDestinationSetter>().target);


        if (DistanceBetweenTarget() < 1 && ai.GetComponent<AIDestinationSetter>().target != null)
        {
            Debug.LogWarning(path.remainingDistance);
            nextState = new Attack(npc, anim, target, unit, path, ai);
            stage = EVENT.EXIT;
        }
        else
        {
            FindClosestGameObject();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isChasing");
        Debug.Log("ChaseExit");
        base.Exit();
    }


    private Transform FindClosestGameObject()
    {
        GameObject[] enemyUnits = instance.enemyUnits.ToArray();
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject units in enemyUnits)
        {
            float distance = Vector3.Distance(npc.transform.position, units.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = units;
            }

        }
        target = closestObject.transform;
        ai.GetComponent<AIDestinationSetter>().target = target;
        //Debug.LogWarning(target);
        return target;
    }

    private float DistanceBetweenTarget()
    {
        if (target != null)
        {
            dist = Vector3.Distance(npc.transform.position, target.position);
            Debug.Log(dist);
            return dist;
        }
        return 100;
    }
}
