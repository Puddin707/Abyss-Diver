using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_Slime_LookForPlayerState : LookForPlayerState
{
    private Spiked_Slime enemy;
    public Spiked_Slime_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Spiked_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange) {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsDone) {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
