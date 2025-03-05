using UnityEngine;

namespace LMO {

    public class PlayerFallingState : PlayerBaseState {

        private PlayerMovement movement;
        private PlayerJump jump;
        private Grounded grounded;
        private PlayerSpinAttack spin;
        private PlayerInput input;

        private float coyoteTime;
        private bool hasPerformedAirMove;

        public PlayerFallingState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            spin = context.playerSpinAttack;
            input = context.playerInput;

            coyoteTime = 0.1f;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += JumpOrHover;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.grappleStarted += Grapple;

            CheckForSpinInput();
            CheckForHoverInput();
        }

        public override void OnStateUpdate() {
            // If the player has landed on the ground, determine which state to transition them to
            if (grounded.IsOnGround) {
                hasPerformedAirMove = false;
                if (input.moveInput == Vector2.zero) {
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
            InputHandler.grappleStarted -= Grapple;

            jump.EndJump();
        }

        public override void OnTriggerEnter(Collider other) {
        }

        // If the player presses the jump button while falling, determine whether to jump (using coyote time),
        // or trigger the hover state
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
            // If the player has reached the maximum amount of spins in the air, don't continue
            if (!spin.CanAirSpin) {
                return;
            }

            // Check if spin input was pressed recently (may have been pressed just before entering state)
            if (InputBuffers.instance.spin.HasInputBeenRecieved()) {
                Spin();
            }
        }

        // Check that the player hasn't already hovered, and begin hover movement
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

        private void Grapple() {
            stateMachine.ChangeState(stateMachine.swingState);
        }
    }
}