using UnityEngine;
using UnityEngine.Events;

namespace NR {
    /// <summary>
    /// Base class for objects that can be triggered by Activator objects.
    /// </summary>
    public class Activateable : MonoBehaviour {
        public Activator activator;

        private void OnEnable() {
            if (activator != null) {
                activator.OnActivate += Activate;
            }

        }

        private void OnDisable() {
            if (activator != null) {
                activator.OnActivate -= Activate;
            }
        }

        /// <summary>
        /// Virtual to be overridden by child classes.
        /// </summary>
        public virtual void Activate() {
            interactActions?.Invoke();
        }
        
        [SerializeField] protected UnityEvent interactActions;
    }
}

