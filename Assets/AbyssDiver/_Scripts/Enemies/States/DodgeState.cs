using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    protected D_DodgeState stateData;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isPlayerInMinAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMinAgroRange();
        isGrounded = CollisionSenses.Ground;
    }

    public override void Enter() {
        base.Enter();

        isDodgeOver = false;

        Movement?.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded) {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
