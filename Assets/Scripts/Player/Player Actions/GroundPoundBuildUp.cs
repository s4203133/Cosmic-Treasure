using UnityEngine;

namespace LMO.Player {

    [System.Serializable]
    public class GroundPoundBuildUp : GroundPoundComponent {
        [SerializeField] private float buildUpDuration;
        private float buildUpTimer;
        private bool starting;

        PlayerGroundPound groundPound;
        private Rigidbody rigidBody;

        // When the player begins the ground pound movement, prepare any required variables
        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            groundPound = targetGroundPound;
            groundPound.OnGroundPoundInitialised += StartGroundPound;
            rigidBody = playerRigidbody;
        }

        public override void Disable() {
            groundPound.OnGroundPoundInitialised -= StartGroundPound;
        }

        public void StartGroundPound() {
            starting = true;
            buildUpTimer = buildUpDuration;
            DisableVelocity();
        }

        private void DisableVelocity() {
            rigidBody.velocity = Vector3.zero;
            rigidBody.useGravity = false;
        }

        // While the ground pound is starting, remove all velocity from the player
        public void HandleGroundPoundBuildUp() {
            if (starting) {
                rigidBody.velocity = Vector3.zero;
                CountdownBuildUp();
            }
        }

        // Once the build up has ended, the falling state of the ground pound can start
        private void CountdownBuildUp() {
            buildUpTimer -= Time.fixedDeltaTime;
            if (buildUpTimer <= 0) {
                EndGroundPoundBuildUp();
            }
        }

        private void EndGroundPoundBuildUp() {
            starting = false;
            rigidBody.useGravity = true;
        }

        public bool IsFinished() {
            return !starting;
        }

        public void ResetTimer() {
            buildUpTimer = buildUpDuration;
        }
    }
}