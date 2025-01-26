using UnityEngine;

public class PlayerSpinState : PlayerBaseState {

    private PlayerMovement movement;
    private PlayerJump jump;
    private Grounded grounded;
    private PlayerSpinAttack spin;

    public PlayerSpinState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = context.playerJump.groundedSystem;
        spin = context.playerSpinAttack;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        spin.StartSpin();
        spin.squashAndStretch.Play();
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
        spin.StopSpin();
    }

    private void DetermineNewState() {
        if (!grounded) {
            stateMachine.ChangeState(stateMachine.fallingState);
            return;
        }
        if(movement.moveInput == Vector2.zero) {
            stateMachine.ChangeState(stateMachine.idleState);
        } else {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }
}
