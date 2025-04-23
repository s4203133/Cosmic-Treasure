using NR;
using UnityEngine;
using UnityEngine.Events;

public class MultiObjectPuzzle : MonoBehaviour
{
    [SerializeField] private Activator[] targetActivators;
    [SerializeField] private UnityEvent Actions;

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable() {
        UnsubscribeEvents();
    }

    private void TestAllActivators() {
        for(int i = 0; i < targetActivators.Length; i++) {
            if (!targetActivators[i].isActive) {
                Debug.Log("Activator " + i + " is not active!");
                return;
            }
        }
        Debug.Log("All Activators are active!");
        Actions?.Invoke();
    }


    private void SubscribeEvents() {
        for (int i = 0; i < targetActivators.Length; i++) {
            targetActivators[i].OnActivate += TestAllActivators;
        }
    }

    private void UnsubscribeEvents() {
        for (int i = 0; i < targetActivators.Length; i++) {
            targetActivators[i].OnActivate -= TestAllActivators;
        }
    }
}
