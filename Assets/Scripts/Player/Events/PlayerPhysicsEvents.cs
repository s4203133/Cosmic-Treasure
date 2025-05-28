using UnityEngine;

namespace LMO {
    public class PlayerPhysicsEvents : MonoBehaviour, ICustomEvent {

        private PlayerController controller;
        private CameraShaker shaker;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            controller = player.Controller;
            shaker = player.CameraShake;
        }

        public void SubscribeEvents() {
            Cannon.OnJumpingInCannon += JumpInCannon;
            Cannon.OnCannonLaunched += LaunchCannon;
        }

        public void UnsubscribeEvents() {
            Cannon.OnJumpingInCannon -= JumpInCannon;
            Cannon.OnCannonLaunched -= LaunchCannon;
        }

        private void JumpInCannon() {
            controller.DisablePhysics();
            controller.playerStateMachine.Deactivate();
        }

        private void LaunchCannon() {
            shaker.shakeTypes.large.Shake();
        }
    }
}