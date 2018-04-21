using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BouncyBall))]
public class BouncyBallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        DrawDefaultInspector();
        GUILayout.TextArea("Ghi chú:\nForce divide chia force cho lần đầu va chạm nhằm giảm lực");
        GUILayout.EndVertical();
    }
}