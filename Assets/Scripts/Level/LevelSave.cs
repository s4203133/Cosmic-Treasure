using System.Collections.Generic;
using UnityEngine;

namespace NR {
    /// <summary>
    /// Object that keeps track of level data for a save file.
    /// Currently keeps track of which gems were collected.
    /// </summary>
    [CreateAssetMenu(menuName = "Player Save/Level Save")]
    public class LevelSave : ScriptableObject {
        public List<int> gemsCollected = new List<int>();
    }
}
