using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LMO;

namespace NR {
    public class EquipMenu : MonoBehaviour {
        [SerializeField]
        private TMP_Dropdown hatDropdown;

        private List<PlayerOutfitItem> hats = new List<PlayerOutfitItem>();

        private PlayerOutfitSave outfit;

        private void Start() {
            if (PlayerOutfitLoader.Instance != null) {
                outfit = PlayerOutfitLoader.Instance.inventory.savedOutfit;
                List<string> hatNames = new List<string>();
                foreach (var item in PlayerOutfitLoader.Instance.inventory.ownedClothes) {
                    switch (item.type) {
                        case OutfitType.Hat:
                            hats.Add(item);
                            hatNames.Add(item.itemName);
                            break;
                    }
                }
                hatDropdown.AddOptions(hatNames);
                hatDropdown.value = hatNames.IndexOf(outfit.hat.itemName);
            }
            InputHandler.pauseStarted += ShowMenu;
            gameObject.SetActive(false);
        }

        private void ShowMenu() {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        public void HatUpdate(int index) {
            outfit.hat = hats[index];
        }

        public void ApplyChanges() {
            PlayerOutfitLoader.Instance.LoadOutfit();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
