using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("INPUT")]
    [SerializeField] private InputHandler input;

    [Header("COMPONENTS")]
    [SerializeField] private PlayerIdle idle;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerJump jump;
    [SerializeField] private PlayerSpinAttack spin;
    [SerializeField] private PlayerGroundPound groundPound;
    [SerializeField] private PlayerHover hover;
    [SerializeField] private PlayerHoverMovement hoverMovement;

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
    public PlayerHoverMovement playerHoverMovement => hoverMovement;
}