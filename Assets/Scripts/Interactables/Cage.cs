using System;
using UnityEngine;
using LMO;

public class CageBreak : MonoBehaviour, IBreakable
{
    public GameObject breakParticles;  // Drag prefab here
    public AudioClip breakSound;

    [Header("RABBIT ANIMATION")]
    [SerializeField] private Animator rabbitAnimator;
    [SerializeField] private string triggerName;

    public static Action HitObject;

    public void Break() {
        BreakCage();
    }

    private void BreakCage()
    {
        if (breakParticles != null)
        {
            Instantiate(breakParticles, transform.position, Quaternion.identity);
        }

        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        if (rabbitAnimator != null) {
            rabbitAnimator.SetTrigger(triggerName);
        }

        HitObject?.Invoke();    
        Destroy(gameObject);
    }
}
