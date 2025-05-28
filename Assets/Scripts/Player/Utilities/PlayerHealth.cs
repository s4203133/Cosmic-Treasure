using System;
using UnityEngine;

namespace LMO {

    public class PlayerHealth : MonoBehaviour {

        [SerializeField] private PlayerController player;
        private PlayerStateMachine stateMachine;
        private Rigidbody rigidBody;

        [Space(15)]
        [Header("Knock Back")]
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private float knockBackStrength;
        [SerializeField] private AnimationCurve knockBackForce;
        private float knockBackDuration;
        private float knockBackTimer;
        private Vector3 knockBackDirection;
        private Vector3 KnockBackVelocity => knockBackDirection * knockBackStrength;

        [Header("Cool Down")]
        [SerializeField] private float standUpDuration;
        private float standUpTimer;
        [SerializeField] private float coolDownDuration;
        private float coolDownTimer;
        private bool recentlyTakenDamage;

        private delegate void Countdown();
        private Countdown currentCountdown;
        private Countdown countdownFinishEvent;

        public static Action OnDamageTaken;
        public static Action OnStandingUp;
        public static Action OnCoolDownStarting;
        public static Action OnCooldownFinsihed;

        public SpawnPlayer playerSpawn;
        public static Action PlayerKilledByEnemy;

        private void Start() {
            stateMachine = player.playerStateMachine;
            rigidBody = player.RigidBody;

            knockBackDuration = knockBackForce.keys[knockBackForce.length - 1].time;
            ClearCountDownTimer();
        }

        private void Update() {
            currentCountdown?.Invoke();
        }

        private void OnTriggerEnter(Collider other) {
            if(enemyLayers == (enemyLayers | (1 << other.gameObject.layer))) {
                //WWH{
                if (other.CompareTag("seagull"))
                {
                    coolDownDuration = 10;
                }
                if (other.CompareTag("slime")) {
                    coolDownDuration = 3;
                }
                if (other.CompareTag("shark")) {
                    coolDownDuration = 3;
                }
                if (!recentlyTakenDamage) {
                    KnockBackPlayer(other.gameObject);
                    
                }
                else {
                    //reset player

                    PlayerKilledByEnemy.Invoke();
                    
                }
                //}
            }
        }

        private void KnockBackPlayer(GameObject hitByEnemy) {
            OnDamageTaken?.Invoke();
            CalculateKnockBackDirection(hitByEnemy.transform);
            InitialiseKickBackTimer();
            stateMachine.TakeDamage();
        }

        private void CalculateKnockBackDirection(Transform hitTransform) {
            knockBackDirection = (player.transform.position - hitTransform.position).normalized;
            knockBackDirection = new Vector3(knockBackDirection.x, 0, knockBackDirection.z);
        }

        public void ApplyKnockBackForce() {
            rigidBody.velocity = (KnockBackVelocity * knockBackForce.Evaluate(knockBackTimer)) * TimeValues.FixedDelta;
        }

        private void FinishKnockBack() {
            OnStandingUp?.Invoke();
            InitialiseStandUpTimer();
            // Apply shader to player to signify they've recently taken damage
        }

        private void FinishStandingUp() {
            OnCoolDownStarting?.Invoke();
            recentlyTakenDamage = true;
            InitialiseCoolDownTimer();
        }

        private void FinishCoolDown() {
            OnCooldownFinsihed?.Invoke();
            recentlyTakenDamage = false;
            ClearCountDownTimer();
        }

        // Handling Timers...

        public void CountdownTimer(ref float timer, float duration) {
            timer += TimeValues.Delta;
            if (timer >= duration) {
                countdownFinishEvent?.Invoke();
            }
        }

        private void InitialiseTimer(ref float timer, Countdown targetCountdown, Countdown finishEvent) {
            timer = 0;
            currentCountdown = targetCountdown;
            countdownFinishEvent = finishEvent;
        }

        private void CountdownKickBackTimer() => CountdownTimer(ref knockBackTimer, knockBackDuration);
        private void CountdownStandUpTimer() => CountdownTimer(ref standUpTimer, standUpDuration);
        private void CountdownCoolDownTimer() => CountdownTimer(ref coolDownTimer, coolDownDuration);
        private void InitialiseKickBackTimer() => InitialiseTimer(ref knockBackTimer, CountdownKickBackTimer, FinishKnockBack);
        private void InitialiseStandUpTimer() => InitialiseTimer(ref standUpTimer, CountdownStandUpTimer, FinishStandingUp);
        private void InitialiseCoolDownTimer() => InitialiseTimer(ref coolDownTimer, CountdownCoolDownTimer, FinishCoolDown);

        private void ClearCountDownTimer() {
            currentCountdown = null;
            countdownFinishEvent = null;
        }
    }
}