using System.Collections.Generic;
using UnityEngine;

namespace NR {
    /// <summary>
    /// The singleton manager for a player's save file in-level.
    /// Holds a reference to the player save and loads a player's outfit into the game.
    /// </summary>
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
            //Applying upgrades would be done here.
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
