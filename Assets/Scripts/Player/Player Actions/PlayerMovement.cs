using System;
using UnityEngine;

namespace LMO {

    public class PlayerMovement : MonoBehaviour {

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private PlayerMovementSettings settings;
        public PlayerMovementSettings CurrentMovementSettings => settings;

        private CalculateMoveVelocity velocityCalculator;
        private RotateCharacter rotation;
        private CameraDirection camDirection;
        private MotionCurves motionCurves;

        [Header("COMPONENTS")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Rigidbody rigidBody;

        [SerializeField] private PlayerInput playerInput;
        private float speed;

        private Vector3 velocity;
        private Vector3 moveDirection;
        // Difference between current velocity and target velocity
        private float velocityDifference;

        private bool isStarting;
        private float startingTimer;
        private bool isStopping;

        [Header("CAMERA")]
        [SerializeField] private Transform cameraTransform;

        public static Action OnMoveStarted;
        public static Action OnMoveStopped;

        private void Awake() {
            velocityCalculator = new CalculateMoveVelocity(playerTransform);
            rotation = new RotateCharacter(playerTransform);
            camDirection = new CameraDirection(cameraTransform);
            motionCurves = new MotionCurves(settings.Acceleration, settings.Deceleration);
        }

        private void OnEnable() {
            OnMoveStarted += StartMoving;
            motionCurves.Enable();
            motionCurves.SetCurves(settings.Acceleration, settings.Deceleration);
            motionCurves.InitialiseMotionCurves();
        }

        private void OnDisable() {
            motionCurves.Disable();
            OnMoveStarted -= StartMoving;
        }

        public void StartMoving() {
            isStarting = true;
            isStopping = false;
            startingTimer = 0f;
        }

        public void HandleMovement() {
            camDirection.CalculateDirection();
            if (!isStopping) {
                MoveCharacter();
            } else {
                DecelerateCharacter();
            }
        }

        // Get the input direction, then move and rotate character in that direction
        public void MoveCharacter() {
            GetMoveDirection();
            TurnCharacter();
            CalculateVelocity(settings.Acceleration);
            ApplyVelocity();
        }

        private void TurnCharacter() {
            if (settings.CanRotate) {
                float rotateSpeed = CalculateRotateSpeed();
                rotation.RotateTowardsDirection(moveDirection, rotateSpeed);
            }
        }

        private float CalculateRotateSpeed() {
            if (isStarting) {
                return settings.InitialRotationSpeed;
            }
            else {
                return settings.RotationSpeed;
            }
        }

        // Slow character down once input has stopped
        public void DecelerateCharacter() {
            CalculateVelocity(settings.Deceleration);
            ApplyVelocity();
        }

        private void GetMoveDirection() {
            Vector3 forwardMovement = camDirection.Forward * playerInput.moveInput.y; 
            Vector3 rightMovement = camDirection.Right * playerInput.moveInput.x; 
            moveDirection = forwardMovement + rightMovement;
        }

        private void CalculateVelocity(MotionCurve motion) {
            // Get the current velocity based off a provided momentum (acceleration or deceleration)
            velocityDifference = velocityCalculator.CalculateVelocityDifference(moveDirection, settings.ChangeDirectionThreshhold);
            speed = velocityCalculator.CalculateChangeInSpeed(motion, settings.MaxSpeed);
            if (!settings.CanRotate) {
                velocity = velocityCalculator.CalculateVelocity(moveDirection, speed);
            }
            else {
                velocity = velocityCalculator.CalculateVelocity(settings.CanChangeDirectionQuickly, moveDirection, speed, velocityDifference);
            }
        }

        private void ApplyVelocity() {
            velocity.y = rigidBody.velocity.y;
            rigidBody.velocity = velocity;
        }

        public bool HasStopped() {
            return (isStopping && speed < 0.1f);
        }

        public void FinishedMoving() {
            isStopping = false;
        }

        public void CountdownStartTimer() {
            isStarting = (startingTimer += TimeValues.Delta) < 0.5f;
        }

        public void ResetVelocityVariables() {
            OnMoveStopped?.Invoke();
            motionCurves.ResetMotionCurves();
            isStopping = true;
        }

/*        private void ResetDeceleration() {
            motionCurves.Deccel.ResetTimer();
        }*/

        // Swap out the variables used to control the movement
        public void ChangeMovementSettings(PlayerMovementSettings newSettings) {
            motionCurves.DeactivateCurrentMotionCurves();
            settings = newSettings;
            motionCurves.SetCurves(newSettings.Acceleration, newSettings.Deceleration);
            motionCurves.InitialiseMotionCurves();
        }
    }
}

namespace LMO {

    public class MotionCurves {
        public MotionCurve Accel { get; private set; }
        public MotionCurve Deccel { get; private set; }

        public MotionCurves(MotionCurve accel, MotionCurve deccel) {
            SetCurves(accel, deccel);
        }

        public void SetCurves(MotionCurve acceleration, MotionCurve decceleratiion) {
            Accel = acceleration;
            Deccel = decceleratiion;
        }

        public void InitialiseMotionCurves() {
            Accel.Initialise();
            Deccel.Initialise();
        }

        public void ResetMotionCurves() {
            Accel.ResetTimer();
            Deccel.ResetTimer();
        }

        public void DeactivateCurrentMotionCurves() {
            Accel.OnDisable();
            Deccel.OnDisable();
        }

        private void ResetDecceleration() {
            Deccel.ResetTimer();
        }

        public void Enable() {
            Grounded.OnLanded += ResetDecceleration;
        }

        public void Disable() {
            Grounded.OnLanded -= ResetDecceleration;
        }
    }
}