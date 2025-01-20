using UnityEngine;

[System.Serializable]
public abstract class PlayerBaseState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController controller;

    public PlayerBaseState(PlayerStateMachine playerStateMachine, PlayerController playerController) {
        stateMachine = playerStateMachine;
        controller = playerController;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStatePhysicsUpdate();

    public abstract void OnStateExit();

    public abstract void OnCollisionEnter(Collision collision);
}
