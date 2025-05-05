using UnityEngine;
using UnityEditor;

namespace NR {
    [CustomEditor(typeof(LevelSaveManager))]
    public class LevelSaveManagerEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            LevelSaveManager script = (LevelSaveManager)target;

            if (GUILayout.Button("Reset Level Save")) {
                script.ClearSavedGems();
            }
        }
    }
}
