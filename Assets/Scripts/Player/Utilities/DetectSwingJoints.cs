using System;
using System.Collections.Generic;
using UnityEngine;

namespace LMO {

    public class DetectSwingJoints : MonoBehaviour {

        GrapplePoint[] allJoints;
        int allJointsCount;
        private List<GrapplePoint> nearbyJoints;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Grapple grapple;

        [SerializeField] private float distance;
        public float Range => distance;
        private float distanceSqrd;
        [SerializeField] private float angle;
        [Tooltip("The layers to check that the grapple joint isn't behind an object and should not be connected to")]
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private LayerMask grapplePointLayers;
        private LayerMask allLayers;

        private int intermittentThink;

        private GrapplePoint closestGrapplePoint;

        public delegate void CustomEvent(GameObject obj);
        public Action OnSwingPointFound;
        public Action OnSwingPointOutOfRange;

        public void Initialise() {
            allJoints = FindObjectsOfType<GrapplePoint>();
            allJointsCount = allJoints.Length;
            distanceSqrd = distance * distance;
            nearbyJoints = new List<GrapplePoint>();
            allLayers = obstacleLayers + grapplePointLayers;
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
                if (!allJoints[i].gameObject.activeInHierarchy) {
                    continue;
                }

                GrapplePoint thisJoint = allJoints[i];

                Vector3 jointPosition = thisJoint.transform.position;
                Vector3 playerPosition = playerTransform.position + Vector3.up;

                float distanceFromPlayer = (jointPosition - playerPosition).sqrMagnitude;
                thisJoint.distanceFromPlayer = distanceFromPlayer;

                if (distanceFromPlayer > distanceSqrd || distanceFromPlayer > thisJoint.DetectionRangeSqrd) {
                    UnregisterSwingPoint(thisJoint);
                    continue;
                }

                if (thisJoint.PlayerMustFacePointToConnect) {
                    if (Vector3.Dot(playerTransform.forward, new Vector3(jointPosition.x - playerPosition.x, 0, jointPosition.z - playerTransform.transform.position.z).normalized) < angle) {
                        UnregisterSwingPoint(thisJoint);
                        continue;
                    }
                }

                RaycastHit hit;
                if (Physics.Raycast(playerPosition + Vector3.up, (jointPosition - playerPosition), out hit, thisJoint.DetectionRange, allLayers)) {
                    if (grapplePointLayers != (grapplePointLayers | (1 << hit.collider.gameObject.layer))) {
                        UnregisterSwingPoint(thisJoint);
                        continue;
                    }
                }

                RegisterNeabySwingPoint(thisJoint);
            }
        }

        private void RegisterNeabySwingPoint(GrapplePoint joint) {
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
            if (closestGrapplePoint == null || closestGrapplePoint != nearbyJoints[0]) {
                UnregisterSwingPoint(closestGrapplePoint);
                closestGrapplePoint = nearbyJoints[0];
                OnSwingPointFound?.Invoke();
            }
        }

        private void ClearClosestSwingPoint() {
            if (closestGrapplePoint != null) {
                OnSwingPointOutOfRange?.Invoke();
            }
            closestGrapplePoint = null;
        }

        private void UnregisterSwingPoint(GrapplePoint joint) {
            if (joint == null) {
                return;
            }
            if (nearbyJoints.Contains(joint)) {
                OnSwingPointOutOfRange?.Invoke();
                nearbyJoints.Remove(joint);
            }
        }

        public bool IsNearSwingJoint() {
            return (nearbyJoints.Count > 0);
        }

        public GrapplePoint NearestGrapplePoint() {
            if (nearbyJoints.Count > 0) {
                return nearbyJoints[0];
            }
            else {
                return null;
            }
        }
    }
}