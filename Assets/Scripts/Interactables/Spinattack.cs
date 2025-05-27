using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    public float spinForce = 300f;
    public bool isSpinning = false;

    void Update()
    {
        // Detect left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            isSpinning = true;

            // Optional: You can rotate the player to simulate spin
            transform.Rotate(Vector3.up * spinForce * Time.deltaTime);
        }
    }

    // Detect collision during spin
    void OnCollisionEnter(Collision collision)
    {
        if (isSpinning)
        {
            // Check if the object we hit is tagged as "Cage"
            if (collision.gameObject.CompareTag("Cage"))
            {
                // Try to break the cage
                CageBreak cage = collision.gameObject.GetComponent<CageBreak>();
                if (cage != null)
                {
                    cage.BreakCage();
                }
            }

            isSpinning = false; // Stop spinning after hitting
        }
    }
}
