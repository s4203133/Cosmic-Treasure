using System;
using UnityEngine;

namespace LMO {

    public class PlayerSpinAttack : MonoBehaviour {
        [Header("SPIN SETTINGS")]
        [SerializeField] private Collider spinRangeCollider;
        [SerializeField] private float length;
        private float counter;
        public bool SpinFinished => counter <= 0;


        [Header("JUMP BOOST")]
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float firstJumpBoostForce;
        [SerializeField] private float secondJumpBoostForce;

        [Header("AIR SPIN SETTINGS")]
        [SerializeField] private float maxAirSpins;
        private float airSpins;
        public bool CanAirSpin => airSpins < maxAirSpins;

        public static Action OnSpin;
        public static Action OnAirSpin;
        public static Action OnSpinEnd;

        public void StartSpin() {
            counter = length;
            spinRangeCollider.enabled = true;
            if (airSpins == 0) {
                OnSpin?.Invoke();
            } else if(airSpins > 0) {
                OnAirSpin?.Invoke();
            }
        }

        public void Countdown() {
            counter -= TimeValues.Delta;
        }

        public void StopSpin() {
            counter = 0;
            spinRangeCollider.enabled = false;
            OnSpinEnd?.Invoke();
        }

        // If the player is in the air apply a jump boost when spinning
        public void ApplyJumpBoost() {
            airSpins++;
            if (airSpins == 1) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, firstJumpBoostForce, rigidBody.velocity.z);
            } else {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, secondJumpBoostForce, rigidBody.velocity.z);
            }
        }

        public void ResetAirSpins() {
            airSpins = 0;
        }
    }
}