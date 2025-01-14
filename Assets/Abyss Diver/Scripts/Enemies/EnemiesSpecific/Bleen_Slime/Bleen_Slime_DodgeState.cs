using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleen_Slime_DodgeState : DodgeState
{
    private Bleen_Slime enemy;
    public Bleen_Slime_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Bleen_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isDodgeOver) {
            if (isPlayerInMaxAgroRange && performCloseRangeAction) {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction) {
                stateMachine.ChangeState(enemy.rangeAttackState);
            }
            else if (!isPlayerInMaxAgroRange) {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
