using UnityEngine;

namespace LMO {

    public class PlayerFloat : MonoBehaviour {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float airForce;
        [SerializeField] private float maxAirVelocity;
        [SerializeField] private AnimationCurve airAcceleration;

        private bool addForce;
        private float timer;

        [Space(15)]
        public LayerMask floatingTriggerLayers;

        public void StartFloating() {
            timer = 0;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            addForce = true;
        }

        public void ApplyForce() {
            if (!addForce) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 1f, rigidBody.velocity.z);
                return;
            }
            float airVelocity = airAcceleration.Evaluate(timer) * airForce;
            float newYVel = Mathf.Min(rigidBody.velocity.y + airVelocity, maxAirVelocity);
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, newYVel, rigidBody.velocity.z);

            timer += TimeValues.FixedDelta;
        }

        public void StopAddingForce() {
            addForce = false;
        }
    }
}