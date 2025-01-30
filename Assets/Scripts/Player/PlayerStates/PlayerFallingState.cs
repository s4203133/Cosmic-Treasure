using UnityEngine;

public class PlayerFallingState : PlayerBaseState {

    private PlayerMovement movement;
    private PlayerJump jump;
    private Grounded grounded;
    private PlayerSpinAttack spin;

    private float coyoteTime;

    private bool hasPerformedAirMove;

    public PlayerFallingState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = jump.groundedSystem;
        spin = context.playerSpinAttack;

        coyoteTime = 0.1f;
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += JumpOrHover;
        InputHandler.SpinStarted += Spin;
        InputHandler.groundPoundStarted += GroundPound;

        CheckForSpinInput();
        CheckForHoverInput();
    }

    public override void OnStateUpdate() {
        if (grounded.IsOnGround) {
            hasPerformedAirMove = false;
            if (movement.moveInput == Vector2.zero) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
        jump.ApplyFallForce();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= JumpOrHover;
        InputHandler.SpinStarted -= Spin;
        InputHandler.groundPoundStarted -= GroundPound;

        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void JumpOrHover() {
        if (!hasPerformedAirMove) {
            if (grounded.timeSinceLeftGround <= coyoteTime) {
                stateMachine.ChangeState(stateMachine.jumpState);
            } else {
                stateMachine.ChangeState(stateMachine.hoverState);
            }
            hasPerformedAirMove = true;
        }
    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }

    private void CheckForSpinInput() {
        if (!spin.CanAirSpin) {
            return;
        }
        if (InputBuffers.instance.spin.HasInputBeenRecieved()) {
            Spin();
        }
    }

    private void CheckForHoverInput() {
        if (hasPerformedAirMove || grounded.timeSinceLeftGround <= coyoteTime) {
            return;
        }

        if (InputBuffers.instance.jump.HasInputBeenRecieved() && InputHandler.jumpBeingPressed) {
            hasPerformedAirMove = true;
            stateMachine.ChangeState(stateMachine.hoverState);
        }
    }

    private void GroundPound() {
        stateMachine.ChangeState(stateMachine.groundPoundState);
    }
}
