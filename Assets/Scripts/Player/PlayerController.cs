using UnityEngine;

namespace LMO {

    public class PlayerController : MonoBehaviour {

        [Header("INPUT")]
        [SerializeField] private InputHandler input;
        public PlayerInput playerInput;

        [Header("COMPONENTS")]
        [SerializeField] private PlayerIdle idle;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerJump jump;
        [SerializeField] private PlayerSpinAttack spin;
        [SerializeField] private PlayerGroundPound groundPound;
        [SerializeField] private PlayerHover hover;
        [SerializeField] private PlayerDive dive;
        [SerializeField] private PlayerSwing swing;
        [SerializeField] private SwingManager swingManager;
        [SerializeField] private PlayerVFX vfx;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerSquashAndStretch squashAndStretch;
        [SerializeField] private HighJumpTrail effectTrail;

        [Header("STATE MACHINE")]
        [SerializeField] private PlayerStateMachine stateMachine;

        // Public Accessors
        public PlayerStateMachine playerStateMachine => stateMachine;
        public PlayerIdle playerIdle => idle;
        public PlayerMovement playerMovment => movement;
        public PlayerJump playerJump => jump;
        public PlayerSpinAttack playerSpinAttack => spin;
        public PlayerGroundPound playerGroundPound => groundPound;
        public PlayerHover playerHover => hover;
        public PlayerDive playerDive => dive;
        public PlayerSwing playerSwing => swing;
        public SwingManager playerSwingManager => swingManager;
        public PlayerVFX playerVFX => vfx;
        public Animator playerAnimator => animator;
        public PlayerSquashAndStretch playerSquashAndStretch => squashAndStretch;
        public HighJumpTrail PlayerEffectTrail => effectTrail;
    }
}