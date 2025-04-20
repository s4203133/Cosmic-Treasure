using UnityEngine;

namespace LMO {

    public class OpenCustomisationMenu : MenuTriggerZoneInteractive {

        [Space(15)]
        [SerializeField] private Transform player;
        private PlayerStateMachine stateMachine;

        [SerializeField] private Transform transformToPlacePlayer;
        [SerializeField] private CameraSwapper cameraSwapper;

        private void Start() {
            stateMachine = player.GetComponent<PlayerStateMachine>();
        }

        protected override void OnInteractiveMenuOpened() {
            base.OnInteractiveMenuOpened();
            player.position = transformToPlacePlayer.position;
            player.rotation = transformToPlacePlayer.rotation;
            stateMachine.Deactivate();
            cameraSwapper.ActivateCamera();
            CloseMenu();
            InputHandler.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        public override void OnInteractiveMenuClosed() {
            base.OnInteractiveMenuClosed();
            stateMachine.Activate();
            cameraSwapper.DeativateCamera();
            OpenMenu();
        }
    }
}