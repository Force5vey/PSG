using UnityEditor;
using UnityEngine;

public class PrefabFolderViewer : EditorWindow
{
    private Vector2 scrollPosition;
    private string folderPath = "Assets/Prefabs"; // Set your prefab folder path here

    [MenuItem("Tools/Prefab Folder (Window)")]
    public static void ShowWindow()
    {
        GetWindow<PrefabFolderViewer>("Prefab Viewer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefabs in Folder: " + folderPath, EditorStyles.boldLabel);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (GUILayout.Button(prefab.name))
            {
                EditorGUIUtility.PingObject(prefab);
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
