using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        player.RB.gravityScale = 0;
        player.RB.velocity = Vector2.zero;
    }

    public override void Exit() {
        base.Exit();
        player.RB.gravityScale = 5;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (!isExitingState) {
            if (yInput > 0) {
            stateMachine.ChangeState(player.WallClimbState);
        }
        else if ( yInput < 0 || !grabInput) {
            stateMachine.ChangeState(player.WallSlideState);
        }
        }   
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
