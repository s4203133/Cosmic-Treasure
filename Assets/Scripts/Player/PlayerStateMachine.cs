using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField]
    private PlayerBaseState currentState;

    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallingState fallingState;

    [SerializeField] private PlayerController controller;

    void Start()
    {
        idleState = new PlayerIdleState(this, controller);
        runState = new PlayerRunState(this, controller, controller.playerMovment);
        jumpState = new PlayerJumpState(this, controller, controller.playerMovment, controller.playerJump);
        fallingState = new PlayerFallingState(this, controller);

        currentState = idleState;
        idleState.OnStateEnter();
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    private void FixedUpdate() {
        currentState.OnStatePhysicsUpdate();
    }

    public void ChangeState(PlayerBaseState newState) {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }
}
