using UnityEngine;

[System.Serializable]
public abstract class PlayerBaseState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine) {
        stateMachine = playerStateMachine;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStatePhysicsUpdate();

    public abstract void OnStateExit();

    public abstract void OnCollisionEnter(Collision collision);
}
