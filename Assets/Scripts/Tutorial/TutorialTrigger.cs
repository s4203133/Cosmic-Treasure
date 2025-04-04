using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Action OnPlayerEnter;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            OnPlayerEnter?.Invoke();
        }
    }
}
