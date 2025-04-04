using UnityEngine;
using NR;

public class Slingshot : MonoBehaviour {
    [SerializeField] private SlingshotJoint joint;

    //assigned in-editor to the Interact Actions event
    public void LaunchSlingshot() {
        float distance = Vector3.Distance(transform.position, joint.transform.position);
        //other behaviour here
    }
}
