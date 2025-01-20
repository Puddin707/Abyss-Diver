using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleen_Slime_RangeAttackState : RangeAttackState
{
    private Bleen_Slime enemy;
    public Bleen_Slime_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Bleen_Slime enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        if (isAnimationFinished) {
            if (isPlayerInMinAgroRange) {
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
