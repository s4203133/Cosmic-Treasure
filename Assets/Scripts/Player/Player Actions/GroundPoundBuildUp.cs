using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class GroundPoundBuildUp : GroundPoundComponent {
        [SerializeField] private float buildUpDuration;
        [SerializeField] private float buildUpAirForce;
        [SerializeField] private AnimationCurve buildUpAirBoost;
        private float buildUpTimer;
        private bool starting;

        PlayerGroundPound groundPound;
        private Rigidbody rigidBody;

        // When the player begins the ground pound movement, prepare any required variables
        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            groundPound = targetGroundPound;
            PlayerGroundPound.OnGroundPoundInitialised += StartGroundPound;
            rigidBody = playerRigidbody;
        }

        public override void Disable() {
            PlayerGroundPound.OnGroundPoundInitialised -= StartGroundPound;
        }

        public void StartGroundPound() {
            starting = true;
            buildUpTimer = 0;
            DisableVelocity();
        }

        private void DisableVelocity() {
            rigidBody.velocity = Vector3.zero;
            rigidBody.useGravity = false;
        }

        // While the ground pound is starting, remove all velocity from the player
        public void HandleGroundPoundBuildUp() {
            if (starting) {
                float t = (buildUpTimer * (100 / buildUpDuration)) / 100;
                float airForce = buildUpAirBoost.Evaluate(t) * buildUpAirForce;
                rigidBody.velocity = new Vector3(0, airForce, 0);
                CountdownBuildUp();
            }
        }

        // Once the build up has ended, the falling state of the ground pound can start
        private void CountdownBuildUp() {
            buildUpTimer += TimeValues.FixedDelta;
            if (buildUpTimer >= buildUpDuration) {
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