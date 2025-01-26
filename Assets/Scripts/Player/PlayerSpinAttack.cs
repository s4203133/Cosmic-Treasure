using UnityEngine;

public class PlayerSpinAttack : MonoBehaviour
{
    [Header("SPIN SETTINGS")]
    [SerializeField] private Collider spinRangeCollider;
    [SerializeField] private float length;
    private float counter;

    [Header("JUMP BOOST")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float jumpBoostForce;

    [Header("COMPONENTS")]
    [SerializeField] private Animator animator;
    public SquashAndStretch squashAndStretch;

    public void StartSpin() {
        counter = length;
        spinRangeCollider.enabled = true;
        animator.SetTrigger("Spin");
    }

    public void Countdown() {
        counter -= Time.deltaTime;
    }

    public void StopSpin() {
        spinRangeCollider.enabled = false;
    }

    public bool SpinFinished() {
        return counter <= 0;
    }

    public void ApplyJumpBoost() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpBoostForce, rigidBody.velocity.z);
    }
}
