using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LMO;
using System.Linq;

namespace NR {
    public class EquipMenu : MonoBehaviour {
        [SerializeField]
        private TMP_Dropdown hatDropdown;

        private List<PlayerOutfitItem> hats = new List<PlayerOutfitItem>();

        private PlayerOutfitSave outfit;

        private void Start() {
            UpdateOptions();
            InputHandler.pauseStarted += ShowMenu;
            gameObject.SetActive(false);
        }

        public void UpdateOptions() {
            if (PlayerOutfitLoader.Instance != null) {
                hatDropdown.ClearOptions();
                outfit = PlayerOutfitLoader.Instance.inventory.savedOutfit;
                var ownedClothes = PlayerOutfitLoader.Instance.inventory.ownedClothes;
                hats = (from item in ownedClothes
                        where (item.type == OutfitType.Hat)
                        select item).ToList();
                List<string> hatNames = hats.Select(item => item.itemName).ToList();
                hatDropdown.AddOptions(hatNames);
                hatDropdown.value = hatNames.IndexOf(outfit.hat.itemName);
            }
        }

        private void ShowMenu() {
            InputHandler.Disable.Invoke();
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        public void HatUpdate(int index) {
            outfit.hat = hats[index];
        }

        public void ApplyChanges() {
            InputHandler.Enable.Invoke();
            PlayerOutfitLoader.Instance.LoadOutfit();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
