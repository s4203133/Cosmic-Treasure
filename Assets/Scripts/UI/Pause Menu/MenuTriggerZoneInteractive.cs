using UnityEngine;

namespace LMO {

    public class MenuTriggerZoneInteractive : MenuTriggerZone
    {
        [SerializeField] private GameObject menuToDisplayOnInteraction;
        [SerializeField] private GamepadUIButton startingButton;
        protected bool interactionMenuOpened;
        private bool justClosed;

        protected override void OnZoneEntered() {
            base.OnZoneEntered();
            InputHandler.jumpStarted += OnInteractiveMenuOpened;
        }

        protected override void OnZoneExited() {
            base.OnZoneExited();
            InputHandler.jumpStarted -= OnInteractiveMenuOpened;
        }

        protected virtual void OnInteractiveMenuOpened() {
            if (justClosed) {
                return;
            }
            if (!interactionMenuOpened) {
                DisplayMenu();
                interactionMenuOpened = true;
                startingButton.Highlight();
            }
        }

        public virtual void OnInteractiveMenuClosed() {
            if (interactionMenuOpened) {
                interactionMenuOpened = false;
                justClosed = true;
                Invoke("SetCanOpen", 0.1f);
                HideMenu();
            }
        }

        protected void DisplayMenu() {
            menuToDisplayOnInteraction.SetActive(true);
        }

        protected void HideMenu() {
            menuToDisplayOnInteraction.SetActive(false);
        }

        private void SetCanOpen() {
            justClosed = false;
        }
    }
}