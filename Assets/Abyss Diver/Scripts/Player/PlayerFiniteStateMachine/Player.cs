using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; } 
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set;}
    public PlayerAttackState SecondaryAttackState { get; private set;}

    [SerializeField] private PlayerData playerData;

    #endregion

    public Core Core { get; private set; }
    public Animator Anim { get; private set; } 
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    
    private Vector2 workspace;
    #region Unity Callback Function
    private void Awake() {
        Core = GetComponentInChildren<Core>();
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, stateMachine,playerData, "inAir");
        AirState = new PlayerAirState(this, stateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, stateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, stateMachine, playerData, "dash");
        CrouchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, stateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<PlayerInventory>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        stateMachine.Initialize(IdleState);


        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
    }

    private void Update() {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    #endregion

    

    

    public void SetColliderHeight(float height) {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }
    
    private void AnimationTrigger() {
        stateMachine.currentState.AnimationTrigger();
    }

    private void AnimationFinishedTrigger() {
        stateMachine.currentState.AnimationFinishTrigger();
    }
    

    
}
