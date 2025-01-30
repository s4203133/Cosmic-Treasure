using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] protected float maxSpeed;
    protected float speed;
    [SerializeField] protected float rotateSpeed;
    private float originalRotateSpeed;
    [SerializeField] protected float reducedRotateSpeed;
    [Range(0.1f, 1f)]
    [SerializeField] protected float changeDirectionThreshhold;
    protected Vector3 velocity;

    [SerializeField] private MotionCurve acceleration;
    [SerializeField] private MotionCurve deceleration;

    [Header("COMPONENTS")]
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected Rigidbody rigidBody;

    [HideInInspector] public Vector2 moveInput;
    protected Vector3 moveDirection;

    // Difference between current velocity and target velocity
    protected float velocityDifference;

    public Action OnMoveStarted;
    public Action OnMoveStopped;

    private void Awake() {
        originalRotateSpeed = rotateSpeed;
    }

    private void OnEnable() {
        SubscribeMoveEvents();
        acceleration.Initialise();
        deceleration.Initialise();
    }

    private void OnDestroy() {
        UnsubscribeMoveEvents();
    }

    public void HandleMovement() {
        if (moveInput != Vector2.zero) {
            MoveCharacter();
        } else {
            DecelerateCharacter();
        }
    }

    public void MoveCharacter() {
        GetMoveDirection();
        CalculateVelocityDifference();
        RotateTowardsMoveDirection();
        CalculateChangeInSpeed(acceleration);
        CalculateFinalVelocity();
        ApplyVelocity();
    }

    public void DecelerateCharacter() {
        CalculateVelocityDifference();
        CalculateChangeInSpeed(deceleration);
        CalculateFinalVelocity();
        ApplyVelocity();
    }

    private void GetMoveDirection() {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
    }

    private void CalculateVelocityDifference() {
        velocityDifference = Vector3.SqrMagnitude((playerTransform.position + (moveDirection * 10)) - (playerTransform.position + (playerTransform.forward * 10)));
        velocityDifference = Mathf.Max(1, velocityDifference * changeDirectionThreshhold);
    }

    private void RotateTowardsMoveDirection() {
        if (moveInput != Vector2.zero) {
            Quaternion moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, moveRotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }

    private void CalculateChangeInSpeed(MotionCurve speedChange) {
        speed = speedChange.CalculateValue(maxSpeed);
    }

    // If the player is making a large change in direction, make them move directly towards their target direction,
    // otherwise move in the direction they are facing
    protected virtual void CalculateFinalVelocity() {
        if (velocityDifference > 1) {
            velocity = moveDirection * speed * Time.deltaTime;
        } else {
            velocity = playerTransform.forward * speed * Time.deltaTime;
        }
        // Keep the y velocity the same as the RigidBodies current one
        velocity.y = rigidBody.velocity.y;
    }

    private void ApplyVelocity() {
        rigidBody.velocity = velocity;
    }

    public bool HasStopped() {
        return (Mathf.Abs(rigidBody.velocity.x) < Mathf.Epsilon && 
                Mathf.Abs(rigidBody.velocity.z) < Mathf.Epsilon);
    }

    public void ResetVelocityVariables() {
        acceleration.Reset();
        deceleration.Reset();
        speed = 0;
    }

    public void ReduceRotateSpeed() {
        rotateSpeed = reducedRotateSpeed;
    }

    public void ResetRotateSpeed() {
        rotateSpeed = originalRotateSpeed;
    }

    // Subscribing functions to input events

    private void StartMovement(Vector2 value) {
        moveInput = value;
        ResetVelocityVariables();
    }

    private void PerformMovement(Vector2 value) {
        moveInput = value;
    }

    private void StopMovement(Vector2 value) {
        moveInput = Vector2.zero;
        deceleration.Reset();
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



[System.Serializable]
public class MotionCurve {

    [SerializeField] private AnimationCurve velocityCurve;
    private float timerLength;
    private float timer;

    public void Initialise() {
        timerLength = velocityCurve.keys[velocityCurve.length - 1].time;
    }

    public float CalculateValue(float maxValue) {
        if (timer < timerLength) {
            timer += Time.deltaTime;
        } else {
            timer = timerLength;
        }
        return maxValue * velocityCurve.Evaluate(timer);
    }

    public void Reset() {
        timer = 0;
    }

    public bool Finished() {
        return (timer >= timerLength);
    }

    public void SetTimer(float value) {
        timer = value;
    }

    public float GetTimerValue() {
        return timer;
    }
}

