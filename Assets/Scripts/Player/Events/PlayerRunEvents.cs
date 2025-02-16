using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerRunEvents : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerMovement playerMovement;

        // Observers
        private PlayerVFX playerVFX;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
        }

        public void SubscribeEvents() {
            if (playerMovement == null) {
                return;
            }
            playerMovement.OnMoveStarted += playerVFX.StartRunParticles;
            playerMovement.OnMoveStopped += playerVFX.StopRunParticles;
        }

        public void UnsubscribeEvents() {
            if (playerMovement == null) {
                return;
            }
            playerMovement.OnMoveStarted -= playerVFX.StartRunParticles;
            playerMovement.OnMoveStopped -= playerVFX.StopRunParticles;
        }
    }
}