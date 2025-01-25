using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
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
    float spinDuration;

    private void Start() {
        jumpParticleDuration = jumpParticles.GetVector2("LifeTimeRange").y;
        //spinDuration = SpinVFX.get
        StopRunParticles();
    }

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
        GameObject newJumpParticles = Instantiate(jumpParticles.gameObject, jumpParticlesSpawnPoint.position, Quaternion.identity);
        Destroy(newJumpParticles, jumpParticleDuration);
    }

    public void PlaySpinVFX() {
        GameObject newSpinEffect = Instantiate(SpinVFX.gameObject, spinVFXSpawnPoint.position, Quaternion.identity, spinVFXSpawnPoint);
        Destroy(newSpinEffect, 0.75f);
    }
}
