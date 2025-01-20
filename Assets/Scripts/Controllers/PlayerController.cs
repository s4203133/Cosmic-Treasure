using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovementInput movement;
    [SerializeField] PlayerJumpInput jump;

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
}

[System.Serializable]
public class PlayerMovementInput {

    [SerializeField] private PlayerMovement playerMovement;

    private void StartMovement(Vector2 value) {
        playerMovement.moveDirection = value;
    }

    private void PerformMovement(Vector2 value) {
        playerMovement.moveDirection = value;
    }

    private void StopMovement(Vector2 value) {
        playerMovement.moveDirection = value;
    }

    public void SubscribeMoveEvents() {
        InputHandler.moveStarted += StartMovement;
        InputHandler.movePerformed += PerformMovement;
        InputHandler.moveCancelled += StopMovement;
    }

    public void UnsubscribeMoveEvents() {
        InputHandler.moveStarted -= StartMovement;
        InputHandler.movePerformed -= PerformMovement;
        InputHandler.moveCancelled -= StopMovement;
    }
}

[System.Serializable]
public class PlayerJumpInput {

    [SerializeField] private PlayerJump playerJump;

    private void StartJump() {
        Debug.Log("Starting Jump!");
    }

    private void PerformJump() {
        Debug.Log("Performing Jump!");
    }

    private void StopJump() {
        Debug.Log("Stopping Jump!");
    }

    public void SubscribeJumpEvents() {
        InputHandler.jumpStarted += StartJump;
        InputHandler.jumpPerformed += PerformJump;
        InputHandler.jumpCancelled += StopJump;
    }

    public void UnsubscribeJumpEvents() {
        InputHandler.jumpStarted -= StartJump;
        InputHandler.jumpPerformed -= PerformJump;
        InputHandler.jumpCancelled -= StopJump;
    }
}
