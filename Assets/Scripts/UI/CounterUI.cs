using TMPro;
using UnityEngine;

namespace LMO {

    public class CounterUI : MonoBehaviour {
        protected TextMeshProUGUI counterext;
        [SerializeField] protected FloatVariable counter;

        private void Awake() {
            counterext = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable() => SubscribeToEvent();

        private void OnDisable() => UnsubscribeFromEvent();

        protected virtual void UpdateText() {
            counterext.text = counter.value.ToString("000");
        }

        private void OnApplicationQuit() {
            counter.value = 0;
        }

        protected virtual void SubscribeToEvent() {

        }

        protected virtual void UnsubscribeFromEvent() {
        
        }
    }
}