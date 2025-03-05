using UnityEngine;

namespace LMO {

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

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnStateEnter() {
            // Stop jump to remove air velocity
            grounded.EndStartOfJump();

            // Apply air spin if player is in the air (or if they have ran out of air spins, change state)
            if (!grounded.IsOnGround) {
                if (!spin.CanAirSpin) {
                    DetermineNewState();
                    return;
                }
            }

            InputHandler.jumpStarted += Jump;
            InputHandler.groundPoundStarted += GroundPound;

            OnSpin?.Invoke();

            // Being spin action, and apply a jump boost if the player is in the air
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

        // Choose correct state to transition to
        private void DetermineNewState() {
            if (!grounded.IsOnGround) {
                stateMachine.ChangeState(stateMachine.fallingState);
                return;
            }
            if (input.moveInput == Vector2.zero) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }

        // Stop the spin early and jump if input is recieved
        private void Jump() {
            if (grounded.IsOnGround) {
                spin.StopSpin();
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        // Ground pound if the player is in the air and input is recieved
        private void GroundPound() {
            if (!grounded.IsOnGround) {
                spin.StopSpin();
                stateMachine.ChangeState(stateMachine.groundPoundState);
            }
        }
    }
}