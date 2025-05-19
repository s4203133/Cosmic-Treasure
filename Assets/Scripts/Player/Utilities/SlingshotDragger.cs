using UnityEngine;
using LMO;

namespace NR {
    /// <summary>
    /// Player component that fixes the player to a slingshot joint with a spring joint when grappled.
    /// Part of the player prefab, and found by slingshot joints.
    /// </summary>
    public class SlingshotDragger : MonoBehaviour {
        [SerializeField] 
        private Transform playerTransform;

        [SerializeField] 
        private PlayerStateMachine playerStateMachine;

        private Rigidbody slingRB;
        private SpringJoint springJoint;

        private void Awake() {
            springJoint = GetComponent<SpringJoint>();
        }

        public void ConnectToSlingshot(Rigidbody newSlingshot) {
            playerTransform.position = newSlingshot.transform.position + (newSlingshot.transform.right * 1.5f);
            //playerStateMachine.ChangeState(playerStateMachine.grappleIdle);
            slingRB = newSlingshot;
            springJoint.connectedBody = slingRB;
        }

        public void DisconnectSlingshot() {
            springJoint.connectedBody = null;
            slingRB = null;
        }
    }
}

