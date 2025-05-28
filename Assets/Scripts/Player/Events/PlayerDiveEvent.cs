using UnityEngine;

namespace LMO {

    public class PlayerDiveEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private HighJumpTrail jumpTrail;
        private FOVChanger fovChanger;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            jumpTrail = player.Trail;
            fovChanger = player.FOV_Changer;
            audioManager = player.Controller.playerAudioManager;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            PlayerDive.OnDive += OnPLayerDive;
        }

        public void UnsubscribeEvents() {
            PlayerDive.OnDive -= OnPLayerDive;
        }

        private void OnPLayerDive() {
            playerVFX.PlayDiveVFX();
            squishy.Dive.Play();
            jumpTrail.StartTrail();
            fovChanger.StartChange();
            audioManager.PlayDive();
            audioManager.StopGroundPound();
        }
    }
}