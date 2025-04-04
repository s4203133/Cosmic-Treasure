using UnityEngine;

namespace LMO {

    public class PlayerSpinEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            PlayerSpinAttack.OnSpin += StartSpin;
        }

        public void UnsubscribeEvents() {
            PlayerSpinAttack.OnSpin -= StartSpin;
        }

        private void StartSpin() {
            playerVFX.PlaySpinVFX();
            squishy.SpinAttack.Play();
        }
    }
}