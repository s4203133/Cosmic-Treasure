using System;
using UnityEngine;

namespace LMO {

    public class Grounded : MonoBehaviour {
        private bool isGrounded;
        [SerializeField] private Transform groundPoint;
        [SerializeField] private LayerMask jumpLayers;
        [SerializeField] private float groundCheckRadius;

        private bool hasStartedJump;
        private bool sentGroundedEvent;

        private float notGroundedTimer;
        public float timeSinceLeftGround => notGroundedTimer;

        public static Action OnLanded;

        public bool IsOnGround => isGrounded;

        void Update() {
            CheckIsGrounded();
            EndStartOfJump();
            SendGroundedEvent();
        }

        private bool DetectingGround() {
            return Physics.CheckSphere(groundPoint.position, groundCheckRadius, jumpLayers);
        }

        private void CheckIsGrounded() {
            if (hasStartedJump) {
                isGrounded = false;
                return;
            }
            isGrounded = DetectingGround();
        }

        public void EndStartOfJump() {
            if (!DetectingGround() || !IsOnGround) {
                hasStartedJump = false;
            }
        }

        public void NotifyLeftGround() {
            hasStartedJump = true;
            isGrounded = false;
        }

        private void SendGroundedEvent() {
            if (isGrounded) {
                notGroundedTimer = 0;
                if (!sentGroundedEvent) {
                    OnLanded?.Invoke();
                    sentGroundedEvent = true;
                }
            } else {
                notGroundedTimer += TimeValues.Delta;
                sentGroundedEvent = false;
            }
        }
    }
}