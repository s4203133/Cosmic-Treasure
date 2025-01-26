using UnityEngine;
using UnityEngine.VFX;

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
            isOpen = true;
            animator.SetTrigger("Open");
            breakOpenEffect.Play();
            ActivateOpenColliders();
            coinSpawner.SpawnCoin();
        }
    }

    private void ActivateClosedColliders() {
        for (int i = 0; i < openColliders.Length; i++) {
            openColliders[i].enabled = false;
        }
        closedCollider.enabled = true;
    }

    private void ActivateOpenColliders() {
        closedCollider.enabled = false;
        for(int i = 0; i < openColliders.Length; i++) {
            openColliders[i].enabled = true;
        }
    }
}
