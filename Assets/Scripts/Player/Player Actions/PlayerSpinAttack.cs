using UnityEngine;

public class PlayerSpinAttack : MonoBehaviour
{
    [Header("SPIN SETTINGS")]
    [SerializeField] private Collider spinRangeCollider;
    [SerializeField] private float length;
    private float counter;

    [Header("JUMP BOOST")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float firstJumpBoostForce;
    [SerializeField] private float secondJumpBoostForce;

    [Header("AIR SPIN SETTINGS")]
    [SerializeField] private float maxAirSpins;
    private float airSpins;

    [Header("COMPONENTS")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform vfxSpawnPoint;

    public void StartSpin() {
        counter = length;
        spinRangeCollider.enabled = true;
        animator.SetTrigger("Spin");
    }

    public void Countdown() {
        counter -= Time.deltaTime;
    }

    public void StopSpin() {
        counter = 0;
        spinRangeCollider.enabled = false;
    }

    public bool SpinFinished() {
        return counter <= 0;
    }

    public void ApplyJumpBoost() {
        airSpins++;
        if (airSpins == 1) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, firstJumpBoostForce, rigidBody.velocity.z);
        } else {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, secondJumpBoostForce, rigidBody.velocity.z);
        }
    }

    public bool CanAirSpin() {
        return airSpins < maxAirSpins;
    }

    public void ResetAirSpins() {
        airSpins = 0;
    }

    private void LateUpdate() {
        vfxSpawnPoint.rotation = Quaternion.Euler(90, vfxSpawnPoint.eulerAngles.y, vfxSpawnPoint.eulerAngles.z);
    }

    private void OnEnable() {
        SubscribeEvents();
    }

    private void OnDisable() {
        UnsubscribeEvents();
    }

    private void SubscribeEvents() {
        Grounded.OnLanded += ResetAirSpins;
    }

    private void UnsubscribeEvents() {
        Grounded.OnLanded -= ResetAirSpins;
    }
}
