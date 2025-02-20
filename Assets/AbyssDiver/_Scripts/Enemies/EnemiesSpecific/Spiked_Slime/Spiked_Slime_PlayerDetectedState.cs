using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_Slime_PlayerDetectedState : PlayerDetectedState
{
    private Spiked_Slime enemy;

    public Spiked_Slime_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Spiked_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        else if(performLongRangeAction) {
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
