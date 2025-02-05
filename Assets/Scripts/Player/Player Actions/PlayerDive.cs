using System;
using UnityEngine;

namespace LMO.Player {

    public class PlayerDive : MonoBehaviour {

        [Header("DIVE SETTINGS")]
        [SerializeField] private float diveForce;
        [SerializeField] private AnimationCurve diveMomentum;
        private float timer;
        private float speed;
        [SerializeField] private float initialAirBoost;

        [Header("AIR MOVEMENT")]
        [SerializeField] private float moveStrength;
        [SerializeField] private float acceleration;
        private float moveSpeed;

        private Vector3 moveDirection;
        private Vector3 velocity;
        private Vector3 appliedVelocity;

        private Vector2 moveInput;

        [Header("COMPONENTS")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Rigidbody rigidBody;

        [Header("CAMERA")]
        [SerializeField] private Transform cameraTransform;
        private CameraDirection camDirection;

        public Action OnDive;

        private void Awake() {
            camDirection = new CameraDirection(cameraTransform);
        }

        // Initialise dive variables and and apply air boost to the player
        public void StartDive() {
            OnDive?.Invoke();
            timer = 0;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, initialAirBoost, rigidBody.velocity.z);
            moveSpeed = 0;
        }

        public void Countdown() {
            timer += Time.deltaTime;
        }

        public void HandleDive() {
            camDirection.CalculateDirection();
            GetVelocity();
            GetMoveDirection();
            AddMoveDirection();
            ApplyDiveForce();
        }

        private void ApplyDiveForce() {
            // Set applied veloicty y component to the players current y velocity
            appliedVelocity.y = rigidBody.velocity.y;
            rigidBody.velocity = appliedVelocity;
        }

        private void GetVelocity() {
            // Get the speed based off the animation curve
            speed = diveMomentum.Evaluate(timer);
            velocity = playerTransform.forward * (speed * diveForce * Time.fixedDeltaTime);
        }

        private void GetMoveDirection() {
            Vector3 forwardMovement = camDirection.Forward * moveInput.y;
            Vector3 rightMovement = camDirection.Right * moveInput.x;
            moveDirection = forwardMovement + rightMovement;
        }

        private void AddMoveDirection() {
            // If the player is pressing input in direction they're diving in, don't add additional speed
            if (Vector3.Dot(playerTransform.forward, moveDirection) < 0.3) {
                moveSpeed += acceleration * Time.fixedDeltaTime;
                moveSpeed = MathF.Min(moveSpeed, moveStrength);
                appliedVelocity = velocity + (moveDirection * moveSpeed);
            } else {
                appliedVelocity = velocity;
            }
        }

        private void GetMoveInput(Vector2 value) {
            moveInput = value;
        }

        private void OnEnable() {
            InputHandler.moveStarted += GetMoveInput;
            InputHandler.movePerformed += GetMoveInput;
            InputHandler.moveCancelled += GetMoveInput;
        }

        private void OnDisable() {
            InputHandler.moveStarted -= GetMoveInput;
            InputHandler.movePerformed -= GetMoveInput;
            InputHandler.moveCancelled -= GetMoveInput;
        }
    }
}