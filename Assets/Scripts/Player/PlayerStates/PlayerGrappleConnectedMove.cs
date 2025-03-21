using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedMove : PlayerRunState {

        private SwingManager swingManager;
        private Grounded grounded;
        private Transform thisTransform;
        private Grapple grapple;
        private Rigidbody rigidBody;

        private float notOnGroundTimer;

        public PlayerGrappleConnectedMove(PlayerController playerController) : base(playerController) {
            moveSettings = playerController.PlayerSettings.GrappleMoveSettings;
            swingManager = context.playerSwingManager;
            grounded = context.playerJump.groundedSystem;
            thisTransform = playerController.transform;
            grapple = playerController.playerGrapple;
            rigidBody = playerController.RigidBody;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();
            notOnGroundTimer = 0.3f;
        }

        public override void OnStateExit() {
            base.OnStateExit();
        }

        public override void OnStateUpdate() {
            base.OnStateUpdate();
            if (grounded.IsOnGround) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, Physics.gravity.y, rigidBody.velocity.z);
            }
            Vector3 targetDirection = swingManager.SwingTarget.transform.position;
            targetDirection.y = thisTransform.position.y;
            thisTransform.LookAt(targetDirection);
        }
        protected override void Idle() {
            stateMachine.ChangeState(stateMachine.grappleIdle);
        }

        protected override void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        protected override void Spin() {
            grapple.OnGrappleEnded?.Invoke();
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected override void CheckIfPlayerLeftPlatform() {
            if (!grounded.IsOnGround) {
                notOnGroundTimer -= TimeValues.Delta;
                if(notOnGroundTimer <= 0) { 
                    stateMachine.ChangeState(stateMachine.swingState);
                }
            }
            else {
                notOnGroundTimer = 0.3f;
            }
        }

        protected override void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        protected override void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }

        protected override void Grapple() {
            
        }
    }
}