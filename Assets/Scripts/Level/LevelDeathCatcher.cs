using UnityEngine;
using LMO.Player;

namespace LMO.LevelMechanics {

    public class LevelDeathCatcher : MonoBehaviour {
        [SerializeField] private Vector3 respawnPosition;

        private void OnTriggerEnter(Collider other) {
            // If the player falls out of the level, reset their position and state machine
            if (other.tag == "Player") {
                other.gameObject.transform.position = respawnPosition;
                other.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                PlayerStateMachine stateMachine = other.gameObject.GetComponent<PlayerStateMachine>();
                stateMachine.ChangeState(stateMachine.fallingState);
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}