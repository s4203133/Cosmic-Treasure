using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private Grounded grounded;

    private float allowJumpDuration;
    private float countdown;

    public PlayerFallingState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = jump.groundedSystem;
        allowJumpDuration = 0.15f;
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += Jump;
        InputHandler.SpinStarted += Spin;
        InputHandler.groundPoundStarted += GroundPound;

        CheckForSpinInput();

        countdown = allowJumpDuration;
    }

    public override void OnStateUpdate() {
        countdown -= Time.deltaTime;

        if (grounded.IsOnGround) {
            if(movement.moveInput == Vector2.zero) {
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
        InputHandler.jumpStarted -= Jump;
        InputHandler.SpinStarted -= Spin;
        InputHandler.groundPoundStarted -= GroundPound;

        if (grounded.IsOnGround) {
            context.squashAndStretch.Land.Play();
        }
        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void Jump() {
        if (countdown > 0) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }

    private void CheckForSpinInput() {
        if (context.inputBufferHolder.spin.HasInputBeenRecieved()) {
            Spin();
        }
    }

    private void GroundPound() {
        stateMachine.ChangeState(stateMachine.groundPoundState);
    }
}
