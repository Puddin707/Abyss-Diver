using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_Slime_ChargeState : ChargeState
{
    private Spiked_Slime enemy;
    public Spiked_Slime_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Spiked_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performCloseRangeAction) {
                stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall) {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver) {
            if(isPlayerInMinAgroRange) {
                stateMachine.ChangeState(enemy.playerDetectedState);
            } else {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
