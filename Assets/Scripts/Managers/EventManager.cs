using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void OnEnable() {
        SubscribeEvents();
    }

    private void OnDisable() {
        UnsubscribeEvents();
    }

    protected virtual void SubscribeEvents() {

    }

    protected virtual void UnsubscribeEvents() {

    }
}
