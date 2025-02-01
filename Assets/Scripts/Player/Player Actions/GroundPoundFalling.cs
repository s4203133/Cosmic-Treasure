using UnityEngine;

namespace LMO.Player {

    [System.Serializable]
    public class GroundPoundFalling : GroundPoundComponent {
        [SerializeField] private float groundPoundSpeed;

        private Rigidbody rigidBody;

        public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
            rigidBody = playerRigidbody;
        }

        // While the ground pound is performing, apply a downwards force to the player, and check if they've reached the ground
        public void HandleGroundPoundFalling() {
            rigidBody.velocity = new Vector3(0, -groundPoundSpeed, 0);
        }

        public override void Disable() {

        }
    }
}