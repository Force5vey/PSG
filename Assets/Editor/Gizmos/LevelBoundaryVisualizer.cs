using UnityEngine;
using UnityEditor;

public class LevelBoundaryVisualizer : EditorWindow
{
    private LevelData levelData;
    private GameObject playerSpawner;
    private float tDriveState; // Replace with the appropriate type if it's an enum
    private bool visualizeBoundary = false;

    [MenuItem("Tools/Level Boundary Visualizer (Window)")]
    public static void ShowWindow()
    {
        GetWindow<LevelBoundaryVisualizer>("Level Boundary Visualizer");
    }

    void OnGUI()
    {
        GUILayout.Label("Level Boundary Settings", EditorStyles.boldLabel);

        levelData = (LevelData)EditorGUILayout.ObjectField("Level Data", levelData, typeof(LevelData), false);
        playerSpawner = (GameObject)EditorGUILayout.ObjectField("Player Spawner", playerSpawner, typeof(GameObject), true);
        tDriveState = EditorGUILayout.FloatField("T-Drive State", tDriveState);

        if (GUILayout.Button(visualizeBoundary ? "Hide Boundary" : "Visualize Boundary"))
        {
            visualizeBoundary = !visualizeBoundary;
            SceneView.RepaintAll();
        }
    }

    void OnFocus()
    {
        SceneView.duringSceneGui += OnScene;
    }

    void OnDestroy()
    {
        SceneView.duringSceneGui -= OnScene;
    }

    void OnScene(SceneView sceneView)
    {
        if (visualizeBoundary && levelData != null && playerSpawner != null)
        {
            Handles.color = Color.yellow;
            Vector3 centerPoint = playerSpawner.transform.position;
            float maxRadius = levelData.maxRadius + tDriveState; // Adjust based on your logic

            // Draw the circle in the X,Y plane
            Handles.DrawWireDisc(centerPoint, Vector3.forward, maxRadius);
        }
    }
}

