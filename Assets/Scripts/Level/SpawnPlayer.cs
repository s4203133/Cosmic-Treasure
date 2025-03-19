using System;
using System.Collections;
using UnityEngine;

namespace LMO {

    public class SpawnPlayer : MonoBehaviour {

        [SerializeField] private GameObject player;

        [Space(15)]
        [SerializeField] private Vector3 respawnPosition;
        [SerializeField] private Vector3 respawnRotation;

        [Space(15)]
        [SerializeField] private float respawnDelay;
        private WaitForSeconds respawnTime;
        private WaitForSeconds fadeOutTime;

        private Coroutine currentRespawn;

        public static Action OnPlayerRespawned;
        public static Action OnLevelReset;

        private void Start() {
            respawnTime = new WaitForSeconds(respawnDelay);
            fadeOutTime = new WaitForSeconds(1f);
        }

        public void ResetPlayer() {
            if (currentRespawn == null) {
                currentRespawn = StartCoroutine(Respawn());
            }
        }

        private IEnumerator Respawn() {
            yield return respawnTime;
            OnLevelReset?.Invoke();

            yield return fadeOutTime;
            player.transform.position = respawnPosition;
            player.transform.rotation = Quaternion.Euler(respawnRotation);

            OnPlayerRespawned?.Invoke();
            currentRespawn = null;
        }

        public void SetRespawnPosition(Vector3 value) {
            respawnPosition = value;
        }

        public void SetRespawnRotation(Vector3 value) {
            respawnRotation = value;
        }

        [ContextMenu("Set Respawn Position To Players Current Position")]
        private void SetSpawnPosition() {
            if (player != null) {
                respawnPosition = player.transform.position;
            }
            else {
                respawnPosition = GameObject.FindWithTag("Player").transform.position;
            }
        }

        [ContextMenu("Set Player To Respawn Position")]
        private void SetPlayerToSpawn() {
            if (player != null) {
                player.transform.parent.transform.position = respawnPosition;
                player.transform.parent.transform.rotation = Quaternion.Euler(respawnRotation);
            }
            else {
                GameObject.FindWithTag("Player").transform.parent.transform.position = respawnPosition;
                GameObject.FindWithTag("Player").transform.parent.transform.rotation = Quaternion.Euler(respawnRotation);
            }
        }
    }
}