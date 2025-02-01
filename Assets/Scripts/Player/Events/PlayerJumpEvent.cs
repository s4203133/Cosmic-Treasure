using UnityEngine;

public class PlayerJumpEvent : MonoBehaviour, ICustomEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerJump playerJump;

    // Observers
    private PlayerVFX playerVFX;
    private PlayerSquashAndStretch squishy;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        playerVFX = player.VFX;
        squishy = player.SqashAndStretch;
    }

    public  void SubscribeEvents() {
        if(playerJump == null) {
            return;
        }
        playerJump.OnJump += playerVFX.PlayJumpParticles;
        playerJump.OnJump += squishy.Jump.Play;
    }

    public  void UnsubscribeEvents() {
        if (playerJump == null) {
            return;
        }
        playerJump.OnJump -= playerVFX.PlayJumpParticles;
        playerJump.OnJump -= squishy.Jump.Play;
    }
}
