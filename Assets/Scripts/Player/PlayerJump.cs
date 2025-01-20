using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    [SerializeField] public float jumpForce;
    [SerializeField] private float jumpReduction;
    private float jumpFrc;
    [SerializeField] Rigidbody rigidBody;

    public Transform groundPoint;
    public LayerMask jumpLayers;

    public void ApplyJumpForce() {
        if (isGrounded) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpFrc, rigidBody.velocity.z);
            jumpFrc -= Time.deltaTime * jumpReduction;
        }
    }

    public void Update() {
        isGrounded = Physics.CheckSphere(groundPoint.position, 0.15f, jumpLayers);
    }

    public void ResetJumpForce() {
        jumpFrc = jumpForce;
    }
}
