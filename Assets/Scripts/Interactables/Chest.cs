using UnityEngine;
using UnityEngine.VFX;
using LMO.Interfaces;

namespace LMO.Interactables {

    public class Chest : MonoBehaviour, IBreakable {

        [SerializeField] private VisualEffect breakOpenEffect;
        private Animator animator;

        private bool isOpen;

        [Header("COLLIDERS")]
        [SerializeField] private Collider closedCollider;
        [SerializeField] private Collider[] openColliders;

        [Header("SPAWNING ITEMS")]
        [SerializeField] private CoinSpawner coinSpawner;

        void Awake() {
            animator = GetComponent<Animator>();
        }

        public void Break() {
            OpenChest();
        }

        private void OpenChest() {
            if (!isOpen) {
                // Open the chest by playing an animtion and VFX, enabling new colliders, and spawning the specified item
                isOpen = true;
                IBreakable.OnBroken?.Invoke();
                animator.SetTrigger("Open");
                breakOpenEffect.Play();
                ActivateOpenColliders();
                coinSpawner.SpawnCoin();
            }
        }

        // When the chest is open, activate the new colliders
        private void ActivateOpenColliders() {
            closedCollider.enabled = false;
            for (int i = 0; i < openColliders.Length; i++) {
                openColliders[i].enabled = true;
            }
        }
    }
}