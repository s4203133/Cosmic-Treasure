using UnityEngine;

namespace NR {
    /// <summary>
    /// The equipped outfit of the player, to be loaded in every level.
    /// </summary>
    [CreateAssetMenu(menuName = "Player Save/Player Outfit Save")]
    public class PlayerOutfitSave : ScriptableObject {
        public PlayerOutfitItem hat;
        public Material material;
    }
}