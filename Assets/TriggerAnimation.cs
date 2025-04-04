using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            animator.SetBool("WaterUp", true);
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            animator.SetBool("WaterUp", false);
        }
    }
}
