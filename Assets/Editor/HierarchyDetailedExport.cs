using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Linq;

public class DetailedHierarchyExporter
{
    [MenuItem("Tools/Export/Export Hierarchy")]
    public static void ExportHierarchy()
    {
        StringBuilder stringBuilder = new StringBuilder();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(go => go.transform.parent == null).ToArray();

        Array.Sort(allObjects, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));

        foreach (GameObject go in allObjects)
        {
            TraverseHierarchy(go.transform, 0, stringBuilder);
        }

        string date = DateTime.Now.ToString("yyyy_MM_dd");
        string defaultName = "Hierarchy_Export.txt";
        string filePath = EditorUtility.SaveFilePanel("Save Detailed Hierarchy", "", defaultName, "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, stringBuilder.ToString());
            EditorUtility.DisplayDialog("Export Complete", "Detailed hierarchy exported to " + filePath, "OK");
        }
    }

    private static void TraverseHierarchy(Transform parent, int depth, StringBuilder stringBuilder)
    {
        string indent = new string(' ', depth * 4);
        string line = depth == 0 ? parent.name + "/" : indent + "├── " + parent.name;

        // Add GameObject's transform details
        line += $" [Position: {parent.position}]";

        // Add attached components information
        var components = parent.GetComponents<Component>();
        if (components.Length > 1) // Always at least one component (Transform)
        {
            line += " {Components: " + string.Join(", ", components.Select(comp => comp.GetType().Name)) + "}";
        }

        stringBuilder.AppendLine(line);

        // Now deal with the children
        foreach (Transform child in parent)
        {
            TraverseHierarchy(child, depth + 1, stringBuilder);
        }
    }
}
