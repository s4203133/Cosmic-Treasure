using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public Animator animator; // Assign the Animator component in the Inspector

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider obj)
    {
        if (animator)
        {
            animator.SetBool("WaterUp", true);
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (animator)
        {
            animator.SetBool("WaterUp", false);
        }
    }
}
