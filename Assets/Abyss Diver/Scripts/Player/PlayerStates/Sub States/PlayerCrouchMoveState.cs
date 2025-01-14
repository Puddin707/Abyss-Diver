using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();

        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit() {
        base.Exit();

        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);
        
        core.Movement.SetVelocityX(playerData.crouchMovementVelocity * core.Movement.FacingDirection);

        if (!isExitingState) {
            if(xInput == 0f) {
               stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (yInput != -1 && !isTouchingCeiling) {
               stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
