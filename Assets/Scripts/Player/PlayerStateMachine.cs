using UnityEngine;

namespace LMO {

    public class PlayerStateMachine : MonoBehaviour {

        [SerializeField] protected PlayerBaseState currentState;
        public string stateName;

        public PlayerIdleState idleState;
        public PlayerRunState runState;
        public PlayerJumpState jumpState;
        public PlayerFallingState fallingState;
        public PlayerSpinState spinState;
        public PlayerGroundPoundState groundPoundState;
        public PlayerHighJumpState highJumpState;
        public PlayerHoverState hoverState;
        public PlayerDiveState diveState;
        public PlayerSwingState swingState;

        public PlayerController controller;

        void Start() {
            InitialiseStates();
            StartStateMachine();
        }

        void Update() {
            currentState.OnStateUpdate();
        }

        private void FixedUpdate() {
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
            fallingState = new PlayerFallingState(controller);
            spinState = new PlayerSpinState(controller);
            groundPoundState = new PlayerGroundPoundState(controller);
            highJumpState = new PlayerHighJumpState(controller);
            hoverState = new PlayerHoverState(controller);
            diveState = new PlayerDiveState(controller);
            swingState = new PlayerSwingState(controller);
        }

        protected virtual void StartStateMachine() {
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
    }
}