using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/Entity Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    public GameObject deathChuckParticle;
    public GameObject deathBloodParticle;

    public float deathAnimationDuration = 0.4f;
}
