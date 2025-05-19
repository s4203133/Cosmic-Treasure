using System.Collections.Generic;
using UnityEngine;

namespace NR {
    /// <summary>
    /// The object for a player's save file.
    /// Will need an alternate method to save properly, as it does not persist when the  game is closed. 
    /// </summary>
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

