using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class HierarchyExpander : EditorWindow
{
    [MenuItem("Tools/Hierarchy/Expand All _F1")] // Set your own shortcut key here
    public static void ExpandAll()
    {
        ExpandCollapseAll(true);
    }

    [MenuItem("Tools/Hierarchy/Collapse All _F2")] // Set your own shortcut key here
    static void CollapseAll()
    {
        ExpandCollapseAll(false);
    }

    static void ExpandCollapseAll(bool expand)
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.transform.parent == null)
            {
                SetExpandedRecursive(obj, expand);
            }
        }
    }

    static void SetExpandedRecursive(GameObject obj, bool expand)
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var methodInfo = type.GetMethod("SetExpandedRecursive");

        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        var hierarchyWindow = EditorWindow.focusedWindow;

        methodInfo.Invoke(hierarchyWindow, new object[] { obj.GetInstanceID(), expand });
    }
}

public static class ToggleGameObjectShortcut
{
    [MenuItem("Tools/Hierarchy/Toggle GameObject Active State _F3")]
    static void ToggleGameObjectActiveState()
    {
        if(Selection.activeGameObject != null)
        {
            bool isActive = Selection.activeGameObject.activeSelf;
            Selection.activeGameObject.SetActive(!isActive);
            EditorUtility.SetDirty(Selection.activeGameObject);
        }
    }
}
