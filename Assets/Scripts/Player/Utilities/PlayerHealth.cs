using System;
using UnityEngine;

namespace LMO {

    public class PlayerHealth : MonoBehaviour {

        [SerializeField] private PlayerController player;
        private PlayerStateMachine stateMachine;
        private Rigidbody rigidBody;

        [Space(15)]
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private float knockBackStrength;
        [SerializeField] private AnimationCurve knockBackForce;
        private float knockBackDuration;
        private Vector3 knockBackDirection;
        private Vector3 KnockBackVelocity => knockBackDirection * knockBackStrength;

        public static Action OnDamageTaken;
        public static Action OnDamageKnockBackOver;

        private float knockBackTimer;

        private void Start() {
            stateMachine = player.playerStateMachine;
            rigidBody = player.RigidBody;
            knockBackDuration = knockBackForce.keys[knockBackForce.length - 1].time;
        }

        private void OnTriggerEnter(Collider other) {
            if(enemyLayers == (enemyLayers | (1 << other.gameObject.layer))) {
                KnockBack(other.gameObject);
            }
        }

        private void KnockBack(GameObject hitByEnemy) {
            OnDamageTaken?.Invoke();
            CalculateKnockBackDirection(hitByEnemy.transform);
            InitialiseTimer();
            stateMachine.TakeDamage();
        }

        private void CalculateKnockBackDirection(Transform hitTransform) {
            knockBackDirection = (player.transform.position - hitTransform.position).normalized;
            knockBackDirection = new Vector3(knockBackDirection.x, 0, knockBackDirection.z);
        }

        private void InitialiseTimer() {
            knockBackTimer = 0;
        }

        public void ApplyKnockBackForce() {
            rigidBody.velocity = (KnockBackVelocity * knockBackForce.Evaluate(knockBackTimer)) * TimeValues.FixedDelta;
        }

        public void CountdownTimer() {
            knockBackTimer += TimeValues.Delta;
            if (knockBackTimer >= knockBackDuration) {
                OnDamageKnockBackOver?.Invoke();
            }
        }
    }
}