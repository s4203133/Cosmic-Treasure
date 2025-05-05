using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR {
    [CreateAssetMenu(menuName = "Player Save/Level Save")]
    public class LevelSave : ScriptableObject {
        public List<int> gemsCollected = new List<int>();
    }
}
