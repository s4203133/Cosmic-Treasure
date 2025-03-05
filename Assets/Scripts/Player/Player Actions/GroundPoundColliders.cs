using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class GroundPoundColliders : GroundPoundComponent {
        [SerializeField] private Collider fallingCollider;
        [SerializeField] private Collider landCollider;

        private PlayerGroundPound groundPound;

        // Enable colliders accordingly based on state of ground pound
        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            groundPound = targetGroundPound;
            groundPound.OnGroundPoundStarted += EnableFallingCollider;
            groundPound.OnGroundPoundLanded += EnableLandingCollider;
            groundPound.OnGroundPoundFinished += DisableColliders;
        }

        public override void Disable() {
            groundPound.OnGroundPoundStarted -= EnableFallingCollider;
            groundPound.OnGroundPoundLanded -= EnableLandingCollider;
            groundPound.OnGroundPoundFinished -= DisableColliders;
        }

        // When the player begins falling, enable the collider that desroys any items beneath them
        public void EnableFallingCollider() {
            fallingCollider.enabled = true;
            landCollider.enabled = false;
        }

        // When the player has landed, enable the collider that destroys any surrounding objects
        public void EnableLandingCollider() {
            fallingCollider.enabled = false;
            landCollider.enabled = true;
        }

        // Once the ground pound has finishhed, disable both colliders
        public void DisableColliders() {
            fallingCollider.enabled = false;
            landCollider.enabled = false;
        }
    }
}