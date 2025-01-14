using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // private Animator anim;
    // private PlayerMovement PC;
    // private PlayerStats PS;
    // [SerializeField] private Transform attack1HitBoxPos;
    // [SerializeField] private LayerMask whatIsDamageable;
    // [SerializeField] private bool combatEnabled;
    // [SerializeField] private float inputTimer, attack1Radius, attack1Damage;
    // [SerializeField] private float stunDamageAmount = 1f;
    // private bool gotInput, isAttacking, isFirstAttack;
    // private float lastInputTime = Mathf.NegativeInfinity;
    // private AttackDetails attackDetails;
    // private bool inputAttack1;

    // private void Start() {
    //     anim = GetComponent<Animator>();
    //     anim.SetBool("canAttack", combatEnabled);
    //     PC = GetComponent<PlayerMovement>();
    //     PS = GetComponent<PlayerStats>();
    // }

    // private void Update()
    // {
    //     CheckCombatInput();
    //     CheckAttacks();
    // }
    // public void OnAttack1(InputAction.CallbackContext context){
    //     if (context.performed) {
    //         inputAttack1 = true;
    //     }
    // }
    // private void CheckCombatInput() {
    //     if (inputAttack1) {
    //         if (combatEnabled) {
    //             gotInput = true;
    //             lastInputTime = Time.time;
    //         }
    //     } 
    //         inputAttack1 = false;
    // }

    // private void CheckAttacks() {
    //     if (gotInput) {
    //         if (!isAttacking) {
    //             gotInput = false;
    //             isAttacking = true;
    //             isFirstAttack = !isFirstAttack;
    //             anim.SetBool("attack1", true);
    //             anim.SetBool("firstAttack", isFirstAttack);
    //             anim.SetBool("isAttacking", isAttacking);
    //         }
    //     }

    //     if (Time.time >= lastInputTime + inputTimer) {
    //         gotInput = false;
    //     }
    // }

    // private void CheckAttackHitBox() {
    //     Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

    //     attackDetails.damageAmount = attack1Damage;
    //     attackDetails.position = transform.position;
    //     foreach (Collider2D collider in detectedObjects) {
    //         collider.transform.parent.SendMessage("Damage", attackDetails);
    //     }
    //     attackDetails.stunDamageAmount = stunDamageAmount;
    // }

    // private void FinishAttack1() {
    //     isAttacking = false;
    //     anim.SetBool("isAttacking", isAttacking);
    //     anim.SetBool("attack1", false);
    // }

    // private void Damage(AttackDetails attackDetails) {
    //     if (!PC.GetDashStatus()) {
    //         int direction;

    //     PS.DecreaseHealth(attackDetails.damageAmount);

    //     if (attackDetails.position.x < transform.position.x) {
    //         direction = 1;
    //     } else {
    //         direction = -1;
    //     }

    //     PC.Knockback(direction);
    //     }
    // }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    // }
}
