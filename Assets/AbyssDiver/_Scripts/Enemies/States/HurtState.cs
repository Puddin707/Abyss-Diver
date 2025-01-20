using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected D_HurtState stateData;
    protected bool isAnimationFinished;
    protected float timeInState;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    public HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HurtState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMinAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter() {
        base.Enter();
        isAnimationFinished = false;
        timeInState = 0f;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        timeInState += Time.deltaTime;

        if (timeInState >= stateData.animationDuration) {
            isAnimationFinished = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

}
