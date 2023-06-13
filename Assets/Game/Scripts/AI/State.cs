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

    public State state;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform target;
    protected Unit unit;
    protected State nextState;
    public State(GameObject npc, Animator anim, Transform target, Unit unit)
    {
        this.npc = npc;
        this.anim = anim;
        this.target = target;
        this.unit = unit;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

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
