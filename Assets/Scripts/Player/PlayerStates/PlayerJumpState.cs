using UnityEngine;

namespace LMO {

    public class PlayerJumpState : PlayerBaseState {

        private PlayerMovement movement;
        protected PlayerJump jump;
        private Grounded grounded;
        private PlayerInput input;
        protected PlayerJumpSettings jumpSettings;

        public PlayerJumpState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            input = context.playerInput;
            jumpSettings = context.PlayerSettings.Jump;
        }

        public override void OnStateEnter() {
            InputHandler.jumpCancelled += jump.CutOffJump;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.jumpStarted += Hover;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

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
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        public override void OnTriggerEnter(Collider other) {

        }

        // Transition states based off input

        protected virtual void StartJump() {
            jump.ChangeJumpSettings(jumpSettings);
            jump.InitialiseJump();
        }

        protected void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected void GroundPound() {
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }

        protected void Hover() {
            if (!grounded.IsOnGround) {
                stateMachine.ChangeState(stateMachine.hoverState);
            }
        }

        protected void Grapple() {
            stateMachine.ChangeState(stateMachine.swingState);
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}