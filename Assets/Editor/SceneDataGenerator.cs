#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public static class SceneDataGenerator
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

}
#endif
