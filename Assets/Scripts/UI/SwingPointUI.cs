using UnityEngine;
using UnityEngine.UI;

namespace LMO {

    public class SwingPointUI : MonoBehaviour {

        [SerializeField] private DetectSwingJoints grapplePointDetector;
        [SerializeField] private Grapple grapple;

        [Space(15)]
        private GameObject targetGrapplePoint;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private float newLockOnStartingScale;
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
            Grapple.OnGrappleStarted += Hide;
            GrapplePoint.GrapplePointBackOnScreen += EnableOnScreenUI;
            GrapplePoint.GrapplePointNoLongerOnScreen += DisableOffScreenUI;
        }

        private void OnDisable() {
            grapplePointDetector.OnSwingPointFound -= AssignGrapplePoint;
            grapplePointDetector.OnSwingPointOutOfRange -= Hide;
            Grapple.OnGrappleStarted -= Hide;
            GrapplePoint.GrapplePointBackOnScreen -= EnableOnScreenUI;
            GrapplePoint.GrapplePointNoLongerOnScreen -= DisableOffScreenUI;
        }

        void Update() {
            if (img.enabled) {
                if (Vector3.Dot((targetGrapplePoint.transform.position - mainCam.transform.position).normalized, mainCam.transform.forward) > 0) {
                    transform.position = mainCam.WorldToScreenPoint(targetGrapplePoint.transform.position + positionOffset);
                }
            }
        }

        private void FixedUpdate() {
            if (img.enabled) {
                thisTransform.localScale = Vector3.Lerp(thisTransform.localScale, Vector3.one, 0.35f);
            }
        }

        public void AssignGrapplePoint() {
            targetGrapplePoint = grapplePointDetector.NearestGrapplePoint().gameObject;
            if (grapple.ConnectedObject != null) {
                if (targetGrapplePoint == grapple.ConnectedObject.gameObject) {
                    Hide();
                    return;
                }
            }
            Show();
        }

        private void DisableOffScreenUI(GameObject grapplePoint) {
            if(targetGrapplePoint == null) {
                return;
            }
            if(grapplePoint == targetGrapplePoint) {
                Hide();
            }
        }

        private void EnableOnScreenUI(GameObject grapplePoint) {
            if (targetGrapplePoint == null) {
                return;
            }
            if (grapplePoint == targetGrapplePoint) {
                Show();
            }
        }

        private void Show() {
            transform.position = mainCam.WorldToScreenPoint(targetGrapplePoint.transform.position);
            img.enabled = true;
        }

        public void Hide() {
            targetGrapplePoint = null;
            img.enabled = false;
            thisTransform.localScale = Vector3.one * newLockOnStartingScale;
        }
    }
}