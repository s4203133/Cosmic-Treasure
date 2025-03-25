using UnityEngine;

namespace LMO {

    public class PlayerGrappleState : PlayerBaseState {

        protected Transform playerTransform;
        protected Rigidbody rigidBody;

        protected Grapple grapple;
        protected SwingManager swingManager;
        protected DetectSwingJoints detectSwingJoints;

        private PlayerMovement movement;

        public PlayerGrappleState(PlayerController playerController) : base(playerController) {
            playerTransform = playerController.transform;
            rigidBody = playerController.RigidBody;

            grapple = playerController.playerGrapple;
            swingManager = playerController.playerSwingManager;

            movement = playerController.playerMovment;
        }

        public override void OnStateEnter() {
            Vector3 constraintPosition = grapple.ConnectedObject.transform.position;
            constraintPosition.y = playerTransform.position.y;
            grapple.ConnectJoint(constraintPosition);
        }

        public override void OnStateExit() {
        }

        public override void OnStatePhysicsUpdate() {

        }

        public override void OnStateUpdate() {

        }

        public override void OnTriggerEnter(Collider collider) {

        }

        protected void LookAtGrappleTarget() {
            Vector3 targetDirection = grapple.ConnectedObject.transform.position;
            targetDirection.y = playerTransform.position.y;
            playerTransform.LookAt(targetDirection);
        }

        protected void Spin() {
            grapple.ConnectedObject.Interact();
            grapple.DisconnectJoint();
            grapple.OnGrappleEnded?.Invoke();
            stateMachine.ChangeState(stateMachine.spinState);
        }
    }
}