using UnityEngine;

public class CageBreak : MonoBehaviour
{
    public GameObject cageObject; // Optional: reference to disable or destroy

    public void BreakCage()
    {
        // Play animation, sound, particles, etc.
        Debug.Log("Cage broken!");

        if (cageObject != null)
        {
            Destroy(cageObject); // Remove cage
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
