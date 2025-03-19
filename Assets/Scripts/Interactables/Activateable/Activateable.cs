using UnityEngine;

namespace NR {
    public class Activateable : MonoBehaviour {
        public Lever activator;

        private void Awake() {
            activator.OnActivate += Activate;
        }

        public virtual void Activate() { }
    }
}

