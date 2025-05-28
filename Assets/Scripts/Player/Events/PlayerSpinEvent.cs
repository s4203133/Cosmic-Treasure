using UnityEngine;

namespace LMO {

    public class PlayerSpinEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            audioManager = player.Controller.playerAudioManager;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            PlayerSpinAttack.OnSpin += StartSpin;
            PlayerSpinAttack.OnAirSpin += StartAirSpin;
        }

        public void UnsubscribeEvents() {
            PlayerSpinAttack.OnSpin -= StartSpin;
            PlayerSpinAttack.OnAirSpin -= StartAirSpin;
        }

        private void StartSpin() {
            
            playerVFX.PlaySpinVFX();
            squishy.SpinAttack.Play();
            audioManager.PlayAttack();
        }

        private void StartAirSpin() {
            playerVFX.PlaySpinVFX();
            squishy.SpinAttack.Play();
            audioManager.PlayAttack2();
        }
    }
}