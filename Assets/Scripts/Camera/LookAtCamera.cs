using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Transform _transform;

    void Start()
    {
        _transform = transform;    
    }

    void Update()
    {
        _transform.LookAt(cameraTransform.position);
    }
}
