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
        SubscribeHittableObjects();
        SubscribeMechanicalObjects();
        SubscribeBottleObjects();
    }

    public void UnsubscribeEvents() {
        UnsubscribeHittableObjects();
        UnsubscribeMechanicalObjects();
        UnsubscribeBottleObjects();
    }

    private void OpenBottleMessage(string message) {
        audioManager.PlayOpenPage();
    }

    private void SubscribeHittableObjects() {
        Chest.OnOpened += audioManager.PlayOpenChest;
        Barrel.HitObject += audioManager.PlayHitObject;
        PalmTree.HitObject += audioManager.PlayHitObject;
        CageBreak.HitObject += audioManager.PlayBreakObject;
        Crate.HitObject += audioManager.PlayBreakObject;
    }

    private void UnsubscribeHittableObjects() {
        Chest.OnOpened -= audioManager.PlayOpenChest;
        Barrel.HitObject -= audioManager.PlayHitObject;
        PalmTree.HitObject -= audioManager.PlayHitObject;
        CageBreak.HitObject -= audioManager.PlayBreakObject;
        Crate.HitObject -= audioManager.PlayBreakObject;
    }

    private void SubscribeMechanicalObjects() {
        Lever.OnInteracted += audioManager.PlayHitLever;
        ButtonCrushable.OnInteracted += audioManager.PlayHitButton;
        FallingPlatform.OnFall += audioManager.PlayFallingPlatform;
    }

    private void UnsubscribeMechanicalObjects() {
        Lever.OnInteracted -= audioManager.PlayHitLever;
        ButtonCrushable.OnInteracted -= audioManager.PlayHitButton;
        FallingPlatform.OnFall -= audioManager.PlayFallingPlatform;
    }

    private void SubscribeBottleObjects() {
        MessageInABottle.OnBroken += audioManager.PlayBreakGlassBottle;
        MessageInABottle.OnMessageOpen += OpenBottleMessage;
    }

    private void UnsubscribeBottleObjects() {
        MessageInABottle.OnBroken -= audioManager.PlayBreakGlassBottle;
        MessageInABottle.OnMessageOpen -= OpenBottleMessage;
    }
}
