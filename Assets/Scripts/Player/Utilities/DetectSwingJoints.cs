using System.Collections.Generic;
using UnityEngine;

public class DetectSwingJoints : MonoBehaviour {

    SwingJoint[] allJoints;
    int allJointsCount;
    private List<SwingJoint> nearbyJoints;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float distance;
    private float distanceSqrd;
    [SerializeField] private float angle;

    private int intermittentThink;

    private SwingJoint closestSwingPoint;

    public delegate void CustomEvent(GameObject obj);
    public CustomEvent OnSwingPointFound;
    public CustomEvent OnSwingPointOutOfRange;

    public void Initialise() {
        allJoints = FindObjectsOfType<SwingJoint>();
        allJointsCount = allJoints.Length;
        distanceSqrd = distance * distance;
        nearbyJoints = new List<SwingJoint>();
    }

    public void GetClosestJoint() {
        // Only run the function every other frame to help with performance
        if (intermittentThink > 0) {
            intermittentThink = 0;
            return;
        }
        intermittentThink++;

        nearbyJoints.Clear();
        FindAllSwingJointsInRange();

        if (nearbyJoints.Count == 0) {
            ClearClosestSwingPoint();
            return;
        } else {
            AssignClosestSwingPoint();
        }
    }

    private void FindAllSwingJointsInRange() {
        for (int i = 0; i < allJointsCount; i++) {
            SwingJoint thisJoint = allJoints[i];
/*            if (thisJoint == closestSwingPoint) {
                continue;
            }*/

            Vector3 jointPosition = thisJoint.transform.position;
            float distance = (jointPosition - playerTransform.position).sqrMagnitude;
            if (distance > distanceSqrd) {
                continue;
            }

            if (Vector3.Dot(playerTransform.forward, (jointPosition - playerTransform.transform.position).normalized) < angle) {
                continue;
            }

            RegisterNeabySwingPoint(thisJoint);
        }
    }

    private void RegisterNeabySwingPoint(SwingJoint joint) {
        if (nearbyJoints.Count == 0) {
            nearbyJoints.Add(joint);
            joint.distanceFromPlayer = distance;
        } else {
            if (distance < nearbyJoints[0].distanceFromPlayer) {
                nearbyJoints.Insert(0, joint);
            }
        }
    }

    private void AssignClosestSwingPoint() {
        if (closestSwingPoint == null) {
            closestSwingPoint = nearbyJoints[0];
            OnSwingPointFound?.Invoke(closestSwingPoint.gameObject);
            closestSwingPoint.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
    }

    private void ClearClosestSwingPoint() {
        if (closestSwingPoint != null) {
            closestSwingPoint.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            OnSwingPointOutOfRange?.Invoke(closestSwingPoint.gameObject);
        }
        closestSwingPoint = null;
    }
}
