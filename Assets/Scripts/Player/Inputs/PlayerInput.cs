using UnityEngine;

namespace LMO {

    public class PlayerInput : MonoBehaviour {
        public Vector2 moveInput;

        [SerializeField] private PlayerMovementInput playerMoveInput;

        public void OnEnable() {
            playerMoveInput.SubscribeMoveEvents();
            SubscribeInputEvents();
        }

        public void OnDisable() {
            playerMoveInput.UnsubscribeMoveEvents();
            UnsubscribeInputEvents();
        }

        private void SubscribeInputEvents() {
            InputHandler.moveStarted += GetMovement;
            InputHandler.movePerformed += GetMovement;
            InputHandler.moveCancelled += GetMovement;
        }

        private void UnsubscribeInputEvents() {
            InputHandler.moveStarted -= GetMovement;
            InputHandler.movePerformed -= GetMovement;
            InputHandler.moveCancelled -= GetMovement;
        }

        private void GetMovement(Vector2 value) {
            moveInput = value;
        }
    }
}
