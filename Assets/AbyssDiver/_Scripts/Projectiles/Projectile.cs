using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private AttackDetails attackDetails;
    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField] private float gravity;
    [SerializeField] private float damageRadius;

    private Rigidbody2D rb;

    private bool isGravityOn;
    private bool hasHitGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform damagePosition;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void Update() {
        if (!hasHitGround) {
            //attackDetails.position = transform.position;
            if (isGravityOn) {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate() {
        if (!hasHitGround) {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, playerLayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, groundLayer);
            Collider2D wallHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, wallLayer);

            if (damageHit) {
                //damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (groundHit) {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (wallHit) {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
        }
        if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn) {
            isGravityOn = true;
            rb.gravityScale = gravity;
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage) {
        this.speed = speed;
        this.travelDistance = travelDistance;
        //attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
