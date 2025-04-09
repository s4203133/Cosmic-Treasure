using UnityEngine;

namespace NR {
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

        public virtual void Activate() { }
    }
}

