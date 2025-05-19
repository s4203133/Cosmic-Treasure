using System;
using UnityEngine;

namespace NR {
    /// <summary>
    /// Base class for objects that trigger Activateable objects.
    /// Most important component is the OnActivate event.
    /// </summary>
    public class Activator : MonoBehaviour {
        public Action OnActivate;
        [HideInInspector] public bool isActive;

        protected Animator animator;

        protected virtual void Awake() {
            animator = GetComponent<Animator>();
        }
    }
}