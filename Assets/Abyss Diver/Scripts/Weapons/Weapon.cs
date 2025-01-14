using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;
    private Animator baseAnimator;
    private Animator slashAnimator;
    protected int attackCounter;

    protected PlayerAttackState state;
    protected Core core;

    protected virtual void Awake() {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        slashAnimator = transform.Find("Slash").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon() {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks) {
            attackCounter = 0;
        }

        baseAnimator.SetBool("attack", true);
        slashAnimator.SetBool("attack", true);


        baseAnimator.SetInteger("attackCounter", attackCounter);
        slashAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon() {
        baseAnimator.SetBool("attack", false);
        slashAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger() {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger() {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger() {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlipTrigger() {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger() {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger() {

    }

    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core) {
        this.state = state;
        this.core = core;
    }
}
