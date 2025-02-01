using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Vector2 moveInput;

    [SerializeField] private PlayerMovementInput playerMoveInput;

    public void OnEnable() {
        playerMoveInput.SubscribeMoveEvents();
        SubscribeInputEvents();
    }

    public void OnDisable() {
        playerMoveInput.UnsubscribeMoveEvents();
        UnsubscribeInputEvents();
    }

    private void SubscribeInputEvents() {
        InputHandler.moveStarted += GetMovement;
        InputHandler.movePerformed += GetMovement;
        InputHandler.moveCancelled += GetMovement;
    }

    private void UnsubscribeInputEvents() {
        InputHandler.moveStarted -= GetMovement;
        InputHandler.movePerformed -= GetMovement;
        InputHandler.moveCancelled -= GetMovement;
    }

    private void GetMovement(Vector2 value) {
        moveInput = value;
    }
}

[System.Serializable]
public class PlayerMovementInput {

    public PlayerMovement playerMovement;

    private void StartMovement(Vector2 value) {
        playerMovement.OnMoveStarted?.Invoke();
        playerMovement.isStopping = false;
    }

    private void StopMovement(Vector2 value) {
        playerMovement.ResetVelocityVariables();
    }

    public void SubscribeMoveEvents() {
        InputHandler.moveStarted += StartMovement;
        InputHandler.moveCancelled += StopMovement;
    }

    public void UnsubscribeMoveEvents() {
        InputHandler.moveStarted -= StartMovement;
        InputHandler.moveCancelled -= StopMovement;
    }
}

[System.Serializable]
public class PlayerJumpInput {

    public PlayerJump playerJump;

    private void StartJump() {
    }

    private void PerformJump() {
    }

    private void StopJump() {
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
