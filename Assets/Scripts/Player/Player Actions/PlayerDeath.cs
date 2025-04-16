using System;
using UnityEngine;

namespace LMO {

    public class PlayerDeath : MonoBehaviour {

        public static Action OnPlayerDied;

        [SerializeField] private PlayerController playerController;
        [SerializeField] private SpawnPlayer spawnPlayer;
        [SerializeField] private CameraShaker cameraShaker;

        private void OnEnable() {
            SubscribeEvents();
        }

        private void OnDisable() {
            UnsubscribeEvents();
        }

        private void Death() {
            OnPlayerDied?.Invoke();
            //playerController.playerAnimator.SetTrigger("Death");
            playerController.playerStateMachine.Deactivate();
            cameraShaker.shakeTypes.small.Shake();
            spawnPlayer.ResetPlayer();
        }

        private void ReInitialisePlayer() {
            playerController.playerStateMachine.Activate();
            playerController.playerAnimator.SetFloat("Speed", 0);
            playerController.playerAnimator.SetBool("Jumping", false);
            playerController.playerAnimator.SetBool("SpinAttacking", false);
        }

        private void SubscribeEvents() {
            LevelDeathCatcher.OnPlayerFellOutLevel += Death;
            OnPlayerDied += InputHandler.Disable;
            SpawnPlayer.OnPlayerRespawned += ReInitialisePlayer;
            SpawnPlayer.OnPlayerRespawned += InputHandler.Enable;
        }

        private void UnsubscribeEvents() {
            LevelDeathCatcher.OnPlayerFellOutLevel -= Death;
            OnPlayerDied -= InputHandler.Enable;
            SpawnPlayer.OnPlayerRespawned -= ReInitialisePlayer;
            SpawnPlayer.OnPlayerRespawned -= InputHandler.Enable;
        }
    }
}