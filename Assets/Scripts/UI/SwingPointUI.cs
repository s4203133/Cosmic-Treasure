using UnityEngine;
using UnityEngine.UI;

namespace LMO {

    public class SwingPointUI : MonoBehaviour {

        [SerializeField] private DetectSwingJoints grapplePointDetector;

        private GameObject targetGrapplePoint;
        private Camera mainCam;

        private Image img;
        private RectTransform thisTransform;

        void Start() {
            mainCam = Camera.main;
            img = GetComponent<Image>();
            img.enabled = false;
            thisTransform = GetComponent<RectTransform>();
        }

        private void OnEnable() {
            grapplePointDetector.OnSwingPointFound += AssignGrapplePoint;
            grapplePointDetector.OnSwingPointOutOfRange += Hide;
        }

        private void OnDisable() {
            grapplePointDetector.OnSwingPointFound -= AssignGrapplePoint;
            grapplePointDetector.OnSwingPointOutOfRange -= Hide;
        }

        void Update() {
            if (img.enabled) {
                transform.position = mainCam.WorldToScreenPoint(targetGrapplePoint.transform.position);
                thisTransform.localScale = Vector3.Lerp(thisTransform.localScale, Vector3.one, 0.5f);
            }
        }

        public void AssignGrapplePoint(GameObject newGrapplePoint) {
            targetGrapplePoint = newGrapplePoint;
            transform.position = mainCam.WorldToScreenPoint(targetGrapplePoint.transform.position);
            img.enabled = true;
        }

        public void Hide(GameObject lastGrapplePoint) {
            targetGrapplePoint = null;
            img.enabled = false;
            thisTransform.localScale = Vector3.one * 2f;
        }
    }
}