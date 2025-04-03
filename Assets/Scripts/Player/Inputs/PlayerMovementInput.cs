using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class PlayerMovementInput {

        public PlayerMovement playerMovement;

        private void StartMovement(Vector2 value) {
            PlayerMovement.OnMoveStarted?.Invoke();
            playerMovement.isStopping = false;
        }

        private void StopMovement(Vector2 value) {
            playerMovement.ResetVelocityVariables();
        }

        public void SubscribeMoveEvents() {
            InputHandler.moveStarted += StartMovement;
            InputHandler.moveCancelled += StopMovement;
        }

        public void UnsubscribeMoveEvents() {
            InputHandler.moveStarted -= StartMovement;
            InputHandler.moveCancelled -= StopMovement;
        }
    }
}