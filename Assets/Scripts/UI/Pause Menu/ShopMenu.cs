using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace NR {
    public class ShopMenu : MonoBehaviour {
        [SerializeField]
        private List<PlayerOutfitItem> buyableClothes;

        [SerializeField]
        private TextMeshProUGUI moneyDisplay;

        [SerializeField]
        private TMP_Dropdown clothesDropdown;

        [SerializeField]
        private TextMeshProUGUI itemDescription;

        [SerializeField]
        private TextMeshProUGUI buyPopup;

        private PlayerOutfitItem selectedOutfitItem;

        private PlayerInventorySave inventorySave;

        private void Start() {
            inventorySave = PlayerSaveLoader.Instance.playerSave;
            moneyDisplay.text = $"{inventorySave.coins.value.ToString("000")}\n{inventorySave.gems.value.ToString("000")}";
            foreach (var item in inventorySave.ownedClothes) {
                if (buyableClothes.Contains(item)) {
                    buyableClothes.Remove(item);
                }
            }
            ListItemNames();
        }

        private void ListItemNames() {
            clothesDropdown.ClearOptions();
            var clothesNames = buyableClothes.Select(item => item.itemName).ToList();
            clothesNames.Insert(0, "");
            clothesDropdown.AddOptions(clothesNames);
            clothesDropdown.value = 0;
        }

        public void OutfitItemSelected(int index) {
            string descriptionText = "";
            if (index <= 0) {
                selectedOutfitItem = null;
            } else {
                selectedOutfitItem = buyableClothes[index - 1];
                descriptionText = $"Price: {selectedOutfitItem.coinCost} coins\n{selectedOutfitItem.itemDescription}\nType: {selectedOutfitItem.type}";
            }
            itemDescription.text = descriptionText;
        }

        public void BuyItem() {
            if (selectedOutfitItem == null) {
                ShowBuyPopup("No item selected!");
                return;
            }
            if (inventorySave.coins.value < selectedOutfitItem.coinCost) {
                ShowBuyPopup("Not enough coins!");
            } else {
                inventorySave.coins.value -= selectedOutfitItem.coinCost;
                moneyDisplay.text = $"{inventorySave.coins.value.ToString("000")}\n{inventorySave.gems.value.ToString("000")}";
                ShowBuyPopup($"{selectedOutfitItem.itemName} bought!");
                inventorySave.ownedClothes.Add(selectedOutfitItem);
                buyableClothes.Remove(selectedOutfitItem);
                itemDescription.text = "";
                ListItemNames();
            }
        }

        private void ShowBuyPopup(string text) {
            StopAllCoroutines();
            StartCoroutine(ShowPopupCoroutine(text));
        }

        private IEnumerator ShowPopupCoroutine(string text) {
            buyPopup.text = text;
            yield return new WaitForSeconds(3);
            buyPopup.text = "";
        }
    }
}
