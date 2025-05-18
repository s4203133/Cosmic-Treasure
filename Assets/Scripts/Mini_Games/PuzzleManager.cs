using UnityEngine;

public class TorchInteractor : MonoBehaviour
{
    public float interactRange = 5f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
            {
                TorchController torch = hit.collider.GetComponent<TorchController>();
                if (torch != null)
                {
                    torch.ChangeColor();
                }
            }
        }
    }
}
