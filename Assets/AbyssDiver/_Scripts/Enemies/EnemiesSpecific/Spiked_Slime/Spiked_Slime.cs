using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_Slime : Entity
{
    public Spiked_Slime_IdleState idleState { get; private set;}
    public Spiked_Slime_MoveState moveState { get; private set;}
    public Spiked_Slime_PlayerDetectedState playerDetectedState { get; private set;}
    public Spiked_Slime_ChargeState chargeState { get; private set;}
    public Spiked_Slime_LookForPlayerState lookForPlayerState { get; private set;}
    public Spiked_Slime_MeleeAttackState meleeAttackState { get; private set;}
    public Spiked_Slime_StunState stunState { get; private set;}
    public Spiked_Slime_DeadState deadState { get; private set;}
    public Spiked_Slime_HurtState hurtState { get; private set;}
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private Transform meleeAttackPosition;

    public override void Awake() {
        base.Awake();

        //touchDamageCheck.gameObject.SetActive(true);

        moveState = new Spiked_Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Spiked_Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Spiked_Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Spiked_Slime_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Spiked_Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new Spiked_Slime_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Spiked_Slime_StunState(this, stateMachine, "stun", stunStateData, this); 
        deadState = new Spiked_Slime_DeadState(this, stateMachine, "dead", deadStateData, this);
        hurtState = new Spiked_Slime_HurtState(this, stateMachine, "hurt", hurtStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
