using UnityEngine;

public class CustomFriction : MonoBehaviour {

    private Rigidbody rigidBody;

    [Range(0f, 10f)]
    [SerializeField] private float friction;

    [Range(0f, 10f)]
    [SerializeField] private float rotationFriction;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.drag = friction;
        rigidBody.angularDrag = rotationFriction;
    }

    void FixedUpdate() {
        ReduceVelocity();
    }

    // Apply artifitial friction by adding a force in the opposite direction an object is moving
    private void ReduceVelocity() {
        if (rigidBody.velocity.sqrMagnitude < 0.25f) {
            rigidBody.velocity = Vector3.zero;
        } else if (rigidBody.velocity.sqrMagnitude > 0) {
            rigidBody.velocity -= (rigidBody.velocity * friction * Time.fixedDeltaTime);
        }
    }
}
