using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;
    [Range(0.1f, 1f)]
    [SerializeField] private float changeDirectionThreshhold;
    [SerializeField] private AnimationCurve acceleration;

    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rigidBody;
    [HideInInspector] public Vector2 moveInput;

    private float velocityMultiplier;
    private float moveTimer;

    private void OnEnable() {
        SubscribeMoveEvents();
    }

    private void OnDestroy() {
        UnsubscribeMoveEvents();
    }

    public void MoveCharacter() {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        float difference = Vector3.SqrMagnitude((playerTransform.position + (moveDirection * 10)) - (playerTransform.position + (playerTransform.forward * 10)));
        difference = Mathf.Max(1, difference * changeDirectionThreshhold);

        if (moveInput != Vector2.zero) {
            Quaternion moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, moveRotation, rotateSpeed * Time.fixedDeltaTime);
        }

        float speed = CalculateAcceleration();
        Vector3 velocity;
        if (difference > 1) {
            velocity = moveDirection * speed * Time.deltaTime;
        } else {
            velocity = playerTransform.forward * speed * Time.deltaTime;
        }
        velocity.y = rigidBody.velocity.y;
        rigidBody.velocity = velocity;
    }

    private float CalculateAcceleration() {
        if(velocityMultiplier < maxSpeed) {
            velocityMultiplier = maxSpeed * acceleration.Evaluate(moveTimer);
            moveTimer += Time.fixedDeltaTime;
            return velocityMultiplier;
        } else {
            return maxSpeed;
        }
    }

    public void StopMovement() {
       rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
    }

    public void ResetVelocityVariables() {
        moveTimer = 0;
        velocityMultiplier = 0;
    }

    private void StartMovement(Vector2 value) {
        moveInput = value;
        ResetVelocityVariables();
    }

    private void PerformMovement(Vector2 value) {
        moveInput = value;
    }

    private void StopMovement(Vector2 value) {
        moveInput = value;
        StopMovement();
    }

    public void SubscribeMoveEvents() {
        InputHandler.moveStarted += StartMovement;
        InputHandler.movePerformed += PerformMovement;
        InputHandler.moveCancelled += StopMovement;
    }

    public void UnsubscribeMoveEvents() {
        InputHandler.moveStarted -= StartMovement;
        InputHandler.movePerformed -= PerformMovement;
        InputHandler.moveCancelled -= StopMovement;
    }
}
