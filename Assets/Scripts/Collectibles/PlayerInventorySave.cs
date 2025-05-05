using System.Collections.Generic;
using UnityEngine;

namespace NR {
    [CreateAssetMenu(menuName = "Player Save/Player Inventory Save")]
    public class PlayerInventorySave : ScriptableObject {
        public FloatVariable coins;

        public FloatVariable gems;

        public PlayerOutfitSave savedOutfit;

        public List<PlayerOutfitItem> ownedClothes;

        public List<PlayerUpgrade> ownedUpgrades;

        public List<LevelSave> levelSaves;
    }
}

