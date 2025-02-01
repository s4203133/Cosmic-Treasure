using UnityEngine;

public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerHover playerHover;

    // Observers
    private PlayerVFX playerVFX;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        playerVFX = player.VFX;
    }

    public  void SubscribeEvents() {
        if (playerHover == null) {
            return;
        }
        playerHover.OnHoverStarted += playerVFX.PlayHoverVFX;
        playerHover.OnHoverEnded += playerVFX.StopHoverVFX;
    }

    public  void UnsubscribeEvents() {
        if (playerHover == null) {
            return;
        }
        playerHover.OnHoverStarted -= playerVFX.PlayHoverVFX;
        playerHover.OnHoverEnded -= playerVFX.StopHoverVFX;
    }
}
