using UnityEngine;
using LMO.Interfaces;

namespace LMO.CustomEvents {

    public class EventManager : MonoBehaviour {
        [SerializeField] protected ICustomEvent[] events;
        private bool init;

        private void OnEnable() {
            if (!init) {
                Initialise();
            }
            SubscribeEvents();
        }

        private void OnDisable() {
            UnsubscribeEvents();
        }

        // Subscribe all events to their subjects
        protected virtual void SubscribeEvents() {
            for (int i = 0; i < events.Length; i++) {
                events[i].SubscribeEvents();
            }
        }

        // Un-subscribr all events from their subjects
        protected virtual void UnsubscribeEvents() {
            for (int i = 0; i < events.Length; i++) {
                events[i].SubscribeEvents();
            }
        }

        // Initialse the events if required
        protected virtual void Initialise() {
            if (events != null) {
                for (int i = 0; i < events.Length; i++) {
                    events[i].Initialise(this);
                }
            }
            init = true;
        }
    }
}