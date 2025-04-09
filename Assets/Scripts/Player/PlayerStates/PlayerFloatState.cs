using UnityEngine;

namespace LMO {

    public class PlayerFloatState : PlayerBaseState {

        private Transform thisTransform;
        private PlayerMovement movement;
        private PlayerFloat playerFloat;
        private LayerMask floatingTriggerLayers;

        public PlayerFloatState(PlayerController playerController) : base(playerController) {
            thisTransform = playerController.transform;
            movement = playerController.playerMovment;
            playerFloat = playerController.playerFloat;
            floatingTriggerLayers = playerFloat.floatingTriggerLayers;
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += Hover;
            InputHandler.SpinStarted += Spin;

            playerFloat.StartFloating();
        }

        public override void OnStateExit() {
            InputHandler.jumpStarted -= Hover;
            InputHandler.SpinStarted -= Spin;
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
            playerFloat.ApplyForce();
        }

        public override void OnStateUpdate() {
            CheckStillInFloatingPlatform();
        }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnTriggerExit(Collider collider) {
            if(collider.tag == "FloatArea") {
                playerFloat.StopAddingForce();
            }
        }

        private void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }

        private void CheckStillInFloatingPlatform() {
            Collider[] floatingArea = Physics.OverlapSphere(thisTransform.position, 0.5f, floatingTriggerLayers);
            if (floatingArea.Length == 0) {
                stateMachine.Fall();
            }
        }

        private void Hover() {
            stateMachine.ChangeState(stateMachine.hoverState);
        }
    }
}