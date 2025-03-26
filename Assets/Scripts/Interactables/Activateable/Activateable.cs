using UnityEngine;

namespace NR {
    public class Activateable : MonoBehaviour {
        public Activator activator;

        private void OnEnable() {
            activator.OnActivate += Activate;
        }

        private void OnDisable() {
            activator.OnActivate -= Activate;
        }

        public virtual void Activate() { }
    }
}

