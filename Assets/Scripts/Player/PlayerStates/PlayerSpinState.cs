using UnityEngine;

public class PlayerSpinState : PlayerBaseState {

    private PlayerMovement movement;
    private Grounded grounded;
    private PlayerSpinAttack spin;

    public PlayerSpinState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        grounded = context.playerJump.groundedSystem;
        spin = context.playerSpinAttack;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        InputHandler.groundPoundStarted += GroundPound;

        spin.StartSpin();
        context.squashAndStretch.SpinAttack.Play();
        context.vfx.PlaySpinVFX();
        if (!grounded.IsOnGround()) {
            spin.ApplyJumpBoost();
        }
    }

    public override void OnStateUpdate() {
        spin.Countdown();
        if (spin.SpinFinished()) {
            DetermineNewState();
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
    }

    public override void OnStateExit() {
        InputHandler.groundPoundStarted -= GroundPound;

        spin.StopSpin();
    }

    private void DetermineNewState() {
        if (!grounded.IsOnGround()) {
            stateMachine.ChangeState(stateMachine.fallingState);
            return;
        }
        if(movement.moveInput == Vector2.zero) {
            stateMachine.ChangeState(stateMachine.idleState);
        } else {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }

    private void GroundPound() {
        if (!grounded.IsOnGround()) {
            spin.StopSpin();
            //context.vfx.StopSpinVFX();
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }
    }
}
