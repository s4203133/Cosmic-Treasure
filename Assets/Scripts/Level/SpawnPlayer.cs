using System.Collections;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CameraControllerBasic cameraController;
    [SerializeField] private float delay;

    public delegate void SpawnEvent(GameObject player);
    public static SpawnEvent OnPlayerSpawned;

    private void Start() {
        StartCoroutine(SpawnPlayerAfterDelay());   
    }

    private IEnumerator SpawnPlayerAfterDelay() {
        yield return new WaitForSeconds(delay);
        GameObject Player = Instantiate(player);
        OnPlayerSpawned?.Invoke(Player);
    }
}
