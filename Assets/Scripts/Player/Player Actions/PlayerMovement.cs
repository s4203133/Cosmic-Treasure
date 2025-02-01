using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private PlayerMovementSettings settings;
    public PlayerMovementSettings CurrentMovementSettings => settings;

    private CalculateMoveVelocity velocityCalculator;
    private RotateCharacter rotation;

    [Header("COMPONENTS")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private PlayerInput playerInput;
    private float speed;
    private Vector3 velocity;
    private Vector3 moveDirection;
    // Difference between current velocity and target velocity
    private float velocityDifference;
    [HideInInspector] public bool isStopping;

    public Action OnMoveStarted;
    public Action OnMoveStopped;

    private void Awake() {
        velocityCalculator = new CalculateMoveVelocity(playerTransform);
        rotation = new RotateCharacter(playerTransform);
    }

    private void OnEnable() {
        settings.Acceleration.Initialise();
        settings.Deceleration.Initialise();
    }

    public void HandleMovement() {
        if (!isStopping) {
            MoveCharacter();
        } else {
            DecelerateCharacter();
        }
    }

    public void MoveCharacter() {
        GetMoveDirection();
        rotation.RotateTowardsDirection(moveDirection, settings.RotationSpeed);
        CalculateVelocity(settings.Acceleration);
        ApplyVelocity();
    }

    public void DecelerateCharacter() {
        CalculateVelocity(settings.Deceleration);
        ApplyVelocity();
    }

    private void GetMoveDirection() {
        moveDirection = new Vector3(playerInput.moveInput.x, 0, playerInput.moveInput.y).normalized;
    }

    private void CalculateVelocity(MotionCurve motion) {
        velocityDifference = velocityCalculator.CalculateVelocityDifference(moveDirection, settings.ChangeDirectionThreshhold);
        speed = velocityCalculator.CalculateChangeInSpeed(motion, settings.MaxSpeed);
        velocity = velocityCalculator.CalculateVelocity(settings.CanChangeDirectionQuickly, moveDirection, speed, velocityDifference);
    }

    private void ApplyVelocity() {
        velocity.y = rigidBody.velocity.y;
        rigidBody.velocity = velocity;
    }

    public bool HasStopped() {
        return (speed < Mathf.Epsilon);
    }

    public void ResetVelocityVariables() {
        settings.Acceleration.Reset();
        settings.Deceleration.Reset();
        isStopping = true;
    }

    public void ChangeMovementSettings(PlayerMovementSettings newSettings) {
        settings = newSettings;
        settings.Acceleration.Initialise();
        settings.Deceleration.Initialise();
    }
}

public class CalculateMoveVelocity {

    private Transform _transform;

    public CalculateMoveVelocity(Transform targetTransform) {
        _transform = targetTransform;
    }

    public float CalculateVelocityDifference(Vector3 direction, float threshold) {
        float returnValue = Vector3.SqrMagnitude((_transform.position + (direction * 10)) - (_transform.position + (_transform.forward * 10)));
        return(Mathf.Max(1, returnValue * threshold));
    }

    public float CalculateChangeInSpeed(MotionCurve speedChange, float maxSpeed) {
        return(speedChange.CalculateValue(maxSpeed));
    }

    // If the player is making a large change in direction, make them move directly towards their target direction,
    // otherwise move in the direction they are facing
    public Vector3 CalculateVelocity(bool canChangeDirectionQuickly, Vector3 direction, float speed, float velocityDifference) {
        Vector3 returnValue;
        if (!canChangeDirectionQuickly) {
            returnValue = _transform.forward * speed * Time.deltaTime;
        } else {
            if (velocityDifference > 1) {
                returnValue = direction * speed * Time.deltaTime;
            } else {
                returnValue = _transform.forward * speed * Time.deltaTime;
            }
        }
        return returnValue;
    }
}

public class RotateCharacter {

    private Transform _transform;

    public RotateCharacter(Transform targetTransform) {
        _transform = targetTransform;
    }

    public void RotateTowardsDirection(Vector3 direction, float speed) {
        if (direction != Vector3.zero) {
            Quaternion moveRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, moveRotation, speed * Time.fixedDeltaTime);
        }
    }
}