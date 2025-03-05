using System;
using UnityEngine;

namespace LMO {

    public class SpringPad : MonoBehaviour {
        [SerializeField] private float boostForce;

        public static Action OnPlayerJumpedOffSpring;

        private void ApplyForce(Rigidbody rigidBody) {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(new Vector3(0f, boostForce, 0f), ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                OnPlayerJumpedOffSpring?.Invoke();
                ApplyForce(other.GetComponent<Rigidbody>());
            }
        }
    }
}