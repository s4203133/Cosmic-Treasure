using UnityEngine;

public class PlayerGroundedEvent : MonoBehaviour, ICustomEvent {
    [Header("SUBJECT")]
    [SerializeField] private Grounded playerGrounded;

    // Observers
    private PlayerVFX playerVFX;
    private PlayerSquashAndStretch squishy;
    private PlayerSpinAttack playerSpin;
    private PlayerHover playerHover;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        playerVFX = player.VFX;
        squishy = player.SqashAndStretch;
        playerSpin = player.Controller.playerSpinAttack;
        playerHover = player.Controller.playerHover;
    }

    public void SubscribeEvents() {
        if (playerGrounded == null) {
            return;
        }
        playerGrounded.OnLanded += playerVFX.PlayLandParticles;
        playerGrounded.OnLanded += squishy.Land.Play;
        playerGrounded.OnLanded += playerSpin.ResetAirSpins;
        playerGrounded.OnLanded += playerHover.EnableHover;
    }

    public void UnsubscribeEvents() {
        if (playerGrounded == null) {
            return;
        }
        playerGrounded.OnLanded -= playerVFX.PlayLandParticles;
        playerGrounded.OnLanded -= squishy.Land.Play;
        playerGrounded.OnLanded -= playerSpin.ResetAirSpins;
        playerGrounded.OnLanded -= playerHover.EnableHover;
    }
}
