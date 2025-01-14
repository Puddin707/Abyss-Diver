using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    private float timeInState;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();

    }

    public override void Enter() {
        base.Enter();

        GameObject.Instantiate(stateData.deathBloodParticle, entity.transform.position, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChuckParticle, entity.transform.position, stateData.deathChuckParticle.transform.rotation);

        timeInState = 0f;

    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        timeInState += Time.deltaTime;

        if (timeInState >= stateData.deathAnimationDuration) {
            entity.gameObject.SetActive(false);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
