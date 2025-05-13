using UnityEngine;
using LMO;

namespace NR {
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
