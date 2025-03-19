using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NR {
    [CustomEditor(typeof(MoveablePart))]
    public class MoveablePartEditor : Editor {

        public override void OnInspectorGUI() { 
            MoveablePart script = (MoveablePart)target;

            DrawDefaultInspector();

            GUILayoutOption[] buttonParams = { GUILayout.Width(170), GUILayout.Height(25) };

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set start to current position", buttonParams)) {
                script.start = script.transform.position;
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Set end to current position", buttonParams)) {
                script.end = script.transform.position;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set current position to start", buttonParams)) {
                script.transform.position = script.start;
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Set current position to end", buttonParams)) {
                script.transform.position = script.end;
            }
            GUILayout.EndHorizontal();
        }

        public void OnSceneGUI() {
            MoveablePart script = (MoveablePart)target;

            GUIStyle style = GUI.skin.box;
            style.alignment = TextAnchor.MiddleCenter;

            Handles.color = Color.red;

            Handles.DrawLine(script.start, script.end, 5);
            Handles.Label(script.start, "Start", style);
            Handles.Label(script.end, "End", style);
            if (script.activator != null) {
                Handles.Label(script.activator.transform.position, "Activator", style);
            }
        }
    }
}

