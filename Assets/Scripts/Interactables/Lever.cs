using System;
using UnityEngine;
using LMO;

namespace NR {
    public class Lever : MonoBehaviour, ISpinnable {
        public Action OnActivate;
        private Animator animator;

        void Awake() {
            animator = GetComponent<Animator>();
        }

        public void OnHit() {
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


