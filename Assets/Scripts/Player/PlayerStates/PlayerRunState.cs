using UnityEngine;

[System.Serializable]
public class PlayerRunState : PlayerBaseState {

    private PlayerMovement movement;
    private Grounded grounded;

    public PlayerRunState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        grounded = context.playerJump.groundedSystem;
    }

    public override void OnStateEnter() {
        InputHandler.moveCancelled += StopMovement;
        InputHandler.jumpStarted += Jump;
        InputHandler.SpinStarted += Spin;

        movement.OnMoveStarted?.Invoke();
        CheckForJumpInput();
    }

    public override void OnStateUpdate() {
        CheckIfPlayerLeftPlatform();
        CheckIfFinishedMoving();
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
    }

    public override void OnStateExit() {
        InputHandler.moveCancelled -= StopMovement;
        InputHandler.jumpStarted -= Jump;
        InputHandler.SpinStarted -= Spin;

        movement.OnMoveStopped?.Invoke();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void StopMovement(Vector2 inputValue) {
        movement.ResetVelocityVariables();
    }

    private void Jump() {
        if (stateMachine.controller.playerJump.CanJump()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void CheckForJumpInput() {
        if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void CheckIfPlayerLeftPlatform() {
        if (!grounded.IsOnGround) {
            stateMachine.ChangeState(stateMachine.fallingState);
            return;
        }
    }

    private void CheckIfFinishedMoving() {
        if (movement.moveInput == Vector2.zero) {
            if (movement.HasStopped()) {
                stateMachine.ChangeState(stateMachine.idleState);
                return;
            }
        }
    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }
}
