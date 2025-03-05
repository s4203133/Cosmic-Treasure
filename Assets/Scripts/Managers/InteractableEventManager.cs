using UnityEngine;

namespace LMO {

    public class InteractableEventManager : EventManager 
    {
        [SerializeField] private CameraShaker cameraShaker;

        // When an item is broken, add a small amount of shake to the camera
        protected override void SubscribeEvents() {
            IBreakable.OnBroken += cameraShaker.shakeTypes.small.Shake;
        }

        protected override void UnsubscribeEvents() {
            IBreakable.OnBroken -= cameraShaker.shakeTypes.small.Shake;
        }
    }
}