using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void OnEnable() {
        SubscribeEvents();
    }

    private void OnDisable() {
        UnsubscribeEvents();
    }

    private void OnDestroy() {
        UnsubscribeEvents();
    }

    protected virtual void SubscribeEvents() {

    }

    protected virtual void UnsubscribeEvents() {

    }
}
