using UnityEngine;

namespace LMO {

    public class MenuTriggerZoneInteractive : MenuTriggerZone
    {
        [SerializeField] private GameObject menuToDisplayOnInteraction;
        private bool interactionMenuOpened;

        protected override void OnZoneEntered() {
            base.OnZoneEntered();
            InputHandler.jumpStarted += OnInteractiveMenuOpened;
        }

        protected override void OnZoneExited() {
            base.OnZoneExited();
            InputHandler.jumpStarted -= OnInteractiveMenuOpened;
        }

        protected virtual void OnInteractiveMenuOpened() {
            if (!interactionMenuOpened) {
                DisplayMenu();
                interactionMenuOpened = true;
            }
        }

        public virtual void OnInteractiveMenuClosed() {
            if (interactionMenuOpened) {
                HideMenu();
                interactionMenuOpened = false;
            }
        }

        protected void DisplayMenu() {
            menuToDisplayOnInteraction.SetActive(true);
        }

        protected void HideMenu() {
            menuToDisplayOnInteraction.SetActive(false);
        }
    }
}