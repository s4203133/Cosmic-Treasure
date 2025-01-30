using UnityEngine;

[System.Serializable]
public abstract class PlayerBaseState
{
    protected PlayerController context;
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerController playerController) {
        context = playerController;
        stateMachine = context.playerStateMachine;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStatePhysicsUpdate();

    public abstract void OnStateExit();

    public abstract void OnCollisionEnter(Collision collision);

}
