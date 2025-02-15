using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerGroundedEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private Grounded playerGrounded;

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

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerGrounded == null) {
                return;
            }
            playerGrounded.OnLanded += playerVFX.PlayLandParticles;
            playerGrounded.OnLanded += squishy.GroundPound.Play;
            playerGrounded.OnLanded += playerSpin.ResetAirSpins;
            playerGrounded.OnLanded += playerHover.EnableHover;
            playerGrounded.OnLanded += fovChanger.EndChange;
        }

        public void UnsubscribeEvents() {
            if (playerGrounded == null) {
                return;
            }
            playerGrounded.OnLanded -= playerVFX.PlayLandParticles;
            playerGrounded.OnLanded -= squishy.GroundPound.Play;
            playerGrounded.OnLanded -= playerSpin.ResetAirSpins;
            playerGrounded.OnLanded -= playerHover.EnableHover;
            playerGrounded.OnLanded -= fovChanger.EndChange;
        }
    }
}