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
    public PlayerSpinState spinState;
    public PlayerGroundPoundState groundPoundState;

    public PlayerController controller;

    void Start()
    {
        idleState = new PlayerIdleState(controller);
        runState = new PlayerRunState(controller);
        jumpState = new PlayerJumpState(controller);
        fallingState = new PlayerFallingState(controller);
        spinState = new PlayerSpinState(controller);
        groundPoundState = new PlayerGroundPoundState(controller);

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
