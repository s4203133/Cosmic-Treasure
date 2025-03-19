using System;
using UnityEngine;

namespace LMO {

    public class LevelDeathCatcher : MonoBehaviour {

        public static Action OnPlayerFellOutLevel;

        private void OnTriggerEnter(Collider other) {
            // If the player falls out of the level, reset their position and state machine
            if (other.tag == "Player") {
                OnPlayerFellOutLevel?.Invoke();
            }
        }
    }
}