using System;
using UnityEngine;

namespace LMO {

    public class Grapple : MonoBehaviour {
        public SwingJoint objectCurrentlyGrappledOnto;

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
        }

        public void RemoveGrapplePoint() {
            objectCurrentlyGrappledOnto = null;
        }
    }
}