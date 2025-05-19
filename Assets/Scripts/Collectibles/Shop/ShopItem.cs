using UnityEngine;

namespace NR {
    /// <summary>
    /// Base class for any item that can be bought in the shop.
    /// This includes outfit items and upgrades.
    /// </summary>
    public class ShopItem : ScriptableObject {
        public string itemName;
        public string itemDescription;
        public Sprite itemIcon;
    }
}
