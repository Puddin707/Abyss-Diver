using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_HurtState : HurtState
{
    private Enemy1 enemy;
    public E1_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
