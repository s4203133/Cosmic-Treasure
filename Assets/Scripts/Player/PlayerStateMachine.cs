using UnityEngine;

namespace LMO {

    public class PlayerStateMachine : MonoBehaviour {

        [SerializeField] protected PlayerBaseState currentState;
        public string stateName;

        public PlayerIdleState idleState;
        public PlayerRunState runState;
        public PlayerJumpState jumpState;
        public PlayerSmallSpringJump smallSpringJumpState;
        public PlayerLargeSpringJump largeSpringJumpState;
        public PlayerFallingState fallingState;
        public PlayerSpinState spinState;
        public PlayerGroundPoundState groundPoundState;
        public PlayerHighJumpState highJumpState;
        public PlayerHoverState hoverState;
        public PlayerDiveState diveState;

        // Grapple States
        public PlayerGrappleConnectedIdle grappleIdle;
        public PlayerGrappleConnectedMove grappleRun;
        public PlayerGrappleConnectedJump grappleJump;
        public PlayerGrappleConnectedSpin grappleSpin;
        public PlayerSwingState swingState;

        public PlayerController controller;

        private bool Active;

        void Start() {
            Active = true;
            InitialiseStates();
            StartStateMachine();
        }

        void Update() {
            if (!Active)
            {
                return;
            }
            currentState.OnStateUpdate();
        }

        private void FixedUpdate() {
            if (!Active)
            {
                return;
            }
            currentState.OnStatePhysicsUpdate();
        }

        public void ChangeState(PlayerBaseState newState) {
            currentState.OnStateExit();
            currentState = newState;
            stateName = currentState.ToString();
            currentState.OnStateEnter();
        }

        private void OnDisable() {
            currentState.OnStateExit();
        }

        private void OnTriggerEnter(Collider other) {
            currentState.OnTriggerEnter(other);
        }

        protected virtual void InitialiseStates() {
            idleState = new PlayerIdleState(controller);
            runState = new PlayerRunState(controller);
            jumpState = new PlayerJumpState(controller);
            smallSpringJumpState = new PlayerSmallSpringJump(controller);
            largeSpringJumpState = new PlayerLargeSpringJump(controller);
            fallingState = new PlayerFallingState(controller);
            spinState = new PlayerSpinState(controller);
            groundPoundState = new PlayerGroundPoundState(controller);
            highJumpState = new PlayerHighJumpState(controller);
            hoverState = new PlayerHoverState(controller);
            diveState = new PlayerDiveState(controller);

            grappleIdle = new PlayerGrappleConnectedIdle(controller);
            grappleRun = new PlayerGrappleConnectedMove(controller);
            grappleJump = new PlayerGrappleConnectedJump(controller);
            grappleSpin = new PlayerGrappleConnectedSpin(controller);
            swingState = new PlayerSwingState(controller);
        }

        protected virtual void StartStateMachine() {
            if (!Active)
            {
                return;
            }
            currentState = idleState;
            stateName = currentState.ToString();
            currentState.OnStateEnter();
        }

        public void Idle() {
            ChangeState(idleState);
        }

        public void Fall() {
            ChangeState(fallingState);
        }

        public void GrappleToTarget() {
            if (controller.playerSwingManager.SwingTarget == null) {
                return;
            }

            controller.playerGrapple.OnGrappleStarted?.Invoke(controller.playerSwingManager.SwingTarget.transform);

            if (currentState == idleState) {
                ChangeState(grappleIdle);
            } else if(currentState == runState) {
                ChangeState(grappleRun);
            }
        }

        public void Activate() {
            controller.EnablePhysics();
            Active = true;
            Idle();
        }

        public void Deactivate() {
            Idle();
            controller.DisablePhysics();
            Active = false;
        }
    }
}