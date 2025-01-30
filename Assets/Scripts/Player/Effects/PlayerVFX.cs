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

    [Header("GROUND POUND PARTICLES")]
    [SerializeField] private VisualEffect groundPoundVFX;
    [SerializeField] private VisualEffect groundPoundParticles;
    [SerializeField] private Transform groundPoundParticlesSpawnPoint;
    float groundPoundParticleDuration;

    [Header("HOVER VFX")]
    [SerializeField] private VisualEffect[] hoverVFX;

    private void Start() {
        jumpParticleDuration = jumpParticles.GetVector2("LifeTimeRange").y;
        groundPoundParticleDuration = groundPoundParticles.GetVector2("LifeTimeRange").y;
        StopRunParticles();
    }

    private void LateUpdate() {
        spinVFXSpawnPoint.rotation = Quaternion.Euler(90, spinVFXSpawnPoint.eulerAngles.y, spinVFXSpawnPoint.eulerAngles.z);
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
        SpinVFX.enabled = true;
        SpinVFX.Play();
    }

    public void StopSpinVFX() {
        SpinVFX.enabled = false;
    }

    public void PlayGroundPoundParticles() {
        //groundPoundVFX.Play();
        GameObject newGroundPoundParticles = Instantiate(groundPoundParticles.gameObject, groundPoundParticlesSpawnPoint.position, Quaternion.identity);
        Destroy(newGroundPoundParticles, groundPoundParticleDuration);
    }

    public void PlayHoverVFX() {
        for(int i = 0; i < hoverVFX.Length; i++) {
            hoverVFX[i].Reinit();
            hoverVFX[i].Play();
        }
    }

    public void StopHoverVFX() {
        for (int i = 0; i < hoverVFX.Length; i++) {
            hoverVFX[i].Stop();
        }
    }
}
