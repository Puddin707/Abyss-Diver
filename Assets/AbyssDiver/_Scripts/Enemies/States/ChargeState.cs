using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

public class ChargeState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    protected bool performMidRangeAction;
    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMinAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        performMidRangeAction = entity.CheckPlayerInMidRangeAction();
    }

    public override void Enter() {
        base.Enter();
        isChargeTimeOver = false;
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit() {
        base.Exit();

    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime) {
            isChargeTimeOver = true;
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
