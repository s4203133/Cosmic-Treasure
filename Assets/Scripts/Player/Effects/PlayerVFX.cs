using UnityEngine;
using UnityEngine.VFX;

namespace LMO.Player {

    public class PlayerVFX : MonoBehaviour {

        [Header("RUN PARTICLES")]
        [SerializeField] private VisualEffect runParticles;
        [SerializeField] private Transform runParticlesEmissionPoint;

        [Header("JUMP PARTICLES")]
        [SerializeField] private VisualEffect jumpParticles;
        [SerializeField] private Transform jumpParticlesSpawnPoint;
        float jumpParticleDuration;

        [Header("SPIN PARTICLES")]
        [SerializeField] private VisualEffect SpinVFX;
        [SerializeField] private Transform spinVFXSpawnPoint;

        [Header("GROUND POUND PARTICLES")]
        [SerializeField] private VisualEffect groundPoundVFX;
        [SerializeField] private VisualEffect groundPoundParticles;
        [SerializeField] private Transform groundPoundParticlesSpawnPoint;
        float groundPoundParticleDuration;

        [Header("HOVER VFX")]
        [SerializeField] private VisualEffect[] hoverVFX;

        [Header("DIVE VFX")]
        [SerializeField] private VisualEffect diveVFX;
        [SerializeField] private Transform diveVFXSpawnPoint;
        private float diveVFXDuration;

        private void Start() {
            // Get the durations of the VFX
            jumpParticleDuration = jumpParticles.GetVector2("LifeTimeRange").y;
            groundPoundParticleDuration = groundPoundParticles.GetVector2("LifeTimeRange").y;
            diveVFXDuration = diveVFX.GetVector2("Lifetime Range").y;
            StopRunParticles();
        }

        // Prevent the spin VFX from being affected by animations by overriding its rotation in 'LateUpdate()' 
        private void LateUpdate() {
            spinVFXSpawnPoint.rotation = Quaternion.Euler(90, spinVFXSpawnPoint.eulerAngles.y, spinVFXSpawnPoint.eulerAngles.z);
        }

        // Functions for triggering and stopping each VFX

        public void StartRunParticles() {
            runParticles.Play();
        }

        public void StopRunParticles() {
            runParticles.Stop();
        }

        public void PlayJumpParticles() {
            GameObject newJumpParticles = Instantiate(jumpParticles.gameObject, jumpParticlesSpawnPoint.position, Quaternion.identity);
            Destroy(newJumpParticles, jumpParticleDuration);
        }

        public void PlayLandParticles() {
            GameObject newLandParticles = Instantiate(jumpParticles.gameObject, jumpParticlesSpawnPoint.position, Quaternion.identity);
            Destroy(newLandParticles, jumpParticleDuration);
        }

        public void PlaySpinVFX() {
            SpinVFX.enabled = true;
            SpinVFX.Play();
        }

        public void StopSpinVFX() {
            SpinVFX.enabled = false;
        }

        public void PlayGroundPoundParticles() {
            GameObject newGroundPoundParticles = Instantiate(groundPoundParticles.gameObject, groundPoundParticlesSpawnPoint.position, Quaternion.identity);
            Destroy(newGroundPoundParticles, groundPoundParticleDuration);
        }

        public void PlayHoverVFX() {
            for (int i = 0; i < hoverVFX.Length; i++) {
                hoverVFX[i].Reinit();
                hoverVFX[i].Play();
            }
        }

        public void StopHoverVFX() {
            for (int i = 0; i < hoverVFX.Length; i++) {
                hoverVFX[i].Stop();
            }
        }

        public void PlayDiveVFX() {
            VisualEffect newDiveVFX = Instantiate(diveVFX, diveVFXSpawnPoint.position, diveVFXSpawnPoint.rotation);
            Destroy(newDiveVFX.gameObject, diveVFXDuration);
        }
    }
}