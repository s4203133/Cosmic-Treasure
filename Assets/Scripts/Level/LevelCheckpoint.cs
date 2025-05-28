using UnityEngine;

namespace LMO {

    public class LevelCheckpoint : MonoBehaviour {
        [Header("CHECK POINT SPAWN POSITION")]
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private Vector3 spawnRotation;
        [SerializeField] private SpawnPlayer spawnPlayer;

        [Header("CHECK POINT ANIMATION")]
        [SerializeField] private Animator checkPointAnimator;
        [SerializeField] private string animationTriggerText;
        [SerializeField] private AudioSource soundEffect;

        private bool triggered;

        private void Start() {
            if (spawnPlayer == null) {
                spawnPlayer = FindObjectOfType<SpawnPlayer>();
            }
            GetComponent<MeshRenderer>().enabled = false;
            triggered = false;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                TriggerCheckpointAnimation();
                SetSpawnData();
            }
        }

        private void SetSpawnData() {
            if (!triggered) {
                spawnPlayer.SetRespawnPosition(spawnPosition);
                spawnPlayer.SetRespawnRotation(spawnRotation);
                triggered = true;
            }
        }

        private void TriggerCheckpointAnimation() {
            if (triggered) {
                return;
            }
            if (checkPointAnimator != null) {
                checkPointAnimator.SetTrigger(animationTriggerText);
            }
            if(soundEffect != null) {
                soundEffect.Play();
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