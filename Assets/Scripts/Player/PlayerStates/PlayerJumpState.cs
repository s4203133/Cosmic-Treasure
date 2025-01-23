using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private Grounded grounded;

    public PlayerJumpState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = jump.groundedSystem;
    }

    public override void OnStateEnter() {
        InputHandler.jumpCancelled += jump.CutOffJump;
        InputHandler.SpinStarted += Spin;

        jump.InitialiseJump();
    }

    public override void OnStateUpdate() {
        if (grounded.IsOnGround()) {
            if (movement.moveInput.x == 0 && movement.moveInput.y == 0) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
        jump.ApplyForce();
    }

    public override void OnStateExit() {
        InputHandler.jumpCancelled -= jump.CutOffJump;
        InputHandler.SpinStarted -= Spin;

        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }
}
