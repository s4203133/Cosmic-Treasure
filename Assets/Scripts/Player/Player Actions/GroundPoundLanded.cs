using UnityEngine;

namespace LMO.Player {

    [System.Serializable]
    public class GroundPoundLanded : GroundPoundComponent {
        [SerializeField] private float landDuration;
        private float landTimer;

        private Grounded grounded;

        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            this.grounded = grounded;
        }

        // if the player has landed on the ground, reset the timer
        public bool CheckLanded() {
            if (grounded.IsOnGround) {
                landTimer = landDuration;
                return true;
            }
            return false;
        }

        public void HandleLand() {
            landTimer -= Time.fixedDeltaTime;
        }

        public bool FinishedLand() {
            return (landTimer <= 0);
        }

        public override void Disable() {

        }
    }
}