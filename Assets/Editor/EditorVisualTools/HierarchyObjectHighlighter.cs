using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyObjectHighlighter
{
    static HierarchyObjectHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }

    private static void HierarchyItemCB(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject != null)
        {
            if (gameObject.GetComponent<CanvasGroup>() != null)
            {
                // Highlight GameObjects with CanvasGroup component in dark green
                EditorGUI.DrawRect(selectionRect, new Color(0.0f, 0.6f, 0.0f, 0.1f));
            }
            else if (gameObject.name.Contains("Panel"))
            {
                // Highlight GameObjects with 'Panel' in their name in light green
                EditorGUI.DrawRect(selectionRect, new Color(0.5f, 1.0f, 0.5f, 0.1f));
            }
            else if (gameObject.name.Contains("Sub"))
            {
                // Highlight gameObjects with 'Sub' in their name in light orange
                EditorGUI.DrawRect(selectionRect, new Color(0.16f, 0.70f, 0.71f, 0.1f));
            }
            else if (gameObject.name.Contains("Boundaries"))
            {
                EditorGUI.DrawRect(selectionRect, new Color(0.44f, 0.81f, 0.89f, 0.1f));
            }
        }
    }
}
