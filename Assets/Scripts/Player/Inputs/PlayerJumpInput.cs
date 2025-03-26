
namespace LMO {

    [System.Serializable]
    public class PlayerJumpInput {

        public PlayerJumpState playerJump;

        private void StartJump() {
        }

        private void PerformJump() {
        }

        private void StopJump() {
        }

        public void SubscribeJumpEvents() {
            InputHandler.jumpStarted += StartJump;
            InputHandler.jumpPerformed += PerformJump;
            InputHandler.jumpCancelled += StopJump;
        }

        public void UnsubscribeJumpEvents() {
            InputHandler.jumpStarted -= StartJump;
            InputHandler.jumpPerformed -= PerformJump;
            InputHandler.jumpCancelled -= StopJump;
        }
    }
}