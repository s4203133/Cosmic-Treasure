using UnityEngine;

public class RabbitCollector : MonoBehaviour
{
    public float collectRange = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click (spin attack)
        {
            RaycastHit hit;

            // Raycast forward from the player
            if (Physics.Raycast(transform.position, transform.forward, out hit, collectRange))
            {
                if (hit.collider.CompareTag("Rabbit"))
                {
                    Debug.Log("Rabbit collected!");

                    if (RabbitCounter.Instance != null)
                    {
                        RabbitCounter.Instance.AddRabbit();
                    }
                    RabbitJumpAndDisappear jumpScript = hit.collider.GetComponent<RabbitJumpAndDisappear>();
                    if (jumpScript != null)
                    {
                        jumpScript.TriggerJumpAndDisappear();
                    }
                    else
                    {
                        Destroy(hit.collider.gameObject); // fallback if script not found
                    }

                }
            }
        }
    }
}
