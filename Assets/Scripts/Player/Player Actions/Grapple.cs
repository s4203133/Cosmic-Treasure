using System;
using UnityEngine;

namespace LMO {

    public class Grapple : MonoBehaviour {
        /*[HideInInspector]*/ public SwingJoint objectCurrentlyGrappledOnto;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private SwingJointSettings jointSettings;

        public Action<Transform> OnGrappleStarted;
        public Action OnGrappleEnded;

        private void OnEnable() {
            OnGrappleStarted += AssignGrapplePoint;
            OnGrappleEnded += RemoveGrapplePoint;
        }

        private void OnDisable() {
            OnGrappleStarted -= AssignGrapplePoint;
            OnGrappleEnded -= RemoveGrapplePoint;
        }

        public void AssignGrapplePoint(Transform newGrapplePoint) {
            objectCurrentlyGrappledOnto = newGrapplePoint.GetComponent<SwingJoint>();
            jointSettings.InitialiseJoint(playerTransform, newGrapplePoint.position);
        }

        public void RemoveGrapplePoint() {
            Destroy(jointSettings.Joint);
            objectCurrentlyGrappledOnto = null;
        }
    }
}