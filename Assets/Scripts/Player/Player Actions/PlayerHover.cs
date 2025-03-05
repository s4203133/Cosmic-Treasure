using System;
using UnityEngine;

namespace LMO {

    public class PlayerHover : MonoBehaviour {

        [Header("HOVER SETTINGS")]
        public PlayerMovementSettings movementSettings;
        [SerializeField] private FloatVariable maxHoverDuration;
        [SerializeField] private FloatVariable timer;
        public bool finished => timer.value <= 0;
        private bool hoverEnded;

        [SerializeField] private AnimationCurve AirForce;
        [SerializeField] private float airBoost;
        private float airBoostTimer;
        private float yVelocityLimit;

        private bool canHover;
        public bool CheckCanHover => canHover;

        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody rigidBody;

        public static Action OnHoverStarted;
        public static Action OnHoverEnded;

        // Initialise hover movement
        public void StartHover() {
            OnHoverStarted?.Invoke();
            
            canHover = false;
            hoverEnded = false;
            rigidBody.velocity = Vector3.zero;
            timer.value = maxHoverDuration.value;
            airBoostTimer = 0;
        }

        // Apply velocity to the y component of the players rigidbody
        public void ApplyHoverForce() {
            ApplyAirBoost();
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocityLimit, rigidBody.velocity.z);
            timer.value -= Time.fixedDeltaTime;
        }

        // Get air force based off current value of the animation curve
        public void ApplyAirBoost() {
            float time = (airBoostTimer * (100f / maxHoverDuration.value)) / 100f;
            yVelocityLimit = AirForce.Evaluate(time) * airBoost;
            airBoostTimer += Time.fixedDeltaTime;
        }

        // If player releases input button early, stop the hover
        public void CuttOffHover() {
            timer.value = 0;
            airBoostTimer = maxHoverDuration.value;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
        }

        // Finish the hover movement
        public void EndHover() {
            if (!hoverEnded) {
                OnHoverEnded?.Invoke();
                hoverEnded = true;
            }
        }

        public void EnableHover() {
            canHover = true;
        }
    }
}