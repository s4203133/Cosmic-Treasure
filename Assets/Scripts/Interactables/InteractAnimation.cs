using UnityEngine;

public class InteractAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Play();
        }
    }

    public void Play() {
        anim.SetTrigger("Interact");
    }
}
