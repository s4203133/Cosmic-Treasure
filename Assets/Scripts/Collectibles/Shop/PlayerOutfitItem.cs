using UnityEngine;

namespace NR {
    public enum OutfitType {
        Hat
    }

    [CreateAssetMenu(menuName = "Shop Items/Player Outfit Item")]
    public class PlayerOutfitItem : ShopItem {
        public GameObject clothesPrefab;
        public OutfitType type;
        public int coinCost;
    }
}
