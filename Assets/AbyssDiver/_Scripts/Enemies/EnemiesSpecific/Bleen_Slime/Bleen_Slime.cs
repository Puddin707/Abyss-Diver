using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleen_Slime : Entity
{
    public Bleen_Slime_IdleState idleState { get; private set;}
    public Bleen_Slime_MoveState moveState { get; private set;}
    public Bleen_Slime_ChargeState chargeState { get; private set;}
    public Bleen_Slime_PlayerDetectedState playerDetectedState { get; private set;}
    public Bleen_Slime_LookForPlayerState lookForPlayerState { get; private set;}
    public Bleen_Slime_MeleeAttackState meleeAttackState { get; private set;}
    public Bleen_Slime_RangeAttackState rangeAttackState { get; private set;}
    public Bleen_Slime_StunState stunState { get; private set;}
    public Bleen_Slime_DeadState deadState { get; private set;}
    public Bleen_Slime_HurtState hurtState { get; private set;}
    public Bleen_Slime_DodgeState dodgeState { get; private set;}

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_LookForPlayer lookForPlayerData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_RangeAttackState rangeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] public D_DodgeState dodgeStateData;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Awake() {
        base.Awake();

        moveState = new Bleen_Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Bleen_Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        chargeState = new Bleen_Slime_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        playerDetectedState = new Bleen_Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState = new Bleen_Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new Bleen_Slime_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        rangeAttackState = new Bleen_Slime_RangeAttackState(this, stateMachine, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        stunState = new Bleen_Slime_StunState(this, stateMachine, "stun", stunStateData, this); 
        deadState = new Bleen_Slime_DeadState(this, stateMachine, "dead", deadStateData, this);
        hurtState = new Bleen_Slime_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        dodgeState = new Bleen_Slime_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    
}
