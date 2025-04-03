using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedMove : PlayerGrappleState {

        private PlayerMovement movement;
        private Grounded grounded;
        private PlayerInput input;

        private PlayerMovementSettings movementSettings;

        private float notOnGroundTimer;

        public PlayerGrappleConnectedMove(PlayerController playerController) : base(playerController) {
            movement = playerController.playerMovment;
            grounded = context.playerJump.groundedSystem;
            input = playerController.playerInput;

            movementSettings = playerController.PlayerSettings.GrappleMoveSettings;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            PlayerMovement.OnMoveStarted?.Invoke();
            // Apply regular movement variables to move component
            movement.ChangeMovementSettings(movementSettings);
            CheckForJumpInput(); 
            
            notOnGroundTimer = 0.3f;
        }

        public override void OnStateExit() {
            base.OnStateExit();
            InputHandler.jumpStarted -= Jump;
            InputHandler.SpinStarted -= Spin;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            //PlayerMovement.OnMoveStopped?.Invoke();
        }

        public override void OnStateUpdate() {
            CheckIfPlayerLeftPlatform();
            CheckIfFinishedMoving();

            LookAtGrappleTarget();
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
        }

        private void Idle() {
            stateMachine.ChangeState(stateMachine.grappleIdle);
        }

        private void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        private void CheckIfPlayerLeftPlatform() {
            if (!grounded.IsOnGround) {
                notOnGroundTimer -= TimeValues.Delta;
                if(notOnGroundTimer <= 0) {
                    grapple.DisconnectJoint();
                    stateMachine.ChangeState(stateMachine.swingState);
                }
            }
            else {
                notOnGroundTimer = 0.3f;
            }
        }

        private void CheckIfFinishedMoving() {
            if (input.moveInput == Vector2.zero) {
                if (movement.HasStopped()) {
                    Idle();
                }
            }
        }

        private void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}