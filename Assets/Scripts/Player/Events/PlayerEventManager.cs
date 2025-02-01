using UnityEngine;

public class PlayerEventManager : EventManager {

    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerVFX vfx;
    [SerializeField] private PlayerSquashAndStretch squashAndStretch;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShaker cameraShake;
    [SerializeField] private HighJumpTrail trail;

    public PlayerController Controller => controller;
    public PlayerVFX VFX => vfx;
    public PlayerSquashAndStretch SqashAndStretch => squashAndStretch;
    public Animator Anim => animator;
    public CameraShaker CameraShake => cameraShake;
    public HighJumpTrail Trail => trail;

    protected override void Initialise() {
        events = GetComponentsInChildren<ICustomEvent>();
        base.Initialise();
    }
}
