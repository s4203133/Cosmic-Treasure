using UnityEngine;

namespace LMO {

    public class PlayerFallingState : PlayerBaseState {

        private PlayerMovement movement;
        private PlayerJump jump;
        private Grounded grounded;
        private PlayerSpinAttack spin;
        private PlayerInput input;
        private PlayerWallJump wallJump;

        private PlayerMovementSettings moveFallSettings;
        private PlayerJumpSettings fallSettings;

        private float coyoteTime;
        private bool hasPerformedAirMove;

        private float preventSlideDownWallTimer;

        public PlayerFallingState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            spin = context.playerSpinAttack;
            input = context.playerInput;
            wallJump = context.playerWallJump;

            moveFallSettings = context.PlayerSettings.JumpMovement;
            fallSettings = context.PlayerSettings.Jump;

            coyoteTime = 0.1f;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += JumpOrHover;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            movement.ChangeMovementSettings(moveFallSettings);
            jump.ChangeJumpSettings(fallSettings);
            CheckForSpinInput();
            CheckForHoverInput();
        }

        public override void OnStateUpdate() {
            preventSlideDownWallTimer -= TimeValues.Delta;
            // If the player has landed on the ground, determine which state to transition them to
            if (grounded.IsOnGround) {
                hasPerformedAirMove = false;
                preventSlideDownWallTimer = 0.0f;
                if (input.moveInput == Vector2.zero) {
                    stateMachine.ChangeState(stateMachine.idleState);
                } else {
                    stateMachine.ChangeState(stateMachine.runState);
                }
            }
            else {
                CheckForWallSlide();
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
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        public override void OnTriggerEnter(Collider other) {
            if (other.tag == "FloatArea") {
                stateMachine.ChangeState(stateMachine.floatState);
            }
        }

        // If the player presses the jump button while falling, determine whether to jump (using coyote time),
        // or trigger the hover state
        private void JumpOrHover() {
            if (!hasPerformedAirMove) {
                if (grounded.timeSinceLeftGround <= coyoteTime) {
                    stateMachine.ChangeState(stateMachine.jumpState);
                }
                else {
                    stateMachine.ChangeState(stateMachine.hoverState);
                }
                hasPerformedAirMove = true;
            }
            else {
                stateMachine.ChangeState(stateMachine.hoverState);
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

        private void CheckForWallSlide() {
            if(preventSlideDownWallTimer >= 0) {
                return;
            }

            wallJump.CheckForWalls();
            if (wallJump.WallInFrontOfPlayer && input.moveInput != Vector2.zero) {
                preventSlideDownWallTimer = 0.2f;
                stateMachine.ChangeState(stateMachine.slideDownWallState);
            }
        }

        private void GroundPound() {
            preventSlideDownWallTimer = 0.0f;
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }

        private void Grapple() {
            preventSlideDownWallTimer = 0.0f;
            stateMachine.ChangeState(stateMachine.swingState);
        }

        private void SmallSpringJump() {
            preventSlideDownWallTimer = 0.0f;
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }

        public void AllowWallSlide() {
            preventSlideDownWallTimer = 0.0f;
        }
    }
}