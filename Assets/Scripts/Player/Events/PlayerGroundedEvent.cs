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
        private PlayerAudioManager audioManager;
        private PlayerInput input;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            playerSpin = player.Controller.playerSpinAttack;
            playerHover = player.Controller.playerHover;
            fovChanger = player.FOV_Changer;
            movement = player.Controller.playerMovment;
            audioManager = player.Controller.playerAudioManager;
            input = player.Controller.playerInput;
        }

        public void SubscribeEvents() {
            Grounded.OnLanded += OnPLayerLanded;
            Grounded.OnLeftGround += LeftGround;
        }

        public void UnsubscribeEvents() {
            Grounded.OnLanded -= OnPLayerLanded;
            Grounded.OnLeftGround -= LeftGround;
        }

        public void OnPLayerLanded() {
            playerVFX.PlayLandParticles();
            squishy.Land.Play();
            playerSpin.ResetAirSpins();
            playerHover.EnableHover();
            fovChanger.EndChange();
            movement.FinishedMoving();
            audioManager.PlayLand();
            if(input.moveInput != Vector2.zero) {
                audioManager.PlayRunning();
            }
        }

        private void LeftGround() {
            audioManager.StopRunning();
        }
    }
}