#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SceneDataGenerator
{
    [MenuItem("Tools/Scene Data/Update SceneData Script Object")]
    public static void UpdateSceneDataList()
    {
        SceneData sceneData = AssetDatabase.LoadAssetAtPath<SceneData>("Assets/Scenes/SceneData/SceneData.asset");

        if (sceneData == null)
        {
            Debug.LogError("SceneData asset not found!");
            return;
        }

        sceneData.scenes.Clear();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                int sceneIndex = SceneUtility.GetBuildIndexByScenePath(scene.path);

                SceneData.SceneInfo newSceneInfo = new SceneData.SceneInfo()
                {
                    sceneIndex = sceneIndex,
                    sceneName = sceneName,
                };

                sceneData.scenes.Add(newSceneInfo);
            }
        }

        EditorUtility.SetDirty(sceneData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    // Custom Editor Window
    [MenuItem("Tools/Scene Selector (Window)")]
    public static void ShowSceneSelector()
    {
        EditorWindow.GetWindow(typeof(SceneSelectorWindow), false, "Scene Selector");
    }
}

public class SceneSelectorWindow : EditorWindow
{
    private SceneData sceneData;
    private Vector2 scrollPosition;

    private void OnEnable()
    {
        sceneData = AssetDatabase.LoadAssetAtPath<SceneData>("Assets/Scenes/SceneData/SceneData.asset");
    }

    private void OnGUI()
    {
        if (sceneData == null)
        {
            EditorGUILayout.HelpBox("SceneData asset not found!", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var scene in sceneData.scenes)
        {
            if (GUILayout.Button(scene.sceneName))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes.First(s => s.path.Contains(scene.sceneName)).path);
                    HierarchyExpander.ExpandAll();
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
#endif
