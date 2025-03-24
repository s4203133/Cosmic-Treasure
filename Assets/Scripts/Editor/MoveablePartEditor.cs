using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NR {
    [CustomEditor(typeof(MoveablePart))]
    [CanEditMultipleObjects]
    public class MoveablePartEditor : Editor {

        SerializedProperty startPos;
        SerializedProperty endPos;

        private void OnEnable() {
            SerializedObject serialisedTarget = this.serializedObject;
            startPos = serialisedTarget.FindProperty("start");
            endPos = serialisedTarget.FindProperty("end");
        }

        public override void OnInspectorGUI() { 
            
            SerializedObject serialisedTarget = this.serializedObject;
            serialisedTarget.Update();

            DrawDefaultInspector();

            MoveablePart script = (MoveablePart)target;

            GUILayoutOption[] buttonParams = { GUILayout.Width(170), GUILayout.Height(25) };

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set start to current position", buttonParams)) {
                startPos.vector3Value = script.transform.position;
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Set end to current position", buttonParams)) {
                endPos.vector3Value = script.transform.position;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Go to start", buttonParams)) {
                script.transform.position = script.start;
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Go to end", buttonParams)) {
                script.transform.position = script.end;
            }
            GUILayout.EndHorizontal();

            serialisedTarget.ApplyModifiedProperties();
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

