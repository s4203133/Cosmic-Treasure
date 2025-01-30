using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    
    public void Idle() {
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
    }
}
