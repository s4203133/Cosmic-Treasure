using UnityEngine;

namespace LMO.Player {

    public class PlayerJumpState : PlayerBaseState {

        private PlayerMovement movement;
        protected PlayerJump jump;
        private Grounded grounded;
        private PlayerInput input;

        public PlayerJumpState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            input = context.playerInput;
        }

        public override void OnStateEnter() {
            InputHandler.jumpCancelled += jump.CutOffJump;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.jumpStarted += Hover;
            InputHandler.grappleStarted += Grapple;

            StartJump();
        }

        public override void OnStateUpdate() {
            // Once the player has landed on the ground, determine which state to transition to
            if (grounded.IsOnGround) {
                if (input.moveInput == Vector2.zero) {
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
            InputHandler.grappleStarted -= Grapple;

            jump.EndJump();
        }

        public override void OnTriggerEnter(Collider other) {

        }

        // Transition states based off input

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

        private void Grapple() {
            stateMachine.ChangeState(stateMachine.swingState);
        }
    }
}