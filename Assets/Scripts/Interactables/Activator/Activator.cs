using System;
using UnityEngine;

namespace NR {
    public class Activator : MonoBehaviour {
        public Action OnActivate;
        protected Animator animator;

        protected virtual void Awake() {
            animator = GetComponent<Animator>();
        }
    }
}