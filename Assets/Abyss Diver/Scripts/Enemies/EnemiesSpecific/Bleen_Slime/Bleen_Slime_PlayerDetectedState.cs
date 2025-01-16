using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleen_Slime_PlayerDetectedState : PlayerDetectedState
{
    private Bleen_Slime enemy;
    public Bleen_Slime_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Bleen_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown) {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
            stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if (performLongRangeAction && performMidRangeAction) {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if (performLongRangeAction) {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if(!isPlayerInMaxAgroRange) {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if(!isDetectingLedge) {
            Movement?.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
