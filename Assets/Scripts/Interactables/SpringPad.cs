using System;
using System.Collections;
using UnityEngine;

namespace LMO {

    public class SpringPad : MonoBehaviour {
        [Header("SMALL BOOST")]
        [SerializeField] private float smallBoostForce;
        [SerializeField] private float smallDelayTime;
        private WaitForSeconds smallDelay;

        [Header("LARGE BOOST")]
        [SerializeField] private float largeBoostForce;
        [SerializeField] private float largeDelayTime;
        private WaitForSeconds largeDelay;

        public static Action OnSmallSpringJump;
        public static Action OnLargeSpringJump;

        private void Start() {
            smallDelay = new WaitForSeconds(smallDelayTime);
            largeDelay = new WaitForSeconds(largeDelayTime);
        }

        private void ApplyForce(Rigidbody rigidBody) {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(new Vector3(0f, smallBoostForce, 0f), ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                OnSmallSpringJump?.Invoke();
                //ApplyForce(other.GetComponent<Rigidbody>());
                //StartCoroutine(Apply(other.GetComponent<Rigidbody>()));
            }
        }

        private IEnumerator Apply(Rigidbody rigidBody) {
            rigidBody.velocity = Vector3.zero;
            yield return smallDelay;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, smallBoostForce,rigidBody.velocity.z);
            //rigidBody.AddForce(new Vector3(0f, smallBoostForce, 0f), ForceMode.Impulse);
        }
    }
}