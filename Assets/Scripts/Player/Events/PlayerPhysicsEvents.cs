using UnityEngine;

namespace LMO {
    public class PlayerPhysicsEvents : MonoBehaviour, ICustomEvent {

        private PlayerController controller;
        private CameraShaker shaker;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            controller = player.Controller;
            shaker = player.CameraShake;
            audioManager = player.Controller.playerAudioManager;
        }

        public void SubscribeEvents() {
            Cannon.OnJumpingInCannon += JumpInCannon;
            Cannon.OnEnteredCannon += EnteredCannon;
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

        private void EnteredCannon() {
            audioManager.PlayEnterCannon();
        }

        private void LaunchCannon() {
            shaker.shakeTypes.large.Shake();
            audioManager.PlayLaunchCannon();
        }
    }
}