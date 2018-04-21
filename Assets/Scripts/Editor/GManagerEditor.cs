using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GManager))]
public class GManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        DrawDefaultInspector();
        GUILayout.EndVertical();
    }
}