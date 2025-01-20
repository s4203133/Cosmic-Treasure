using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovementInput movement;
    [SerializeField] PlayerJumpInput jump;

    [SerializeField] private PlayerStateMachine stateMachine;

    public PlayerMovement playerMovment => movement.playerMovement;
    public PlayerJump playerJump => jump.playerJump;

    private void OnEnable() {
        SubscribeActionEvents();
    }

    private void OnDisable() {
        UnsubscribeActionEvents();
    }

    private void SubscribeActionEvents() {
        movement.SubscribeMoveEvents();
        jump.SubscribeJumpEvents();
    }

    private void UnsubscribeActionEvents() {
        movement.UnsubscribeMoveEvents();
        jump.UnsubscribeJumpEvents();
    }

    public void Idle() {
        stateMachine.ChangeState(stateMachine.idleState);
    }

    public void Move() {
        stateMachine.ChangeState(stateMachine.runState);
    }

    public void Jump() {
        stateMachine.ChangeState(stateMachine.jumpState);
    }
}
