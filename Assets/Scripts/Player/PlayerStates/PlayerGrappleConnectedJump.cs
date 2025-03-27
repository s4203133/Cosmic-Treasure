using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedJump : PlayerGrappleState {

        private PlayerMovement movement;
        protected PlayerJump jump;
        private Grounded grounded;
        private PlayerInput input;

        private float timer;

        public PlayerGrappleConnectedJump(PlayerController playerController) : base(playerController) {
            movement = playerController.playerMovment;
            jump = playerController.playerJump;
            grounded = playerController.playerJump.groundedSystem;
            input = context.playerInput;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();
            InputHandler.jumpCancelled += jump.CutOffJump;
            InputHandler.SpinStarted += Spin;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            timer = 0.5f;
            StartJump();
        }

        public override void OnStateUpdate() {
            LookAtGrappleTarget();
            CheckIfPlatformIsUnderneathPlayer();

            // Once the player has landed on the ground, determine which state to transition to
            if (grounded.IsOnGround) {
                if (input.moveInput == Vector2.zero) {
                    Idle();
                }
                else {
                    Run();
                }
            }
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
            jump.ApplyForce();
        }

        public override void OnStateExit() {
            base.OnStateExit();

            InputHandler.jumpCancelled -= jump.CutOffJump;
            InputHandler.SpinStarted -= Spin;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        protected virtual void StartJump() {
            jump.InitialiseJump();
        }

        private void Idle() {
            stateMachine.ChangeState(stateMachine.grappleIdle);
        }

        private void Run() {
            stateMachine.ChangeState(stateMachine.grappleRun);
        }

        protected virtual void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }

        private void CheckIfPlatformIsUnderneathPlayer() {
            RaycastHit hit;
            if(Physics.Raycast(playerTransform.position + Vector3.up, Vector3.down, out hit, 5f, grounded.DetectableLayers)) {
                timer = 0.15f;
            } else {
                timer -= TimeValues.Delta;
                if (timer <= 0) {
                    grapple.DisconnectJoint();
                    stateMachine.ChangeState(stateMachine.swingState);
                }
            }
        }
    }
}