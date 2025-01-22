using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private InputHandler input;

    [SerializeField] private PlayerMovementInput movement;
    [SerializeField] private PlayerJumpInput jump;

    [SerializeField] private PlayerStateMachine stateMachine;

    public Rigidbody rigidBody;

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