using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
//REFERENCES
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public ParticleSystem dust;
    public ParticleSystem dash;
//MOVE THING
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Vector2 moveInput;
    private bool jumpCheck;
    private bool isFacingRight = true;
    private bool canFlip = true;
    private int facingDirection = 1;
    private bool knockback;
    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;

//WALL THING
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
//LEDGE THING
    [HideInInspector] public bool ledgeDetected;
    [SerializeField] private Vector2 offset1, offset2, offset3, offset4;
    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;
    private bool canGrabLedge = true;
    private bool canClimb;
//DASH THING
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing;
    private bool hasDashedDiagonally;
    private float dashCooldownTimer;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float afterImageInterval = 0.06f;
    private float afterImageTimer;
//AWAKE
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        body.velocity = new Vector2(0, body.velocity.y);
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
//UPDATE
    private void Update() {
        Flip();
        checkForLedge();
        AnimatorControllers();
        WallSlide();
        WallJump();
        CheckKnockBack();
        if (dashCooldownTimer > 0) {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (isGrounded()) {
            hasDashedDiagonally = false;
        }
        if (jumpCheck) {
            Jump();
            jumpCheck = false;
        }
        anim.SetBool("run", moveInput.x != 0);
        anim.SetBool("grounded", isGrounded());
    }
    private void FixedUpdate() {
        if (!isWallJumping && !isDashing && !knockback) {
            body.velocity = new Vector2(moveInput.x * moveSpeed, body.velocity.y);
        }
    }
//INPUT ACTION
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed && !canClimb) {
            jumpCheck = true;
        }
    }
    public void OnDash(InputAction.CallbackContext context) {
        if (context.performed && dashCooldownTimer <= 0 && !isDashing) {
            StartCoroutine(Dash());
        }
    }
//FLIP
    private void Flip() {
        if (moveInput.x > 0.01f && !isFacingRight && !canClimb && canFlip && !knockback) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
            facingDirection = 1;
            if(isGrounded()){
                CreateDust(); 
            }
        }
        else if (moveInput.x < -0.01f && isFacingRight && !canClimb && canFlip && !knockback) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
            facingDirection = -1;
            if(isGrounded()){
                CreateDust(); 
            }
        }   
    }
    public void EnableFlip() {
        canFlip = true;
    }
    public void DisableFlip() {
        canFlip = false;
    }
    public int GetFacingDirection() {
        return facingDirection;
    }
//KNOCKBACK
    public void Knockback(int direction) {
        knockback = true;
        knockbackStartTime = Time.time;
        body.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockBack() {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback) {
            knockback = false;
            body.velocity = new Vector2(0.0f, body.velocity.y);
        }
    }
//JUMP
    public void Jump() {
        if (isGrounded()) {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            CreateDust();
            anim.SetTrigger("jump");
        }
    }
//WALL
    private void WallSlide() {
        if (onWall() && !isGrounded() && moveInput.x != 0f) {
            isWallSliding = true;
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
            anim.SetBool("isWallSliding", true);
        } else {
            isWallSliding = false;
            anim.SetBool("isWallSliding", false);
        }
    }
    private void WallJump() {
        if (isWallSliding) {
            isWallJumping = false;
            wallJumpingDirection = onWall() ? -Mathf.Sign(moveInput.x) : transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        } else {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (jumpCheck && wallJumpingCounter > 0f) {
            isWallJumping = true;
            body.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpingDirection) {
                Vector3 localScale = transform.localScale;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping() {
        isWallJumping = false;
    }
//LEDGE
    private void checkForLedge() {
        if (ledgeDetected && canGrabLedge) {
            canGrabLedge = false;
            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;
            climbBegunPosition = isFacingRight ? ledgePosition + offset1 : ledgePosition + offset3;
            climbOverPosition = isFacingRight ? ledgePosition + offset2 : ledgePosition + offset4;
            canClimb = true;
        }
        if (canClimb) {
            transform.position = climbBegunPosition;
        }
    }
    private void ledgeClimbOver() {
        canClimb = false;
        transform.position = climbOverPosition;
        Invoke("AllowLedgeGrab", .1f);
    }
    private void AllowLedgeGrab() => canGrabLedge = true;
//DASH
    private IEnumerator Dash() {
        if(!isWallSliding && !canClimb) {
            if (moveInput.x != 0 && moveInput.y != 0 && hasDashedDiagonally) {
            yield break;
        }
    isDashing = true;
    CreateDash();

    Vector2 originalSize = boxCollider.size;
    Vector2 originalOffset = boxCollider.offset;
    float originalGravity = body.gravityScale;
    body.gravityScale = 0;
        if(isGrounded()) {
        boxCollider.size = new Vector2(0.1766083f, 0.08382312f);
        boxCollider.offset = new Vector2(0.003479198f, -0.11f);
        }

    Vector2 dashingDir = new Vector2(moveInput.x, moveInput.y).normalized;
    body.velocity = new Vector2(dashingDir.x * dashForce, dashingDir.y * dashForce);
        if (moveInput.x != 0 && moveInput.y != 0) {
            hasDashedDiagonally = true;
            body.velocity = new Vector2(dashingDir.x * dashForce, dashingDir.y * dashForce * 0.7f);
        }
        if (moveInput.x == 0) {
            body.velocity = new Vector2(isFacingRight ? dashForce : -dashForce, 0);
        }

    float dashTimer = dashDuration;
    while (dashTimer > 0) {
        if (IsInsideNarrowSpace()) {
            dashTimer += Time.deltaTime; 
        }
        afterImageTimer -= Time.deltaTime;
        if (afterImageTimer <= 0) {
            SpawnAfterImage();
            afterImageTimer = afterImageInterval;
        }
        dashTimer -= Time.deltaTime;
        yield return null;
    }

    body.gravityScale = originalGravity;
    boxCollider.size = originalSize;
    boxCollider.offset = originalOffset;

    isDashing = false;
    dashCooldownTimer = dashCooldown;

    anim.SetBool("isRolling", false);
    anim.SetBool("isDashing", false);
        }
        
    }

    private void SpawnAfterImage() {
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, Quaternion.identity);
        AfterImage afterImageScript = afterImage.GetComponent<AfterImage>();
        afterImageScript.SetUp(GetComponent<SpriteRenderer>().sprite,transform.position,transform.localScale,new Color(1f, 1f, 1f, 0.8f));
    }

    private bool IsInsideNarrowSpace() {
        float checkDistance = 2f;
        Vector2 topCheck = boxCollider.bounds.center + new Vector3(0, boxCollider.bounds.extents.y, 0);
        Vector2 bottomCheck = boxCollider.bounds.center - new Vector3(0, boxCollider.bounds.extents.y, 0);
        RaycastHit2D topHit = Physics2D.Raycast(topCheck, Vector2.up, checkDistance, groundLayer);
        RaycastHit2D bottomHit = Physics2D.Raycast(bottomCheck, Vector2.down, checkDistance, groundLayer);
        return topHit.collider != null && bottomHit.collider != null;
     }

     public bool GetDashStatus() {
        return isDashing;
     }
//CHECK COLLIDER
    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
//ANIMATION
    private void AnimatorControllers() {
        anim.SetBool("canClimb", canClimb);
        if(isDashing){
            if (isGrounded()) {
                anim.SetBool("isRolling", true);
                anim.SetBool("isDashing", false);
                } else {
                anim.SetBool("isRolling", false);
                anim.SetBool("isDashing", true);
            }
        }
    }
//PARTICLE
    void CreateDust() {
        dust.Play();
    }
    void CreateDash() {
        dash.Play();
    } 
}
