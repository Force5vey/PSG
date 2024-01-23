using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class LevelScriptCreatorWindow : EditorWindow
{
    private int selectedSceneIndex = 0;
    private string[] sceneNames;
    private string[] scenePaths;

    [MenuItem("Tools/Create/Level Specific Controller", false, 51)]
    public static void ShowWindow()
    {
        // Retrieve scenes from build settings
        var scenes = EditorBuildSettings.scenes;
        if (scenes == null || scenes.Length == 0)
        {
            EditorUtility.DisplayDialog("Error", "No scenes found in Build Settings!", "OK");
            return;
        }

        // Prepare scene names and paths
        var window = GetWindow<LevelScriptCreatorWindow>("Create Level Specific Controller");
        window.sceneNames = scenes.Select(s => Path.GetFileNameWithoutExtension(s.path)).ToArray();
        window.scenePaths = scenes.Select(s => s.path).ToArray();
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Select Scene", EditorStyles.boldLabel);
        selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames);

        if (GUILayout.Button("Create Script"))
        {
            CreateScriptForSelectedScene();
            Close();
        }
    }

    private void CreateScriptForSelectedScene()
    {
        string selectedScenePath = scenePaths[selectedSceneIndex];
        string selectedSceneName = Path.GetFileNameWithoutExtension(selectedScenePath);

        // Extract folder path from the scene path
        string folderPath = Path.GetDirectoryName(selectedScenePath);
        if (!Directory.Exists(folderPath))
        {
            EditorUtility.DisplayDialog("Error", "Scene folder does not exist: " + folderPath, "OK");
            return;
        }

        string scriptName = selectedSceneName.Replace(" ", ""); // Replace spaces if necessary
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        if (File.Exists(scriptPath))
        {
            EditorUtility.DisplayDialog("Error", "Script already exists!", "OK");
            return;
        }

        // Generate script content
        string scriptContent = GenerateScriptContent(scriptName);
        File.WriteAllText(scriptPath, scriptContent);

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Level specific script created: " + scriptName, "OK");
    }

    private static string GenerateScriptContent(string className)
    {
        return
$@"using UnityEngine;

public class {className} : MonoBehaviour, ILevelSpecific
{{
    public void CustomStart()
    {{
        // Custom start logic for {className}
    }}
}}";
    }
}
