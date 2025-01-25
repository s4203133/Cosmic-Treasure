using UnityEngine;

public class InteractAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "PlayerSpinAttack") {
            Play();
        }
    }

    public void Play() {
        animator.SetTrigger("Interact");
    }
}
