using LMO;
using NR;
using UnityEngine;

public class PlayerIneractableEvents : MonoBehaviour, ICustomEvent
{
    private PlayerAudioManager audioManager;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        audioManager = player.Controller.playerAudioManager;
    }

    public void SubscribeEvents() {
        Chest.OnOpened += audioManager.PlayOpenChest;
        Lever.OnInteracted += audioManager.PlayHitLever;
        ButtonCrushable.OnInteracted += audioManager.PlayHitButton;
        MessageInABottle.OnBroken += audioManager.PlayBreakGlassBottle;
        MessageInABottle.OnMessageOpen += OpenBottleMessage;
    }

    public void UnsubscribeEvents() {
        Chest.OnOpened -= audioManager.PlayOpenChest;
        Lever.OnInteracted -= audioManager.PlayHitLever;
        ButtonCrushable.OnInteracted -= audioManager.PlayHitButton;
        MessageInABottle.OnBroken -= audioManager.PlayBreakGlassBottle;
        MessageInABottle.OnMessageOpen -= OpenBottleMessage;
    }

    private void OpenBottleMessage(string message) {
        audioManager.PlayOpenPage();
    }
}
