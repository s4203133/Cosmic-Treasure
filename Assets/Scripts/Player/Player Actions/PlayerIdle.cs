using UnityEngine;

namespace LMO {

    public class PlayerIdle : MonoBehaviour {
        [SerializeField] private Rigidbody rigidBody;

        // While the player is idle, stop any movement forces (while keeping gravity)
        public void Idle() {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        }
    }
}