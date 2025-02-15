using UnityEngine;

namespace LMO.Player {

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
            if (!hover.CheckCanHover) {
                stateMachine.ChangeState(stateMachine.fallingState);
                return;
            }

            InputHandler.jumpCancelled += hover.CuttOffHover;
            InputHandler.groundPoundStarted += GroundPound;

            // Change the variables used in the movement component
            movement.ChangeMovementSettings(moveSettings);
            hover.StartHover();
        }

        public override void OnStateUpdate() {
            if (hover.finished) {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
            hover.ApplyHoverForce();
        }

        public override void OnStateExit() {
            InputHandler.groundPoundStarted -= GroundPound;
            InputHandler.jumpCancelled -= hover.CuttOffHover;

            hover.EndHover();
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        // If ground pounding, finish hover and change states
        private void GroundPound() {
            hover.CuttOffHover();
            stateMachine.ChangeState(stateMachine.groundPoundState);
        }
    }
}