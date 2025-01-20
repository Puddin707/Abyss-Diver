using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitesState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter() {
        base.Enter();
        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.AirState.SetIsJumping();
    }

    public bool CanJump() {
        if (amountOfJumpsLeft > 0) {
            return true;
        } else {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public void DecreaseAmountOfJumpLeft() {
        amountOfJumpsLeft--;
    }
}
