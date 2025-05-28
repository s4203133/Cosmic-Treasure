using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using WWH;

namespace LMO {

    public class ControllerRumbleManager : MonoBehaviour {
        [SerializeField] private ControllerRumbles controllerRumbles;

        private void OnEnable() {
            PlayerSpinState.OnSpin += ShortRumble;
            Grapple.OnGrappleStarted += ShortRumble;
            PlayerJump.OnHighJump += ShortRumble;
            PlayerSwingState.OnJumpFromSwing += ShortRumble;
            PlayerGroundPound.OnGroundPoundInitialised += ShortRumble;
            PlayerHealth.OnDamageTaken += ShortRumble;

            Collectible.OnCollected += TinyRumble;
            PlayerJump.OnJump += TinyRumble;
            Grounded.OnLanded += TinyRumble;

            PlayerGroundPound.OnGroundPoundLanded += MediumRumble;
            EnemyDeath.OnEnemyHit += MediumRumble;
            SeagullGrappleKill.OnEnemyHit += MediumRumble;

            PlayerHealth.PlayerKilledByEnemy += LongRumble;
            LevelDeathCatcher.OnPlayerFellOutLevel += LongRumble;

            SceneManager.sceneUnloaded += StopRumbleOnSceneEnd;
        }

        private void OnDisable() {
            PlayerSpinState.OnSpin -= ShortRumble;
            Grapple.OnGrappleStarted -= ShortRumble;
            PlayerJump.OnHighJump -= ShortRumble;
            PlayerSwingState.OnJumpFromSwing -= ShortRumble;
            PlayerGroundPound.OnGroundPoundInitialised -= ShortRumble;
            PlayerHealth.OnDamageTaken -= ShortRumble;

            Collectible.OnCollected -= TinyRumble;
            PlayerJump.OnJump -= TinyRumble;
            Grounded.OnLanded -= TinyRumble;

            PlayerGroundPound.OnGroundPoundLanded -= MediumRumble;
            EnemyDeath.OnEnemyHit -= MediumRumble;
            SeagullGrappleKill.OnEnemyHit -= MediumRumble;

            PlayerHealth.PlayerKilledByEnemy -= LongRumble;
            LevelDeathCatcher.OnPlayerFellOutLevel -= LongRumble;

            SceneManager.sceneUnloaded -= StopRumbleOnSceneEnd;
        }

        private void RumbleController(GamepadRumble rumble) {
            if (rumble.currentRumble != null) {
                StopCoroutine(rumble.currentRumble);
            }
            rumble.Rumble();
            rumble.currentRumble = StartCoroutine(rumble.StopRumble());
        }

        void StopRumbleOnSceneEnd(Scene scene) {
            StopRumbling();
        }

        void StopRumbling( ) {
            controllerRumbles.Tiny.CancelRumble();
            controllerRumbles.Short.CancelRumble();
            controllerRumbles.Medium.CancelRumble();
            controllerRumbles.Long.CancelRumble();
        }

        void TinyRumble() {
            RumbleController(controllerRumbles.Tiny);
        }

        void ShortRumble() {
            RumbleController(controllerRumbles.Short);
        }

        void MediumRumble() {
            RumbleController(controllerRumbles.Medium);
        }

        void LongRumble() {
            RumbleController(controllerRumbles.Long);
        }
    }
}
