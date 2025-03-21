using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class PlayerRunState : PlayerBaseState {

        private PlayerMovement movement;
        private Grounded grounded;
        private PlayerInput input;

        protected PlayerMovementSettings moveSettings;

        public PlayerRunState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            grounded = context.playerJump.groundedSystem;
            input = context.playerInput;
            moveSettings = movement.CurrentMovementSettings;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            movement.OnMoveStarted?.Invoke();
            // Apply regular movement variables to move component
            movement.ChangeMovementSettings(moveSettings);
            CheckForJumpInput();
        }

        public override void OnStateUpdate() {
            CheckIfPlayerLeftPlatform();
            CheckIfFinishedMoving();
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();

        }

        public override void OnStateExit() {
            InputHandler.jumpStarted -= Jump;
            InputHandler.SpinStarted -= Spin;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            movement.OnMoveStopped?.Invoke();
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        // Transition states based off input or environmental changes

        protected virtual void Idle() {
            stateMachine.ChangeState(stateMachine.idleState);
        }

        protected virtual void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        protected virtual void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        protected virtual void CheckIfPlayerLeftPlatform() {
            if (!grounded.IsOnGround) {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
        }

        protected virtual void CheckIfFinishedMoving() {
            if (input.moveInput == Vector2.zero) {
                if (movement.HasStopped()) {
                    Idle();
                }
            }
        }

        protected virtual void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected virtual void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}