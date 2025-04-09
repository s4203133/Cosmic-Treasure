using UnityEngine;
using NR;

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
            //<NR>
            SlingshotJoint grappleSlingshot = grapple.ConnectedObject as SlingshotJoint;
            if (grappleSlingshot != null) {
                constraintPosition = grappleSlingshot.SlingshotTransform.position;
            }
            //</NR>
            constraintPosition.y = playerTransform.position.y;
            grapple.ConnectJoint(constraintPosition);

            PlayerDeath.OnPlayerDied += grapple.DisconnectJoint;
        }

        public override void OnStateExit() {
            PlayerDeath.OnPlayerDied -= grapple.DisconnectJoint;
        }   

        public override void OnStatePhysicsUpdate() {

        }

        public override void OnStateUpdate() {

        }

        public override void OnTriggerEnter(Collider collider) {

        }

        protected void LookAtGrappleTarget() {
            if(grapple.ConnectedObject.transform == null) {
                return;
            }
            Vector3 targetDirection = grapple.ConnectedObject.transform.position;
            targetDirection.y = playerTransform.position.y;
            playerTransform.LookAt(targetDirection);
        }

        protected void Spin() {
            grapple.ConnectedObject.Interact();
            grapple.DisconnectJoint();
            Grapple.OnGrappleEnded?.Invoke();
            stateMachine.ChangeState(stateMachine.spinState);
        }
    }
}