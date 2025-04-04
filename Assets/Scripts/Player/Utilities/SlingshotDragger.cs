using UnityEngine;
using NR;

public class SlingshotDragger : MonoBehaviour {
    private Rigidbody slingRB;
    private SpringJoint springJoint;

    private void Awake() {
        springJoint = GetComponent<SpringJoint>();
    }

    public void ConnectToSlingshot(Rigidbody newSlingshot) {
        slingRB = newSlingshot;
        springJoint.connectedBody = slingRB;
    }

    public void DisconnectSlingshot() {
        springJoint.connectedBody = null;
        slingRB = null;
    }
}
