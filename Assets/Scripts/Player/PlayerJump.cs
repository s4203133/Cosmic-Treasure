using UnityEngine;

public class PlayerJump : MonoBehaviour {
    [Header("Jumping")]
    [SerializeField] public float jumpForce;

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
    private bool isGrounded;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask jumpLayers;
    private bool hasLeftGround;
    float framesGrounded;

    [Header("Jump Apex")]
    [SerializeField] private float jumpApexThreshold;
    [SerializeField] private float jumpApexGravityReduction;
    [SerializeField] private float jumpApexMaxSpeed;
    [SerializeField] private float jumpApexSpeedIncrease;
    private float jumpApexSpeed;

    [Header("Components")]
    [SerializeField] Rigidbody rigidBody;

    private void Update() {
        isGrounded = Physics.CheckSphere(groundPoint.position, 0.1f, jumpLayers);
        jumpTimer -= Time.deltaTime;
        CheckPlayerLeftGround();
        IncrementGroundedTimer();
    }

    public void ApplyForce() {
        ApplyJumpForce();
        ApplyFallForce();
        HandleJumpApex();
    }

    private void ApplyJumpForce() {
        if (jumpTimer >= 0) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime, rigidBody.velocity.z);
        } else {
            if (jumpCutoff) {
                ReduceJumpForce();
            }
        }
    }

    private void ApplyFallForce() {
        if (rigidBody.velocity.y < 0) {
            fallForce += Mathf.Pow(fallSpeed, 2);
            float yVelocity = Mathf.Max(rigidBody.velocity.y - (fallForce * Time.fixedDeltaTime), -maxFallSpeed);
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocity, rigidBody.velocity.z);
        }
    }

    // Apply a speed and gravity modifyer while the player is at the peak of a jump
    private void HandleJumpApex() {
        if (!isGrounded && Mathf.Abs(rigidBody.velocity.y) < jumpApexThreshold) {
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

    private void ReduceJumpForce() {
        if (rigidBody.velocity.y > 0) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y * jumpCutOffIntensity, rigidBody.velocity.z);
        }
    }

    public bool CanJump() {
        return !isGrounded;
    }

    public void InitialiseJump() {
        jumpTimer = maxJumpDuration;
        framesGrounded = 0;
        jumpApexSpeed = 1;
    }

    public void EndJump() {
        hasLeftGround = false;
        fallForce = 0;
        jumpApexSpeed = 1;
        jumpCutoff = false;
        framesGrounded = 0;
    }

    public void CutOffJump() {
        if(jumpTimer >= maxJumpDuration - minJumpInput) {
            jumpTimer = minJumpDuration;
        } else {
            jumpTimer = 0;
        }
        jumpCutoff = true;
    }

    private void CheckPlayerLeftGround() {
        if (!hasLeftGround) {
            if (!isGrounded) {
                hasLeftGround = true;
            }
        }
    }

    public bool IsGrounded() {
        return isGrounded;
    }

    private void IncrementGroundedTimer() {
        if (isGrounded) {
            framesGrounded++;
        }
    }

    public bool HasLanded() {
        if(framesGrounded > 5) {
            return true;
        }

        if (!hasLeftGround) {
            return false;
        }
        return isGrounded;
    }
}
