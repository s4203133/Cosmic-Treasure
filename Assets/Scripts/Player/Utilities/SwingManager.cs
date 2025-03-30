using UnityEngine;

namespace LMO {

    public class SwingManager : MonoBehaviour {

        [SerializeField] private DetectSwingJoints swingJointDetector;
        private bool canSwing;
        public bool CanSwing => swingJointDetector.IsNearSwingJoint();

        private GameObject swingTarget;
        public GameObject SwingTarget => swingTarget;

        //[SerializeField] private SwingPointUI swingPointUI;

        void Start() {
            swingJointDetector.Initialise();
        }

        private void OnEnable() {
            swingJointDetector.OnSwingPointFound += EnableSwing;
            swingJointDetector.OnSwingPointOutOfRange += DisableSwing;
        }

        private void OnDisable() {
            swingJointDetector.OnSwingPointFound -= EnableSwing;
            swingJointDetector.OnSwingPointOutOfRange -= DisableSwing;
        }

        void Update() {
            swingJointDetector.GetClosestJoint();
        }

        private void EnableSwing() {
            swingTarget = swingJointDetector.NearestGrapplePoint().gameObject;
            //anSwing = true;
        }

        private void DisableSwing() {
            swingTarget = null;
            //canSwing = false;
        }
    }
}