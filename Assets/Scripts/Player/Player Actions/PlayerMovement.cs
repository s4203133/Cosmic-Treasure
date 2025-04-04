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

        [Header("CAMERA")]
        [SerializeField] private Transform cameraTransform;

        public static Action OnMoveStarted;
        public static Action OnMoveStopped;

        private void Awake() {
            velocityCalculator = new CalculateMoveVelocity(playerTransform);
            rotation = new RotateCharacter(playerTransform);
            camDirection = new CameraDirection(cameraTransform);
        }

        private void OnEnable() {
            settings.Acceleration.Initialise();
            settings.Deceleration.Initialise();
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
            if (settings.CanRotate) {
                rotation.RotateTowardsDirection(moveDirection, settings.RotationSpeed);
            }
            CalculateVelocity(settings.Acceleration);
            ApplyVelocity();
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
            return (speed < Mathf.Epsilon);
        }

        public void ResetVelocityVariables() {
            OnMoveStopped?.Invoke();
            settings.Acceleration.Reset();
            settings.Deceleration.Reset();
            isStopping = true;
        }

        // Swap out the variables used to control the movement
        public void ChangeMovementSettings(PlayerMovementSettings newSettings) {
            settings = newSettings;
            settings.Acceleration.Initialise();
            settings.Deceleration.Initialise();
        }
    }
}
