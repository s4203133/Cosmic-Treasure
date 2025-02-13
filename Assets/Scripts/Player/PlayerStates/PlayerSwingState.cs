using UnityEngine;

namespace LMO.Player {

public class PlayerSwingState : PlayerBaseState {

        PlayerSwing swing;
        GameObject targetPoint;

        public PlayerSwingState(PlayerController playerController) : base(playerController) {
            swing = playerController.playerSwing;
            targetPoint = playerController.testSwingObject;
        }

        public override void OnCollisionEnter(Collision collision) {

        }

        public override void OnStateEnter() {
            InputHandler.grappleStarted += DisconnectGrapple;

            swing.StartSwing(targetPoint);
        }

        public override void OnStateExit() {
            InputHandler.grappleStarted -= DisconnectGrapple;

            swing.EndSwing();
        }

        public override void OnStatePhysicsUpdate() {
            swing.PerformSwing();
        }

        public override void OnStateUpdate() {
        }

        private void DisconnectGrapple() {
            stateMachine.ChangeState(stateMachine.fallingState);
        }
    }
}