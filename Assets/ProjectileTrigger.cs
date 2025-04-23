using UnityEngine;
using UnityEngine.Events;

namespace NR {
    public class ProjectileTrigger : Activator {
        public UnityEvent OnProjectileHit;
        public UnityEvent OnProjectileLeave;

        void OnTriggerEnter(Collider obj) {
            if (obj.gameObject.CompareTag("Projectile")) {
                OnProjectileHit.Invoke();
                isActive = true;
                OnActivate?.Invoke();
            }
            //animator.SetBool("Door", true);
        }

        void OnTriggerExit(Collider obj) {
            if (obj.gameObject.CompareTag("Projectile")) {
                OnProjectileLeave.Invoke(); 
            }
            //animator.SetBool("WaterUp", false);
        }
    }
}