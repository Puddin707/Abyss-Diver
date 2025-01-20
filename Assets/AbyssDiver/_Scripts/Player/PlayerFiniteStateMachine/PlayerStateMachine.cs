using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    public void Initialize(PlayerState startingState) {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
