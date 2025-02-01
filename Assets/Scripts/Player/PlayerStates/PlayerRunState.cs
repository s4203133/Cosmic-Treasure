using UnityEngine;

namespace LMO.Player {

    [System.Serializable]
    public class PlayerRunState : PlayerBaseState {

        private PlayerMovement movement;
        private Grounded grounded;
        private PlayerInput input;

        private PlayerMovementSettings moveSettings;

        public PlayerRunState(PlayerController playerController) : base(playerController) {
            movement = context.playerMovment;
            grounded = context.playerJump.groundedSystem;
            input = context.playerInput;
            moveSettings = movement.CurrentMovementSettings;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;

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

            movement.OnMoveStopped?.Invoke();
        }

        public override void OnCollisionEnter(Collision collision) {

        }

        // Transition states based off input or environmental changes

        private void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        private void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        private void CheckIfPlayerLeftPlatform() {
            if (!grounded.IsOnGround) {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
        }

        private void CheckIfFinishedMoving() {
            if (input.moveInput == Vector2.zero) {
                if (movement.HasStopped()) {
                    stateMachine.ChangeState(stateMachine.idleState);
                }
            }
        }

        private void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }
    }
}