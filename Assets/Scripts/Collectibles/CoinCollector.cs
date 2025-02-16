using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform thisTransform;
    private Transform player;

    private bool canCollect;
    private bool collected;

    public bool CoinInRange => canCollect;

    private void OnEnable() => Coin.OnCoinActivated += AllowCollect;

    private void OnDisable() => Coin.OnCoinActivated -= AllowCollect;

    private void Update() => Collect();

    private void Awake() {
        thisTransform = transform.parent;
        Coin.OnCoinActivated += AllowCollect;
    }

    private void OnTriggerEnter(Collider other) {
        if (!canCollect) {
            return;
        }

        if(other.tag == "Player") {
            collected = true;
            player = other.transform;
        }
    }

    private void Collect() {
        if(collected) {
            thisTransform.position = Vector3.Lerp(thisTransform.position, player.position, speed);
            speed += Time.deltaTime;
        }
    }

    private void AllowCollect() {
        canCollect = true;
    }
}
