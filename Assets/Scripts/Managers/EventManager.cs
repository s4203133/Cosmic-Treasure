using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] protected ICustomEvent[] events;
    private bool init;

    private void OnEnable() {
        if(!init) {
            Initialise();
        }
        SubscribeEvents();
    }

    private void OnDisable() {
        UnsubscribeEvents();
    }

    protected virtual void SubscribeEvents() {
        for (int i = 0; i < events.Length; i++) {
            events[i].SubscribeEvents();
        }
    }

    protected virtual void UnsubscribeEvents() {
        for (int i = 0; i < events.Length; i++) {
            events[i].SubscribeEvents();
        }
    }

    protected virtual void Initialise() {
        if (events != null) {
            for (int i = 0; i < events.Length; i++) {
                events[i].Initialise(this);
            }
        }
        init = true;
    }
}
