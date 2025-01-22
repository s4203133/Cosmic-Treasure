using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Input")]
    [SerializeField] private InputHandler input;
    [SerializeField] private InputBuffers inputBuffers;


    [SerializeField] private PlayerMovementInput movement;
    [SerializeField] private PlayerJumpInput jump;

    [Header("Components")]
    [SerializeField] private PlayerStateMachine stateMachine;
    public Rigidbody rigidBody;

    // Public Accessors
    public InputBuffers inputBufferHolder => inputBuffers;
    public PlayerStateMachine playerStateMachine => stateMachine;
    public PlayerMovement playerMovment => movement.playerMovement;
    public PlayerJump playerJump => jump.playerJump;

    private void OnEnable() {
        SubscribeActionEvents();
    }

    private void OnDisable() {
        UnsubscribeActionEvents();
    }

    private void SubscribeActionEvents() {
        //movement.SubscribeMoveEvents();
        jump.SubscribeJumpEvents();
    }

    private void UnsubscribeActionEvents() {
        //movement.UnsubscribeMoveEvents();
        jump.UnsubscribeJumpEvents();
    }
}