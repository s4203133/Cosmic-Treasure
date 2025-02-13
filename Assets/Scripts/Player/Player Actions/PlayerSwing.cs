using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("JOINT SETTINGS")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float spring;
    [SerializeField] private float damper;
    [SerializeField] private float massScale;


    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody rigidBody;
    private SpringJoint currentJoint;


    public void StartSwing(GameObject swingPoint) {
        rigidBody.velocity = Vector3.zero;
        InitialiseJoint(swingPoint.transform.position);
    }

    private void InitialiseJoint(Vector3 connectPoint) {
        currentJoint = transform.parent.gameObject.AddComponent<SpringJoint>();
        currentJoint.autoConfigureConnectedAnchor = false;
        //currentJoint.connectedBody = swingPoint.GetComponent<Rigidbody>();
        currentJoint.connectedAnchor = connectPoint;

        currentJoint.maxDistance = maxDistance;
        currentJoint.minDistance = minDistance;

        currentJoint.spring = spring;
        currentJoint.damper = damper;
        currentJoint.massScale = massScale;
    }

    public void PerformSwing() {

    }

    public void EndSwing() {
        Destroy(currentJoint);
    }
}
