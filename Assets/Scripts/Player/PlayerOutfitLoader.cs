using UnityEngine;

namespace NR {
    public class PlayerOutfitLoader : MonoBehaviour {
        public static PlayerOutfitLoader Instance;

        public PlayerInventorySave inventory;

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
            playerHatInstance = Instantiate(inventory.savedOutfit.hat.clothesPrefab, playerHatSlot);
        }
    }
}
