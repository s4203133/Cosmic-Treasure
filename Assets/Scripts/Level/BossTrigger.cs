using UnityEngine;
using LMO;

namespace NR {
    /// <summary>
    /// An invisible trigger that starts the ship boss's firing behaviour.
    /// Resets when the player dies.
    /// </summary>
    public class BossTrigger : MonoBehaviour, IResettable {
        [SerializeField]
        private ShipBoss boss;

        private bool triggered;

        void Start() {
            GetComponent<MeshRenderer>().enabled = false;
            triggered = false;
        }

        private void OnTriggerEnter(Collider other) {
            if (triggered) {
                return;
            }

            if (other.CompareTag("Player")) {
                boss.Activate();
                triggered = true;
            }
        }

        public void Reset() {
            triggered = false;
        }
    }

}
