
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AddScenesToBuild
{
    [MenuItem("Tools/Scene Data/Update Scenes In Build")]
    public static void AddScenes()
    {
        var scenesGUIDs = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        var editorBuildSettingsScenes = new EditorBuildSettingsScene[scenesGUIDs.Length];

        for (int i = 0; i < scenesGUIDs.Length; i++)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(scenesGUIDs[i]);
            editorBuildSettingsScenes[i] = new EditorBuildSettingsScene(scenePath, true);
        }

        EditorBuildSettings.scenes = editorBuildSettingsScenes;
    }
}
#endif
