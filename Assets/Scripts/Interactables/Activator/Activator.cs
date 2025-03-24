using System;
using UnityEngine;

namespace NR {
    public class Activator : MonoBehaviour {
        public Action OnActivate;
        protected Animator animator;

        void Awake() {
            animator = GetComponent<Animator>();
        }
    }
}