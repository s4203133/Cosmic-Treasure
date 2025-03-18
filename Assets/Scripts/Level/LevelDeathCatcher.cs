using UnityEngine;

namespace LMO {

    public class LevelDeathCatcher : MonoBehaviour {
        [SerializeField] private Vector3 respawnPosition;
        [SerializeField] private Vector3 respawnRotation;

        private void OnTriggerEnter(Collider other) {
            // If the player falls out of the level, reset their position and state machine
            if (other.tag == "Player") {
                other.gameObject.transform.position = respawnPosition;
                other.gameObject.transform.rotation = Quaternion.Euler(respawnRotation);
                PlayerStateMachine stateMachine = other.gameObject.GetComponent<PlayerStateMachine>();
                stateMachine.ChangeState(stateMachine.fallingState);
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public void SetRespawnPosition(Vector3 value) {
            respawnPosition = value;
        }

        public void SetRespawnRotation(Vector3 value) {
            respawnRotation = value;
        }

        [ContextMenu("Set Respawn Position To Players Current Position")]
        private void SetSpawnPosition() {
            respawnPosition = GameObject.FindWithTag("Player").transform.position;
        }

        [ContextMenu("Set Player To Respawn Position")]
        private void SetPlayerToSpawn() {
            GameObject.FindWithTag("Player").transform.parent.transform.position = respawnPosition;
            GameObject.FindWithTag("Player").transform.parent.transform.rotation = Quaternion.Euler(respawnRotation);
        }
    }
}