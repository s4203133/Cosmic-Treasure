using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Open() {
        animator.SetTrigger("Open");
    }
}
