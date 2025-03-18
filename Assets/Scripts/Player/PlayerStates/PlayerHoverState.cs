using UnityEngine;

namespace LMO {

    public class PlayerHoverState : PlayerBaseState {

        PlayerHover hover;
        PlayerMovement movement;

        PlayerMovementSettings moveSettings;

        public PlayerHoverState(PlayerController playerController) : base(playerController) {
            hover = context.playerHover;
            movement = context.playerMovment;
            moveSettings = hover.movementSettings;
        }

        public override void OnStateEnter() {
            // Validate that the hover move can be performed
            if (!hover.CanHover) {
                stateMachine.ChangeState(stateMachine.fallingState);
                return;
            }

            InputHandler.jumpCancelled += hover.CuttOffHover;
            InputHandler.jumpCancelled += TransitionToFallState;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.grappleStarted += Grapple;

            // Change the variables used in the movement component
            movement.ChangeMovementSettings(moveSettings);
            hover.StartHover();
        }

        public override void OnStateUpdate() {
            if (hover.finished) {
                TransitionToFallState();
            }
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
            hover.ApplyHoverForce();
        }

        public override void OnStateExit() {
            InputHandler.groundPoundStarted -= GroundPound;
            InputHandler.jumpCancelled -= TransitionToFallState;
            InputHandler.jumpCancelled -= hover.CuttOffHover;
            InputHandler.grappleStarted -= Grapple;
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        private void TransitionToFallState()
        {
            stateMachine.ChangeState(stateMachine.fallingState);
        }

        // If ground pounding, finish hover and change states
        private void GroundPound() {
            hover.CuttOffHover();
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }

        private void Grapple() {
            hover.CuttOffHover();
            stateMachine.ChangeState(stateMachine.swingState);
        }
    }
}