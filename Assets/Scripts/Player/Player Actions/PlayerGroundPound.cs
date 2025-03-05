using System;
using UnityEngine;

namespace LMO {

    public class PlayerGroundPound : MonoBehaviour {

        public bool canGroundPound => !grounded.IsOnGround;
        public bool canDive => diveTimer.canInitiateDive;
        public bool hasLanded => landing.CheckLanded();

        private bool finished;

        [Header("GROUND POUND SETTINGS")]
        [SerializeField] private GroundPoundBuildUp buildUp;
        [SerializeField] private GroundPoundFalling falling;
        [SerializeField] private GroundPoundLanded landing;
        [SerializeField] private GroundPoundColliders colliders;
        [SerializeField] private DiveTimer diveTimer;

        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Grounded grounded;

        public Action OnGroundPoundInitialised;
        public Action OnGroundPoundStarted;
        public Action OnGroundPoundLanded;
        public Action OnGroundPoundFinished;

        // Delegate to hold the current action of the ground pound being performed
        private delegate void GroundPoundAction();
        private GroundPoundAction GroundPound;

        private void OnEnable() {
            buildUp.Initialise(this, rigidBody, grounded);
            falling.Initialise(this, rigidBody, grounded);
            landing.Initialise(this, rigidBody, grounded);
            colliders.Initialise(this, rigidBody, grounded);
            diveTimer.Initialise(this, rigidBody, grounded);
        }

        private void OnDisable() {
            buildUp.Disable();
            colliders.Disable();
            diveTimer.Disable();
        }

        // Initialise ground pound, and set delegate to the build up
        public void StartGroundPound() {
            OnGroundPoundInitialised?.Invoke();
            finished = false;
            GroundPound = HandleBuildUp;
        }

        public void HandleGroundPound() {
            // If the ground pound has finished, don't continue, otherwise call current logic stored in delegate
            if (finished) {
                return;
            }
            GroundPound?.Invoke();
        }

        private void HandleBuildUp() {
            buildUp.HandleGroundPoundBuildUp();
            diveTimer.CountdownTimer();
            // If the build up has finished, set the delegate to the falling phase of the ground pound
            if (buildUp.IsFinished()) {
                OnGroundPoundStarted?.Invoke();
                GroundPound = HandleFalling;
            }
        }

        private void HandleFalling() {
            falling.HandleGroundPoundFalling();
            // If the player has landed, set the delegate to the ladning phase of the ground pound
            if (hasLanded) {
                OnGroundPoundLanded?.Invoke();
                GroundPound = HandleLanding;
            }
        }

        private void HandleLanding() {
            landing.HandleLand();
            // Once the landing duration is over, finish the ground pound
            if (landing.FinishedLand()) {
                FinishGroundPound();
            }
        }

        public void FinishGroundPound() {
            // Notify the player has landed
            OnGroundPoundFinished?.Invoke();
            rigidBody.useGravity = true;
            finished = true;
        }
    }
}