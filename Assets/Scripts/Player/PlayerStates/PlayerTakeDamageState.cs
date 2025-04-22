using UnityEngine;

namespace LMO {

    public class PlayerTakeDamageState : PlayerBaseState {

        private PlayerHealth health;
        private Rigidbody rigidBody;

        private float timer;

        public PlayerTakeDamageState(PlayerController playerController) : base(playerController) {
            health = context.playerHealth;
            rigidBody = context.RigidBody;
        }

        public override void OnStateEnter() {
            if(timer > 0) {
                return;
            }
            rigidBody.velocity = Vector3.zero;
            PlayerHealth.OnCoolDownStarting += stateMachine.Idle;
        }

        public override void OnStateExit() {
            rigidBody.velocity = Vector3.zero;
            PlayerHealth.OnCoolDownStarting -= stateMachine.Idle;
        }

        public override void OnStatePhysicsUpdate() {
            health.ApplyKnockBackForce();
        }

        public override void OnStateUpdate() {

        }

        public override void OnTriggerEnter(Collider collider) {

        }
    }
}