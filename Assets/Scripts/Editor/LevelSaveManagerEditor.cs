using UnityEngine;
using UnityEditor;

namespace NR {
    /// <summary>
    /// Simple editor functionality to quickly reset level saves, for development.
    /// </summary>
    [CustomEditor(typeof(LevelSaveManager))]
    public class LevelSaveManagerEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            LevelSaveManager script = (LevelSaveManager)target;

            if (GUILayout.Button("Reset Level Save")) {
                script.ClearData();
            }
        }
    }
}
