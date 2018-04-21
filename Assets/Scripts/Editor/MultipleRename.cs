using UnityEngine;
using UnityEditor;
using System.Collections;

public class MultipleRename : ScriptableWizard
{
    public string BaseName = "Tên";

    [MenuItem("D/Đặt tên objects...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Đặt tên objects...", typeof(MultipleRename), "Đồng ý");
    }

    //Called when the window first appears
    void OnEnable()
    {
        UpdateSelectionHelper();
    }

    //Function called when selection changes in scene
    void OnSelectionChange()
    {
        UpdateSelectionHelper();
    }

    //Update selection counter
    void UpdateSelectionHelper()
    {
        helpString = "";

        if (Selection.objects != null)
            helpString = "Số lượng object đã chọn (Hierarchy): " + Selection.objects.Length;
    }

    //Rename
    void OnWizardCreate()
    {
        if (Selection.objects == null)
            return;

		for (int i = 0; i < Selection.objects.Length; ++i)
		{
			Selection.objects[i].name = BaseName + " " + "[" + i + "]";
		}
    }
}