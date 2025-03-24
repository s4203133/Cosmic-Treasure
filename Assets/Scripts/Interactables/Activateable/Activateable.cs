using UnityEngine;

namespace NR {
    public class Activateable : MonoBehaviour {
        public Activator activator;

        private void Awake() {
            activator.OnActivate += Activate;
        }

        public virtual void Activate() { }
    }
}

