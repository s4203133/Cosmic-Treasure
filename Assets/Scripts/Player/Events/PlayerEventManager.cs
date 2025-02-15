using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {
    public class PlayerEventManager : EventManager {

        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerVFX vfx;
        [SerializeField] private PlayerSquashAndStretch squashAndStretch;
        [SerializeField] private Animator animator;
        [SerializeField] private CameraShaker cameraShake;
        [SerializeField] private HighJumpTrail trail;
        [SerializeField] private FOVChanger fovChanger;

        public PlayerController Controller => controller;
        public PlayerVFX VFX => vfx;
        public PlayerSquashAndStretch SqashAndStretch => squashAndStretch;
        public Animator Anim => animator;
        public CameraShaker CameraShake => cameraShake;
        public HighJumpTrail Trail => trail;
        public FOVChanger FOV_Changer => fovChanger;

        protected override void Initialise() {
            events = GetComponentsInChildren<ICustomEvent>();
            base.Initialise();
        }
    }
}