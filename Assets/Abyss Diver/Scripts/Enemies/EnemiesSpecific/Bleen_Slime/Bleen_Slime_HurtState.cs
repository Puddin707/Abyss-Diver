using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleen_Slime_HurtState : HurtState
{
    private Bleen_Slime enemy;
    public Bleen_Slime_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Bleen_Slime enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (!isPlayerInMinAgroRange) {
                enemy.lookForPlayerState.SetFlipImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
            else {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }   
        }
        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
