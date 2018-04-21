using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshPaint)), CanEditMultipleObjects]
public class MeshPaintEditor : Editor
{
	private MeshPaint script;

	void OnSceneGUI()
	{
		if (script == null)
			script = target as MeshPaint;

		Event e = Event.current;

		Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
		Vector3 mousePosition = ray.GetPoint(0);
		mousePosition.z = 0;

		if(e.type == EventType.MouseDown && e.button == 1)
		{
			script.AddPoint(mousePosition);
		}
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical("box");
		DrawDefaultInspector();

		if (GUILayout.Button("Triangulate"))
		{
			script.Triangulate();
		}

		if(GUILayout.Button("Reset"))
		{
			script.Reset();
			SceneView.RepaintAll();
		}

		GUILayout.EndVertical();
	}
}
