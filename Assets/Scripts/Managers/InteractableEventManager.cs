using UnityEngine;

public class InteractableEventManager : EventManager
{
    [SerializeField] private CameraShaker cameraShaker;

    protected override void SubscribeEvents() {
        IBreakable.OnBroken += cameraShaker.shakeTypes.small.Shake;
    }

    protected override void UnsubscribeEvents() {
        IBreakable.OnBroken -= cameraShaker.shakeTypes.small.Shake;
    }
}
