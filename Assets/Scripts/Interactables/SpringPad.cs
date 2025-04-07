using System;
using UnityEngine;

namespace LMO {

    public class SpringPad : MonoBehaviour {

        public static Action OnSmallSpringJump;
        public static Action OnLargeSpringJump;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                OnSmallSpringJump?.Invoke();
            }
        }
    }
}