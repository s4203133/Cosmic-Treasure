using UnityEngine;
using Cinemachine;

namespace LMO {

    public class LevelIntroAnimation : MonoBehaviour {

        private Animator animator;
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        private CinemachineBrain playerCamera;
        private FollowPlayerPoint cameraFollowPoint;

        private void Start() {
            animator = GetComponent<Animator>();
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerStateMachine = playerController.playerStateMachine;

            playerCamera = Camera.main.GetComponent<CinemachineBrain>();
            cameraFollowPoint = FindObjectOfType<FollowPlayerPoint>();

            playerController.DisablePhysics();
            playerCamera.enabled = false;
            cameraFollowPoint.enabled = false;

            InputHandler.Disable();
        }

        public void ActivatePlayer() {
            animator.enabled = false;
            playerStateMachine.ActivateStateMachine();
            playerController.EnablePhysics();
            InputHandler.Enable();
            playerCamera.enabled = true;
            cameraFollowPoint.enabled = true;
        }
    }
}