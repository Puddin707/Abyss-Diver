using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_Slime_IdleState : IdleState
{
    private Spiked_Slime enemy;
    public Spiked_Slime_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Spiked_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isPlayerInMinAgroRange) {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver) {
            stateMachine.ChangeState(enemy.moveState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }
}
