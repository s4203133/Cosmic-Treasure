using UnityEngine;

namespace LMO.Player {

public class PlayerSwingState : PlayerBaseState {

        PlayerSwing swing;
        SwingManager swingManager;

        PlayerMovement movement;

        public delegate void CustomEvent();
        public delegate void CustomEventV3(Vector3 targetPosition);

        public static CustomEventV3 OnSwingStart;
        public static CustomEvent OnJumpFromSwing;
        public static CustomEvent OnSwingEnd;

        public PlayerSwingState(PlayerController playerController) : base(playerController) {
            swing = playerController.playerSwing;
            swingManager = playerController.playerSwingManager;
            movement = playerController.playerMovment;
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        public override void OnStateEnter() {
            if(!swingManager.CanSwing) {
                stateMachine.ChangeState(stateMachine.fallingState);
                return;
            }

            InputHandler.grappleStarted += DisconnectGrapple;
            InputHandler.jumpStarted += JumpFromGrapple;

            movement.ChangeMovementSettings(swing.MovementSettings);
            OnSwingStart?.Invoke(swingManager.SwingTarget.transform.position);
        }

        public override void OnStatePhysicsUpdate() {
            swing.PerformSwing();
        }

        public override void OnStateExit() {
            InputHandler.grappleStarted -= DisconnectGrapple;
            InputHandler.jumpStarted -= JumpFromGrapple;

            OnSwingEnd?.Invoke();
        }

        public override void OnStateUpdate() {
            swing.CountdownConnectionTimer();
        }

        private void DisconnectGrapple() {
            stateMachine.ChangeState(stateMachine.fallingState);
        }

        private void JumpFromGrapple() {
            OnJumpFromSwing?.Invoke();
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }
}