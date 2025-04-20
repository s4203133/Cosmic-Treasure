using UnityEngine;

namespace LMO {

    public class MenuTriggerZone : MonoBehaviour {
        [SerializeField] private FadeScreen menuToDisplay;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                OnZoneEntered();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                OnZoneExited();
            }
        }

        protected virtual void OnZoneEntered() {
            OpenMenu();
        }

        protected virtual void OnZoneExited() {
            CloseMenu();
        }

        protected void OpenMenu() {
            //Debug.Log("Opening Menu");
            if (menuToDisplay != null) {
                menuToDisplay.FadeIn();
            }
        }

        protected void CloseMenu() {
            //Debug.Log("Closing Menu");
            if (menuToDisplay != null) {
                menuToDisplay.FadeOut();
            }
        }
    }
}