using UnityEngine;

namespace LMO {

    public class PlayerSwingState : PlayerBaseState {

        private PlayerSwing swing;
        private SwingManager swingManager;
        private PlayerMovement movement;
        private Grapple grapple;

        public delegate void CustomEvent();
        public delegate void CustomEventT(Transform target);

        public static CustomEvent OnSwingStart;
        public static CustomEvent OnJumpFromSwing;
        public static CustomEvent OnSwingEnd;

        public PlayerSwingState(PlayerController playerController) : base(playerController) {
            swing = playerController.playerSwing;
            swingManager = playerController.playerSwingManager;
            movement = playerController.playerMovment;
            grapple = playerController.playerGrapple;
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

            StartSwing();
        }

        public override void OnStatePhysicsUpdate() {
            swing.PerformSwing();
        }

        public override void OnStateExit() {
            InputHandler.grappleStarted -= DisconnectGrapple;
            InputHandler.jumpStarted -= JumpFromGrapple;

            EndSwing();
        }

        public override void OnStateUpdate() {
            swing.CountdownConnectionTimer();
        }

        private void StartSwing() {
            movement.ChangeMovementSettings(swing.MovementSettings);
            OnSwingStart?.Invoke();

            // If 'objectCurrentlyGrappledOnto' is null, then the 'OnGrappleStarted' event will assign it
            if (grapple.objectCurrentlyGrappledOnto == null) {
                Grapple.OnGrappleStarted?.Invoke();
            }
        }

        private void EndSwing() {
            OnSwingEnd?.Invoke();
            Grapple.OnGrappleEnded?.Invoke();
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