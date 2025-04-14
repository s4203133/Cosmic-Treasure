using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider obj)
    {
        animator.SetBool("Door", true);
    }

    //void OnTriggerExit(Collider obj)
    //{
    //animator.SetBool("WaterUp", false);
    //}
}