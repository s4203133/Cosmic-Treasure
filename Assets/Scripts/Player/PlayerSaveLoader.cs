using System.Collections.Generic;
using UnityEngine;

namespace NR {
    public class PlayerSaveLoader : MonoBehaviour {
        public static PlayerSaveLoader Instance;

        public PlayerInventorySave playerSave;

        [SerializeField]
        private Transform playerHatSlot;

        private GameObject playerHatInstance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
            LoadOutfit();
        }

        public void LoadOutfit() {
            if (playerHatInstance != null) {
                Destroy(playerHatInstance);
            }
            playerHatInstance = Instantiate(playerSave.savedOutfit.hat.clothesPrefab, playerHatSlot);
        }

        public List<LevelSave> GetSaves() { 
            return playerSave.levelSaves;
        }

        public void AddLevel(LevelSave levelSave) { 
            playerSave.levelSaves.Add(levelSave);
        }
    }
}
