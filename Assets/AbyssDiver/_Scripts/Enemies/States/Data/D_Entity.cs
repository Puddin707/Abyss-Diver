using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;
    public float damageHopSpeed = 3f;
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;
    public float maxAgroDistance = 4f;
    public float minAgroDistance = 3f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    public float closeRangeActionDistance = 1f;
    public float midRangeActionDistance = 5f;

    [Header("Touch Damage Settings")]
    public float touchDamage;
    public float touchDamageWidth;
    public float touchDamageHeight;

    public GameObject hitParticle;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
}
