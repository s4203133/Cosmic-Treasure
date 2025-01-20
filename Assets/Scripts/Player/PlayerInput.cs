using UnityEngine;

public class PlayerInput {
    public Vector2 moveInput;
    static public bool jumpPressed;
    public bool groundPoundPressed;
}

[System.Serializable]
public class PlayerMovementInput {

    public PlayerMovement playerMovement;

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

    public PlayerJump playerJump;

    private void StartJump() {
        PlayerInput.jumpPressed = true;
    }

    private void PerformJump() {
        PlayerInput.jumpPressed = true;
    }

    private void StopJump() {
        PlayerInput.jumpPressed = false;
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

