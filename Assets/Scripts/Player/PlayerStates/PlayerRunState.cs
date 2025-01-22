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
        CheckForJumpInput();
    }

    public override void OnStateUpdate() {
        if (!grounded.IsOnGround()) {
            stateMachine.ChangeState(stateMachine.fallingState);
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
    }

    public override void OnStateExit() {
        InputHandler.moveCancelled -= StopMovement;
        InputHandler.jumpStarted -= Jump;
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void StopMovement(Vector2 inputValue) {
        movement.StopMovement();
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Jump() {
        if (stateMachine.controller.playerJump.CanJump()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void CheckForJumpInput() {
        if (context.inputBufferHolder.jumpInputBuffer.HasInputBeenRecieved()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }
}
