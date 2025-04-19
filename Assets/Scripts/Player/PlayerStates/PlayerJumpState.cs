using UnityEngine;

namespace LMO {

    public class PlayerJumpState : PlayerBaseState {

        protected PlayerMovement movement;
        protected PlayerJump jump;
        private Grounded grounded;
        private PlayerInput input;

        protected PlayerJumpSettings jumpSettings;
        protected PlayerMovementSettings jumpMoveSettings;

        public PlayerJumpState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            input = context.playerInput;

            jumpSettings = context.PlayerSettings.Jump;
            jumpMoveSettings = context.PlayerSettings.JumpMovement;
        }

        public override void OnStateEnter() {
            InputHandler.jumpCancelled += jump.CutOffJump;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.jumpStarted += Hover;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            StartJump();
            stateMachine.fallingState.AllowWallSlide();
        }

        public override void OnStateUpdate() {
            // Once the player has landed on the ground, determine which state to transition to
            if (grounded.IsOnGround) {
                if (input.moveInput == Vector2.zero) {
                    Idle();
                } else {
                    Run();
                }
            }
            else {
                if(jump.reachedPeakOfJump) {
                    stateMachine.Fall();
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
            InputHandler.grappleStarted -= Grapple;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        public override void OnTriggerEnter(Collider other) {
            if (other.tag == "FloatArea") {
                stateMachine.ChangeState(stateMachine.floatState);
            }
        }

        // Transition states based off input

        protected virtual void StartJump() {
            movement.ChangeMovementSettings(jumpMoveSettings);
            jump.ChangeJumpSettings(jumpSettings);
            jump.InitialiseJump();
        }

        protected virtual void Idle() {
            stateMachine.ChangeState(stateMachine.idleState);
        }

        protected virtual void Run() {
            stateMachine.ChangeState(stateMachine.runState);
        }

        protected virtual void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected void GroundPound() {
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }

        protected virtual void Hover() {
            if (!grounded.IsOnGround) {
                stateMachine.ChangeState(stateMachine.hoverState);
            }
        }

        protected void Grapple() {
            stateMachine.ChangeState(stateMachine.swingState);
        }

        protected virtual void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}