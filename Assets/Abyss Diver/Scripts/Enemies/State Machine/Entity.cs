using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;
    public FiniteStateMachine stateMachine;
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set;}
    public int lastDamageDirection { get; private set;}
    public Core Core { get; private set; }
    [SerializeField] private Transform playerCheck;
    //[SerializeField] protected Transform touchDamageCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    
    private Vector2 velocityWorkspace;
    protected bool isStunned;
    protected bool isDead;

    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;
    private float lastTouchDamageTime;
    //[SerializeField] private float touchDamageCooldown = 1f;

    public virtual void Awake() {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();

        stateMachine =  new FiniteStateMachine();

        //touchDamageCheck.gameObject.SetActive(false);
    }

    public virtual void Update() {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);
        
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime) {
            ResetStunResistance();
        }

        //TouchDamage();
    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAgroRange() {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.playerLayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange() {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.playerLayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction() {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.playerLayer);
    }

    public virtual bool CheckPlayerInMidRangeAction() {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.midRangeActionDistance, entityData.playerLayer);
    }

    public virtual void DamageHop(float velocity) {
        velocityWorkspace.Set(Core.Movement.RB.velocity.x, velocity);
        Core.Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance() {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    // public virtual void TouchDamage() {
    //     AttackDetails attackDetails = new AttackDetails();
    //     if (Time.time >= lastTouchDamageTime + touchDamageCooldown) {
    //         touchDamageBotLeft.Set(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
    //     touchDamageTopRight.Set(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));

    //     if (touchDamageCheck.gameObject.activeSelf) {
    //         Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, entityData.playerLayer);

    //     if (hit != null)
    // {
    //     lastTouchDamageTime = Time.time;
    //     attackDetails.damageAmount = entityData.touchDamage;
    //     attackDetails.position = transform.position;

    //     hit.SendMessage("Damage", attackDetails);
    // }
    //     }
    //     }
        
    // }

    public virtual void OnDrawGizmos() {
        if (Core != null) {
        Gizmos.DrawLine(Core.CollisionSenses.WallCheck.position, Core.CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * Core.CollisionSenses.WallCheckDistance));
        Gizmos.DrawLine(Core.CollisionSenses.LedgeCheckVertical.position, Core.CollisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * Core.CollisionSenses.WallCheckDistance));
        

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.midRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);

        // Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
        // Vector2 botRight = new Vector2(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
        // Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));
        // Vector2 topRight = new Vector2(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));

        // Gizmos.DrawLine(botLeft, botRight);
        // Gizmos.DrawLine(botRight, topRight);
        // Gizmos.DrawLine(topRight, topLeft);
        // Gizmos.DrawLine(topLeft, botLeft);
        }
        
    }
    

}
