using UnityEngine;

namespace LMO {

    public class LevelCheckpoint : MonoBehaviour {
        [Header("CHECK POINT SPAWN POSITION")]
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private Vector3 spawnRotation;
        [SerializeField] private LevelDeathCatcher deathCatcher;

        [Header("CHECK POINT ANIMATION")]
        [SerializeField] private Animator checkPointAnimator;
        [SerializeField] private string animationTriggerText;

        private bool triggered;

        private void Start() {
            if (deathCatcher == null) {
                deathCatcher = FindObjectOfType<LevelDeathCatcher>();
            }
            GetComponent<MeshRenderer>().enabled = false;
            triggered = false;
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Object hit checkpoint");

            if (other.CompareTag("Player")) {
                Debug.Log("Player hit checkpoint");
                SetSpawnData();
                TriggerCheckpointAnimation();
            }
        }

        private void SetSpawnData() {
            if (!triggered) {
                deathCatcher.SetRespawnPosition(spawnPosition);
                deathCatcher.SetRespawnRotation(spawnRotation);
                triggered = true;
            }
        }

        private void TriggerCheckpointAnimation() {
            if (checkPointAnimator != null) {
                checkPointAnimator.SetTrigger(animationTriggerText);
            }
        }

        [ContextMenu("Set Spawn Position To Players Current Position")]
        private void SetSpawnPosition() {
            spawnPosition = GameObject.FindWithTag("Player").transform.position;
            spawnRotation = GameObject.FindWithTag("Player").transform.eulerAngles;
        }

        [ContextMenu("Set Player To Spawn Position")]
        private void SetPlayerToSpawn() {
            GameObject.FindWithTag("Player").transform.parent.transform.position = spawnPosition;
            GameObject.FindWithTag("Player").transform.parent.transform.rotation = Quaternion.Euler(spawnRotation);
        }
    }
}