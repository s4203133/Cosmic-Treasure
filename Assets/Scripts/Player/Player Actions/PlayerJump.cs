using UnityEngine;

namespace LMO.Player {

    public class PlayerJump : MonoBehaviour {

        [Header("Jumping")]
        [SerializeField] public float jumpForce;
        [SerializeField] public float highJumpForce;
        private float jump;
        [SerializeField] private float maxJumpDuration;
        [SerializeField] private float minJumpDuration;
        [SerializeField] private float minJumpInput;

        private bool jumpCutoff;
        [Range(0.1f, 0.99f)]
        [SerializeField] private float jumpCutOffIntensity;
        private float jumpTimer;

        [Header("Falling")]
        [SerializeField] private float fallSpeed;
        [SerializeField] private float maxFallSpeed;
        private float fallForce;

        [Header("Ground Checking")]
        [SerializeField] private Grounded grounded;
        public Grounded groundedSystem => grounded;

        [Header("Jump Apex")]
        [SerializeField] private float jumpApexThreshold;
        [SerializeField] private float jumpApexGravityReduction;
        [SerializeField] private float jumpApexMaxSpeed;
        [SerializeField] private float jumpApexSpeedIncrease;
        private float jumpApexSpeed;

        [Header("Components")]
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Animator animator;

        // Custom Events
        public delegate void JumpEvent();
        public JumpEvent OnJump;
        public JumpEvent OnHighJump;

        private void Update() {
            jumpTimer -= Time.deltaTime;
        }

        public void ApplyForce() {
            ApplyJumpForce();
            ApplyFallForce();
            HandleJumpApex();
        }

        // Apply upwards force while the player is jumping
        private void ApplyJumpForce() {
            if (jumpTimer >= 0) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jump * Time.fixedDeltaTime, rigidBody.velocity.z);
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
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y * jumpCutOffIntensity, rigidBody.velocity.z);
            }
        }

        // While the player is falling, gradually increase their speed until it reaches a max limit
        public void ApplyFallForce() {
            if (rigidBody.velocity.y < 0) {
                fallForce += Mathf.Pow(fallSpeed, 2);
                float yVelocity = Mathf.Max(rigidBody.velocity.y - (fallForce * Time.fixedDeltaTime), -maxFallSpeed);
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocity, rigidBody.velocity.z);
            }
        }

        // Apply a speed and gravity modifyer while the player is at the peak of a jump
        private void HandleJumpApex() {
            if (!grounded.IsOnGround && Mathf.Abs(rigidBody.velocity.y) < jumpApexThreshold) {
                // Exponensially increase the players speed over time
                jumpApexSpeed += jumpApexSpeedIncrease;
                jumpApexSpeed = Mathf.Min(jumpApexSpeed, jumpApexMaxSpeed);

                // Apply the speed and gravity multiplyers to the RigidBody velocity 
                rigidBody.velocity = new Vector3(
                    rigidBody.velocity.x * jumpApexSpeed,
                    rigidBody.velocity.y * jumpApexGravityReduction,
                    rigidBody.velocity.z * jumpApexSpeed);
            }
        }

        public bool CanJump() {
            return grounded.IsOnGround;
        }

        public void InitialiseJump() {
            grounded.NotifyLeftGround();
            jumpTimer = maxJumpDuration;
            jump = jumpForce;
            jumpApexSpeed = 1;
            OnJump?.Invoke();
        }

        public void InitialiseHighJump() {
            grounded.NotifyLeftGround();
            jumpTimer = maxJumpDuration;
            jump = highJumpForce;
            jumpApexSpeed = 1;
            OnHighJump?.Invoke();
        }

        public void EndJump() {
            fallForce = 0;
            jumpApexSpeed = 1;
            jumpCutoff = false;
        }

        public void CutOffJump() {
            if (jumpTimer >= maxJumpDuration - minJumpInput) {
                jumpTimer = minJumpDuration;
            } else {
                jumpTimer = 0;
            }
            jumpCutoff = true;
        }
    }
}