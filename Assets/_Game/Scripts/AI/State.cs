using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE, CHASE, ATTACK, DEAD, SKILL
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform target;
    protected Unit unit;
    protected State nextState;
    protected AIPath path;
    protected AI ai;

    protected State(GameObject npc, Animator anim, Transform target, Unit unit, AIPath path, AI ai)
    {
        this.npc = npc;
        this.anim = anim;
        this.target = target;
        this.unit = unit;
        this.path = path;
        this.ai = ai;
        stage = EVENT.ENTER;
    }

    protected virtual void Enter() { stage = EVENT.UPDATE; }
    protected virtual void Update() { stage = EVENT.UPDATE; }
    protected virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
