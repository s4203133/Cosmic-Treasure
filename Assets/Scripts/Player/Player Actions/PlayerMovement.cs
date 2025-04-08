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
        [SerializeField] private PlayerSkid skid;

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
        public static Action OnSkid;

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
            OnSkid += Skid;
        }

        private void OnDisable() {
            motionCurves.Disable();
            OnMoveStarted -= StartMoving;
            OnSkid -= Skid;
        }

        public void StartMoving() {
            isStarting = true;
            isStopping = false;
            startingTimer = 0f;
            skid.ResetSkid(playerTransform.forward);
        }

        public void HandleMovement() {
            skid.UpdateDirection(playerTransform, playerInput.moveInput);
            camDirection.CalculateDirection();
            if (skid.isSkidding) {
                skid.CountdownSkidTimer();
                return;
            }
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
            HandleSkid();
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

        private void HandleSkid() {
            if (isStarting || speed != settings.MaxSpeed) {
                return;
            }
            skid.UpdateDirection(playerTransform, playerInput.moveInput);
        }

        private void Skid() {
            rigidBody.velocity = Vector3.zero;
            StartMoving();
            motionCurves.ResetMotionCurves();
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

        // Swap out the variables used to control the movement
        public void ChangeMovementSettings(PlayerMovementSettings newSettings) {
            motionCurves.DeactivateCurrentMotionCurves();
            settings = newSettings;
            motionCurves.SetCurves(newSettings.Acceleration, newSettings.Deceleration);
            motionCurves.InitialiseMotionCurves();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(playerTransform.position, playerTransform.position + (skid.playerDir * 5));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerTransform.position, playerTransform.position + (skid.targetDir * 8));
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

namespace LMO {

    [System.Serializable]
    public class PlayerSkid {
        private Vector3 playerDirection;
        public bool isSkidding { get; private set; }

        private float updateTimer;
        private float skiddingTimer;

        [SerializeField] private float straightLineThreshold;
        [SerializeField] private float skidTime;

        public Vector3 playerDir;
        public Vector3 targetDir;

        public void ResetSkid(Vector3 direction) {
            playerDirection = direction;
            updateTimer = 0;
            skiddingTimer = skidTime;
            isSkidding = false;
        }

        public void UpdateDirection(Transform player, Vector2 moveInput) {
            Vector3 direction = player.position + player.forward;
            direction = (direction - player.position).normalized;
            Vector3 targetDirection = ((player.position + new Vector3(moveInput.x, 0, moveInput.y)) - player.position).normalized;

            playerDir = direction;
            targetDir = targetDirection;
            //Debug.Log("Player: " + direction);
            //Debug.Log("Target: " + targetDirection);


            //updateTimer += TimeValues.FixedDelta;
            //if (updateTimer > 0.1f) {
                //if (Vector3.Dot(direction, targetDirection) < straightLineThreshold) {
                    //Skid();
                //}
                //playerDirection = direction;
                //updateTimer = 0;
            //}
        }

        private void Skid() {
            isSkidding = true;
            skiddingTimer = skidTime;
            PlayerMovement.OnSkid?.Invoke();
        }

        public void CountdownSkidTimer() {
            skiddingTimer -= TimeValues.Delta;
            isSkidding = skiddingTimer > 0;
        }
    }
}