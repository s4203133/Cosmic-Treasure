using UnityEngine;

public class PlayerSpinState : PlayerBaseState {

    private PlayerMovement movement;
    private Grounded grounded;
    private PlayerSpinAttack spin;
    private PlayerInput input;

    public delegate void CustomEvent();
    public static CustomEvent OnSpin;

    public PlayerSpinState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        grounded = context.playerJump.groundedSystem;
        spin = context.playerSpinAttack;
        input = context.playerInput;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        grounded.EndStartOfJump();
        if (!grounded.IsOnGround) {
            if (!spin.CanAirSpin) {
                DetermineNewState();
                return;
            }
        }

        InputHandler.jumpStarted += Jump;
        InputHandler.groundPoundStarted += GroundPound;

        OnSpin?.Invoke();

        spin.StartSpin();
        if (!grounded.IsOnGround) {
            spin.ApplyJumpBoost();
        }
    }

    public override void OnStateUpdate() {
        spin.Countdown();
        if (spin.SpinFinished) {
            DetermineNewState();
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= Jump;
        InputHandler.groundPoundStarted -= GroundPound;

        spin.StopSpin();
    }

    private void DetermineNewState() {
        if (!grounded.IsOnGround) {
            stateMachine.ChangeState(stateMachine.fallingState);
            return;
        }
        if(input.moveInput == Vector2.zero) {
            stateMachine.ChangeState(stateMachine.idleState);
        } else {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }

    private void Jump() {
        if (grounded.IsOnGround) {
            spin.StopSpin();
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void GroundPound() {
        if (!grounded.IsOnGround) {
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }
    }
}
