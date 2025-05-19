using UnityEngine;

namespace NR {
    // Currently only hats, but any type can be added here.
    public enum OutfitType {
        Hat
    }

    /// <summary>
    /// A purchaseable outfit item that can be equipped on the player.
    /// Created in-editor and assigned to a shop.
    /// Equipped at runtime by PlayerSaveLoader.
    /// </summary>
    [CreateAssetMenu(menuName = "Shop Items/Player Outfit Item")]
    public class PlayerOutfitItem : ShopItem {
        public GameObject clothesPrefab;
        public OutfitType type;
        public int coinCost;
    }
}
