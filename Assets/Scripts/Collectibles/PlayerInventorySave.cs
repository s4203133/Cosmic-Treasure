using System.Collections.Generic;
using UnityEngine;

namespace NR {
    [CreateAssetMenu(menuName = "Player Save/Player Inventory Save")]
    public class PlayerInventorySave : ScriptableObject {
        public int coins;

        public int gems;

        public PlayerOutfitSave savedOutfit;

        public List<PlayerOutfitItem> ownedClothes;

        public List<PlayerUpgrade> ownedUpgrades;
    }
}

