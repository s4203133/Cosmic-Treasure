using System.Collections.Generic;
using UnityEngine;

namespace LMO {

    public class DetectSwingJoints : MonoBehaviour {

        SwingJoint[] allJoints;
        int allJointsCount;
        private List<SwingJoint> nearbyJoints;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private float distance;
        public float Range => distance;
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

                Vector3 jointPosition = thisJoint.transform.position;
                float distanceFromPlayer = (jointPosition - playerTransform.position).sqrMagnitude;
                thisJoint.distanceFromPlayer = distanceFromPlayer;
                if (distanceFromPlayer > distanceSqrd) {
                    UnregisterSwingPoint(thisJoint);
                    continue;
                }

                if (Vector3.Dot(playerTransform.forward, new Vector3(jointPosition.x - playerTransform.transform.position.x, 0, jointPosition.z - playerTransform.transform.position.z).normalized) < angle) {
                    UnregisterSwingPoint(thisJoint);
                    continue;
                }

                RegisterNeabySwingPoint(thisJoint);
            }
        }

        private void RegisterNeabySwingPoint(SwingJoint joint) {
            if (nearbyJoints.Count == 0) {
                nearbyJoints.Add(joint);
            } else {
                if (joint.distanceFromPlayer < nearbyJoints[0].distanceFromPlayer) {
                    UnregisterSwingPoint(nearbyJoints[0]);
                    nearbyJoints.Add(joint);
                }
            }
        }

        private void AssignClosestSwingPoint() {
            if (closestSwingPoint == null || closestSwingPoint != nearbyJoints[0]) {
                UnregisterSwingPoint(closestSwingPoint);
                closestSwingPoint = nearbyJoints[0];
                closestSwingPoint.Activate();
                OnSwingPointFound?.Invoke(closestSwingPoint.gameObject);
            }
        }

        private void ClearClosestSwingPoint() {
            if (closestSwingPoint != null) {
                closestSwingPoint.Deactivate();
                OnSwingPointOutOfRange?.Invoke(closestSwingPoint.gameObject);
            }
            closestSwingPoint = null;
        }

        private void UnregisterSwingPoint(SwingJoint joint) {
            if(joint == null) {
                return;
            }
            joint.Deactivate();
            if (nearbyJoints.Contains(joint)) {
                nearbyJoints.Remove(joint);
            }
        }

        /*        private void OnDrawGizmos() {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(playerTransform.position + Vector3.up, (playerTransform.position + playerTransform.forward * 5) + Vector3.up);


                    if (Vector3.Dot(playerTransform.forward, new Vector3(closestSwingPoint.transform.position.x - playerTransform.transform.position.x, 0, closestSwingPoint.transform.position.z - playerTransform.transform.position.z).normalized) < angle) {
                        Gizmos.color = Color.yellow;
                    }
                    else {
                        Gizmos.color = Color.green;
                    }
                    Gizmos.DrawLine(playerTransform.position + Vector3.up, playerTransform.position + new Vector3(closestSwingPoint.transform.position.x - playerTransform.transform.position.x, 0, closestSwingPoint.transform.position.z - playerTransform.transform.position.z).normalized * 5 + Vector3.up);
                }*/
    }
}