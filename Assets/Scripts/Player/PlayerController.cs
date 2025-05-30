using UnityEngine;

namespace LMO {

    public class PlayerController : MonoBehaviour {

        [Header("INPUT")]
        [SerializeField] private InputHandler input;
        public PlayerInput playerInput;

        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private PlayerIdle idle;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerJump jump;
        [SerializeField] private PlayerSpinAttack spin;
        [SerializeField] private PlayerGroundPound groundPound;
        [SerializeField] private PlayerHover hover;
        [SerializeField] private PlayerDive dive;
        [SerializeField] private PlayerSwing swing;
        [SerializeField] private Grapple grapple;
        [SerializeField] private SwingManager swingManager;
        [SerializeField] private PlayerWallJump wallJump;
        [SerializeField] private PlayerHealth health;
        [SerializeField] private LedgeHopUp ledgeHopUp;
        [SerializeField] private PlayerFloat floaty;
        [SerializeField] private PlayerVFX vfx;
        [SerializeField] private PlayerAudioManager audioManager;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerSquashAndStretch squashAndStretch;
        [SerializeField] private HighJumpTrail effectTrail;

        [Header("STATE MACHINE")]
        [SerializeField] private PlayerStateMachine stateMachine;

        // Public Accessors
        public PlayerStateMachine playerStateMachine => stateMachine;
        public Rigidbody RigidBody => rigidBody;
        public PlayerSettings PlayerSettings => settings;
        public PlayerIdle playerIdle => idle;
        public PlayerMovement playerMovment => movement;
        public PlayerJump playerJump => jump;
        public PlayerSpinAttack playerSpinAttack => spin;
        public PlayerGroundPound playerGroundPound => groundPound;
        public PlayerHover playerHover => hover;
        public PlayerDive playerDive => dive;
        public PlayerSwing playerSwing => swing;
        public Grapple playerGrapple => grapple;
        public SwingManager playerSwingManager => swingManager;
        public PlayerWallJump playerWallJump => wallJump;
        public PlayerHealth playerHealth => health;
        public LedgeHopUp LedgeHopUp => ledgeHopUp;
        public PlayerFloat playerFloat => floaty;
        public PlayerVFX playerVFX => vfx;
        public PlayerAudioManager playerAudioManager => audioManager;
        public Animator playerAnimator => animator;
        public PlayerSquashAndStretch playerSquashAndStretch => squashAndStretch;
        public HighJumpTrail PlayerEffectTrail => effectTrail;

        public void EnablePhysics() {
            rigidBody.isKinematic = false;
        }

        public void DisablePhysics() {
            if (!rigidBody.isKinematic) {
                rigidBody.velocity = Vector3.zero;
                rigidBody.isKinematic = true;
            }
        }
    }
}