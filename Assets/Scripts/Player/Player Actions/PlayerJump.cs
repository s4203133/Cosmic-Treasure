using System;
using UnityEngine;

namespace LMO {

    public class PlayerJump : MonoBehaviour {

        [SerializeField] private PlayerJumpSettings settings;
        public PlayerJumpSettings JumpSettings;

        private float jump;
        private bool jumpCutoff;
        private float jumpTimer;
        private float jumpApexSpeed;
        public bool reachedPeakOfJump => (jumpTimer <= 0 && rigidBody.velocity.y < 0);
        private float fallForce;

        [Header("Ground Checking")]
        [SerializeField] private Grounded grounded;
        public Grounded groundedSystem => grounded;

        [Header("Components")]
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Animator animator;

        public static Action OnJump;
        public static Action OnHighJump;

        private void Update() {
            jumpTimer -= TimeValues.Delta;
        }

        public void ApplyForce() {
            ApplyJumpForce();
            ApplyFallForce();
            HandleJumpApex();
        }

        // Apply upwards force while the player is jumping
        private void ApplyJumpForce() {
            if (jumpTimer >= 0) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jump * TimeValues.FixedDelta, rigidBody.velocity.z);
            } else {
                // Reduce the force if the player releases the jump button early
                if (jumpCutoff) {
                    ReduceJumpForce();
                }
            }
        }

        // Gradually decrease the players air velocity
        private void ReduceJumpForce() {
            if (rigidBody.velocity.y > 0) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y * settings.JumpCutOffIntensity, rigidBody.velocity.z);
            }
        }

        // While the player is falling, gradually increase their speed until it reaches a max limit
        public void ApplyFallForce() {
            if (rigidBody.velocity.y < 0) {
                fallForce += Mathf.Pow(settings.FallSpeed, 2);
                float yVelocity = Mathf.Max(rigidBody.velocity.y - (fallForce * TimeValues.FixedDelta), -settings.MaxFallSpeed);
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocity, rigidBody.velocity.z);
            }
        }

        // Apply a speed and gravity modifyer while the player is at the peak of a jump
        private void HandleJumpApex() {
            if (!grounded.IsOnGround && Mathf.Abs(rigidBody.velocity.y) < settings.JumpApexThreshold) {
                // Exponensially increase the players speed over time
                jumpApexSpeed += settings.JumpApexSpeedIncrease;
                jumpApexSpeed = Mathf.Min(jumpApexSpeed, settings.JumpApexMaxSpeed);

                // Apply the speed and gravity multiplyers to the RigidBody velocity 
                rigidBody.velocity = new Vector3(
                    rigidBody.velocity.x * jumpApexSpeed,
                    rigidBody.velocity.y * settings.JumpApexGravityReduction,
                    rigidBody.velocity.z * jumpApexSpeed);
            }
        }

        public bool CanJump() {
            return grounded.IsOnGround;
        }

        public void InitialiseJump() {
            grounded.NotifyLeftGround();
            jumpTimer = settings.MaxJumpDuration;
            jump = settings.JumpForce;
            jumpApexSpeed = 1;
            OnJump?.Invoke();
        }

        public void InitialiseHighJump() {
            grounded.NotifyLeftGround();
            jumpTimer = settings.MaxJumpDuration;
            jump = settings.HighJumpForce;
            jumpApexSpeed = 1;
            OnHighJump?.Invoke();
        }

        public void EndJump() {
            fallForce = 0;
            jumpApexSpeed = 1;
            jumpCutoff = false;
        }

        public void CutOffJump() {
            if (jumpTimer >= settings.MaxJumpDuration - settings.MinJumpInput) {
                jumpTimer = settings.MinJumpDuration;
            } else {
                jumpTimer = 0;
            }
            jumpCutoff = true;
        }

        public void ChangeJumpSettings(PlayerJumpSettings newSettings) {
            settings = newSettings;
        }
    }
}