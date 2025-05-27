using UnityEngine;

public class CageBreak : MonoBehaviour
{
    public GameObject breakParticles;  // Drag prefab here
    public AudioClip breakSound;

    public void BreakCage()
    {
        // Play particle effect
        if (breakParticles != null)
        {
            Instantiate(breakParticles, transform.position, Quaternion.identity);
        }

        // Play sound
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        // Destroy cage
        Destroy(gameObject);
    }
}
