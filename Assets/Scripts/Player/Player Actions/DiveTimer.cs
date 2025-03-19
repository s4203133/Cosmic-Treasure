using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class DiveTimer : GroundPoundComponent {
        // How long of the action needs to run before allowing the player to dive
        [SerializeField] private float timeUntilDiveCanStart;
        [HideInInspector] public bool canInitiateDive;
        private float allowDiveTimer;

        PlayerGroundPound groundPound;

        // Start the timer to allow the player to dive when the player begins ground pounding,
        // and cancel it once they land on the ground
        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            groundPound = targetGroundPound;
            groundPound.OnGroundPoundInitialised += InitialiseTimer;
            groundPound.OnGroundPoundLanded += DoNotAllowDive;
        }

        public override void Disable() {
            groundPound.OnGroundPoundInitialised -= InitialiseTimer;
            groundPound.OnGroundPoundLanded -= DoNotAllowDive;

        }

        private void InitialiseTimer() {
            allowDiveTimer = timeUntilDiveCanStart;
            canInitiateDive = false;
        }

        public void CountdownTimer() {
            allowDiveTimer -= TimeValues.FixedDelta;
            if (allowDiveTimer <= 0) {
                canInitiateDive = true;
            }
        }

        private void DoNotAllowDive() {
            canInitiateDive = false;
        }
    }
}