using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelData levelData = (LevelData)target;
        string sceneDataPath = "Assets/Scenes/SceneData/SceneData.asset";
        SceneData sceneData = AssetDatabase.LoadAssetAtPath<SceneData>(sceneDataPath);

        if (sceneData == null)
        {
            EditorGUILayout.HelpBox("SceneData asset not found!", MessageType.Error);
            return;
        }

        //Dropdown for selecting the scene
        string[] sceneNames = sceneData.GetSceneNames();
        int currentSceneIndex = sceneData.GetSceneIndexByName(levelData.levelName);
        int selectedSceneIndex = EditorGUILayout.Popup("Select Scene", currentSceneIndex, sceneNames);

        if (selectedSceneIndex != currentSceneIndex)
        {
            SceneData.SceneInfo selectedSceneInfo = sceneData.scenes[selectedSceneIndex];
            levelData.sceneIndex = selectedSceneInfo.sceneIndex;
            levelData.levelName = selectedSceneInfo.sceneName;

            string newName = selectedSceneInfo.sceneName + "Data";
            string assetPath = AssetDatabase.GetAssetPath(levelData);
            AssetDatabase.RenameAsset(assetPath, newName);

            EditorUtility.SetDirty(levelData);
        }

        DrawDefaultInspector();
    }
}
