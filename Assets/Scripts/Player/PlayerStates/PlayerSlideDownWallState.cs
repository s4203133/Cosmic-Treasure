using UnityEngine;

namespace LMO {

    public class PlayerSlideDownWallState : PlayerBaseState {

        private Transform thisTransform;
        private PlayerJump jump;
        private Grounded grounded;
        private PlayerSpinAttack spin;
        private PlayerInput input;
        private PlayerWallJump wallJump;
        private Rigidbody rigidBody;

        private PlayerJumpSettings fallSettings;

        private float startFallingDelay;
        private float timer;

        public PlayerSlideDownWallState(PlayerController playerController) : base(playerController) {
            thisTransform = context.transform;
            jump = context.playerJump;
            grounded = jump.groundedSystem;
            spin = context.playerSpinAttack;
            input = context.playerInput;
            wallJump = context.playerWallJump;
            rigidBody = context.RigidBody;

            fallSettings = context.PlayerSettings.SlideDownWall;

            startFallingDelay = 0.25f;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            jump.ChangeJumpSettings(fallSettings);
            CheckForSpinInput();
            timer = startFallingDelay;
            startFallingDelay = 0.25f;
        }

        public override void OnStatePhysicsUpdate() {
            if (timer > 0) {
                if (PlayerStoppedMovingTowardsWall()) {
                    return;
                }
                rigidBody.velocity = new Vector3(0, 1.5f, 0);
                return;
            }
            jump.ApplyFallForce();
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        }

        public override void OnStateUpdate() {
            thisTransform.LookAt(wallJump.PlayerFaceWallDirection());

            timer -= TimeValues.Delta;
            if(timer > 0) {
                if (PlayerStoppedMovingTowardsWall()) {
                    return;
                }
                rigidBody.velocity = new Vector3(0, 1.5f, 0);
                return;
            }

            // If the player has landed on the ground, determine which state to transition them to
            if (grounded.IsOnGround) {
                if (input.moveInput == Vector2.zero) {
                    stateMachine.ChangeState(stateMachine.idleState);
                }
                else {
                    stateMachine.ChangeState(stateMachine.runState);
                }
            }
            else {
                PlayerStoppedMovingTowardsWall();
            }
        }

        public override void OnStateExit() {
            InputHandler.jumpStarted -= Jump;
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

        private bool PlayerStoppedMovingTowardsWall() {
            wallJump.CheckForWalls();
            if (!wallJump.WallInFrontOfPlayer || input.moveInput == Vector2.zero) {
                stateMachine.Fall();
                return true;
            }
            return false;
        }

        private void Jump() {
            wallJump.CalculateJumpDirection();
            stateMachine.ChangeState(stateMachine.wallJumpState);
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

        private void GroundPound() {
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }

        private void Grapple() {
            stateMachine.ChangeState(stateMachine.swingState);
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}