using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class DefaultPlayModeScene
{
    private const string MenuName = "Tools/Play From Loading Scene";
    private const string EditorPrefKey = "DefaultPlayModeSceneEnabled";
    private static string previousScenePath;

    static DefaultPlayModeScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        UpdateMenuCheckedState();
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (!EditorPrefs.GetBool(EditorPrefKey, true)) return;

        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                SaveCurrentScenePath();
                OpenDefaultScene();
                break;
            case PlayModeStateChange.EnteredEditMode:
                LoadPreviousScene();
                break;
        }
    }

    private static void SaveCurrentScenePath()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.isDirty)
        {
            EditorSceneManager.SaveScene(currentScene);
    }
    previousScenePath = currentScene.path;
    }

    private static void OpenDefaultScene()
    {
            // Replace "YourSceneName" with the name of the scene you want to load
        EditorSceneManager.OpenScene("Assets/Scenes/LoadingScene.unity");
        
    }

    private static void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScenePath))
        {
            EditorSceneManager.OpenScene(previousScenePath);
        }
    }

    [MenuItem(MenuName)]
    private static void ToggleMenuOption()
    {
        bool currentState = EditorPrefs.GetBool(EditorPrefKey, true);
        EditorPrefs.SetBool(EditorPrefKey, !currentState);
        UpdateMenuCheckedState();
    }

    private static void UpdateMenuCheckedState()
    {
        Menu.SetChecked(MenuName, EditorPrefs.GetBool(EditorPrefKey, true));
    }
}
