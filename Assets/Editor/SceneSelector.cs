using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SceneSelector : EditorWindow
{

     // Custom Editor Window
     [MenuItem("Tools/Scene Selector (Window)")]
     public static void ShowSceneSelector()
     {
          //EditorWindow.GetWindow(typeof(SceneSelector), false, "Scene Selector");

          GetWindow<SceneSelector>("Scenes");
     }

     private SceneData sceneData;
     private Vector2 scrollPosition;

     private void OnEnable()
     {
          EditorApplication.delayCall += DelayedInitialization;
     }

     private void DelayedInitialization()
     {
          sceneData = AssetDatabase.LoadAssetAtPath<SceneData>("Assets/Scenes/SceneData/SceneData.asset");

          EditorApplication.delayCall -= DelayedInitialization;
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
