using UnityEngine;

namespace LMO {

    public class PlayerGroundedEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private PlayerSpinAttack playerSpin;
        private PlayerHover playerHover;
        private FOVChanger fovChanger;
        private PlayerMovement movement;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            playerSpin = player.Controller.playerSpinAttack;
            playerHover = player.Controller.playerHover;
            fovChanger = player.FOV_Changer;
            movement = player.Controller.playerMovment;
        }

        public void SubscribeEvents() {
            Grounded.OnLanded += OnPLayerLanded;
        }

        public void UnsubscribeEvents() {
            Grounded.OnLanded -= OnPLayerLanded;
        }

        public void OnPLayerLanded() {
            playerVFX.PlayLandParticles();
            squishy.Land.Play();
            playerSpin.ResetAirSpins();
            playerHover.EnableHover();
            fovChanger.EndChange();
            movement.FinishedMoving();
        }
    }
}