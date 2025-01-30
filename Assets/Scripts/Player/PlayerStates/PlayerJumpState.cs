using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private PlayerMovement movement;
    protected PlayerJump jump;
    private Grounded grounded;

    public PlayerJumpState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = jump.groundedSystem;
    }

    public override void OnStateEnter() {
        InputHandler.jumpCancelled += jump.CutOffJump;
        InputHandler.SpinStarted += Spin;
        InputHandler.groundPoundStarted += GroundPound;
        InputHandler.jumpStarted += Hover;

        StartJump();
    }

    public override void OnStateUpdate() {
        if (grounded.IsOnGround) {
            if (movement.moveInput == Vector2.zero) {
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
        InputHandler.groundPoundStarted -= GroundPound;
        InputHandler.jumpStarted -= Hover;

        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    protected virtual void StartJump() {
        jump.InitialiseJump();
    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }

    private void GroundPound() {
        stateMachine.ChangeState(stateMachine.groundPoundState);
    }

    private void Hover() {
        if (!grounded.IsOnGround) {
            stateMachine.ChangeState(stateMachine.hoverState);
        }
    }
}
