using UnityEngine;

namespace LMO.Player {

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

        [Header("STATE MACHINE")]
        [SerializeField] private PlayerStateMachine stateMachine;

        [Space(15)]
        [Header("TESTING")]
        public GameObject testSwingObject;

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
    }
}