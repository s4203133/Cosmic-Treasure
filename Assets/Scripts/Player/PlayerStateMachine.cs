using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField]
    private PlayerBaseState currentState;
    public string stateName;

    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallingState fallingState;

    [SerializeField] private PlayerController controller;

    void Start()
    {
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this, controller.playerMovment);
        jumpState = new PlayerJumpState(this, controller.playerMovment, controller.playerJump);
        fallingState = new PlayerFallingState(this, controller.playerMovment, controller.playerJump);

        currentState = idleState;
        stateName = currentState.ToString();
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
        stateName = currentState.ToString();
        currentState.OnStateEnter();
    }
}
