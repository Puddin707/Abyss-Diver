using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Slime : Entity
{
    public Green_Slime_IdleState idleState { get; private set;}
    public Green_Slime_MoveState moveState { get; private set;}
    public Green_Slime_ChargeState chargeState { get; private set;}
    public Green_Slime_PlayerDetectedState playerDetectedState { get; private set;}
    public Green_Slime_LookForPlayerState lookForPlayerState { get; private set;}
    public Green_Slime_RangeAttackState rangeAttackState { get; private set;}
    public Green_Slime_StunState stunState { get; private set;}
    public Green_Slime_DeadState deadState { get; private set;}
    public Green_Slime_HurtState hurtState { get; private set;}

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_LookForPlayer lookForPlayerData;
    [SerializeField] private D_RangeAttackState rangeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Awake() {
        base.Awake();

        moveState = new Green_Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Green_Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        chargeState = new Green_Slime_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        playerDetectedState = new Green_Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState = new Green_Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        rangeAttackState = new Green_Slime_RangeAttackState(this, stateMachine, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        stunState = new Green_Slime_StunState(this, stateMachine, "stun", stunStateData, this); 
        deadState = new Green_Slime_DeadState(this, stateMachine, "dead", deadStateData, this);
        hurtState = new Green_Slime_HurtState(this, stateMachine, "hurt", hurtStateData, this);

    }

    private void Start() {
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
