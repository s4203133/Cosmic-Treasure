using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerGroundedEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private PlayerSpinAttack playerSpin;
        private PlayerHover playerHover;
        private FOVChanger fovChanger;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            playerSpin = player.Controller.playerSpinAttack;
            playerHover = player.Controller.playerHover;
            fovChanger = player.FOV_Changer;
        }

        public void SubscribeEvents() {
            Grounded.OnLanded += OnPLayerLanded;
        }

        public void UnsubscribeEvents() {
            Grounded.OnLanded -= OnPLayerLanded;
        }

        public void OnPLayerLanded() {
            playerVFX.PlayLandParticles();
            squishy.GroundPound.Play();
            playerSpin.ResetAirSpins();
            playerHover.EnableHover();
            fovChanger.EndChange();
        }
    }
}