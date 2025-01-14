using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool isKnockbackActive;
    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration = 0.2f;
    public override void LogicUpdate() {
        CheckKnockback();
    }
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        core.Stats.DecreaseHealth(amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback() {
    if (isKnockbackActive) {
        if (Time.time >= knockbackStartTime + knockbackDuration || (core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground)) {
            isKnockbackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }
}
}
