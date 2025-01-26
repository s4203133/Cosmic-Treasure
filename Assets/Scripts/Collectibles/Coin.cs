using UnityEngine;
using UnityEngine.VFX;

public class Coin : MonoBehaviour
{
    private Transform thisTransform;
    private Vector3 position;
    private float yPosition;
    private float timeSinceSpawn;

    [SerializeField] private float spawnAnimateDuration;

    [SerializeField] private VisualEffect collectedVFX;

    private void Awake() {
        thisTransform = transform;
    }

    private void FixedUpdate() {
        AnimateCoin();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            //VisualEffect newParticles = Instantiate(collectedVFX, transform.position, Quaternion.identity);
            //Destroy(newParticles.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 newPosition) {
        position = newPosition;
        yPosition = position.y;
    }

    public void AnimatedCoinToPosition() {
        
    }

    private void AnimateCoin() {
        Vector3 targetPosition = position;
        targetPosition.y = yPosition + Mathf.Sin(timeSinceSpawn * 5);
        timeSinceSpawn += Time.fixedDeltaTime;
        thisTransform.position = Vector3.Lerp(thisTransform.position, targetPosition, spawnAnimateDuration);
    }
}
