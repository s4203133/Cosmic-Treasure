using UnityEngine;

public class RotateInfinte : MonoBehaviour
{
    public bool rotateX = true;
    public bool rotateY = false;
    public bool rotateZ = false;
    
    public float speed = 50f;

    void Update()
    {
        Vector3 rotation = Vector3.zero;
        if (rotateX)
        {
            rotation += Vector3.right;
        }
        if (rotateY)
        {
            rotation += Vector3.up;
        }
        if (rotateZ)
        {
            rotation += Vector3.forward;
        }

        transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
