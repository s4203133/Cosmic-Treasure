using System;
using UnityEngine;

namespace LMO {

    public class Grapple : MonoBehaviour {
        public GrapplePoint objectCurrentlyGrappledOnto;
        public GrapplePoint ConnectedObject => objectCurrentlyGrappledOnto;

        [SerializeField] private Transform playerTransform;

        [Space(15)]
        [SerializeField] private SwingRope swingRope;
        public SwingRope Rope => swingRope;

        [Space(15)]
        [SerializeField] private SwingJointSettings jointSettings;
        private bool connectedToJoint;

        public Action<Transform> OnGrappleStarted;
        public Action OnGrappleEnded;

        private void Awake() {
            swingRope.Initialise();
        }

        private void LateUpdate() {
            swingRope.DrawRope();
        }

        private void OnEnable() {
            OnGrappleStarted += AssignGrapplePoint;
            OnGrappleEnded += RemoveGrapplePoint;
        }

        private void OnDisable() {
            OnGrappleStarted -= AssignGrapplePoint;
            OnGrappleEnded -= RemoveGrapplePoint;
        }

        public void AssignGrapplePoint(Transform newGrapplePoint) {
            objectCurrentlyGrappledOnto = newGrapplePoint.GetComponent<GrapplePoint>();
            //jointSettings.InitialiseJoint(playerTransform, newGrapplePoint.position);
        }

        public void RemoveGrapplePoint() {
            objectCurrentlyGrappledOnto = null;
        }

        public void ConnectJoint(Vector3 jointPosition) {
            if (!connectedToJoint) {
                jointSettings.InitialiseJoint(playerTransform, jointPosition, objectCurrentlyGrappledOnto);
                connectedToJoint = true;
            }
        }

        public void DisconnectJoint() {
            Destroy(jointSettings.Joint);
            connectedToJoint = false;
        }
    }
}