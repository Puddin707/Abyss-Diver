using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Slime_MoveState : MoveState
{
    private Green_Slime enemy;
    public Green_Slime_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Green_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();

    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange) {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if(isDetectingWall || !isDetectingLedge) {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
