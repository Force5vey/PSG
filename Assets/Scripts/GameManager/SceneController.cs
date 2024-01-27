using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController :MonoBehaviour
{
   public SceneData sceneData;

   //Core scene names
   //TODO: Check here to ensure CoreScenes stay up to date with game development process.
   public enum CoreScene
   {
      ErrorScene,
      LoadingScene,
      MainMenu,
      Cockpit
   }

   public static string GetSceneName( CoreScene scene )
   {
      return scene switch
      {
         CoreScene.ErrorScene => "ErrorScene",
         CoreScene.LoadingScene => "LoadingScene",
         CoreScene.MainMenu => "MainMenu",
         CoreScene.Cockpit => "Cockpit",
         _ => null,
      };
   }

   /// <summary>
   /// Triggers a load scene by converting scene index to scene name.
   /// </summary>
   /// <param name="sceneIndex">Index as tracked in SceneData</param>
   public void LoadLevelByIndex( int sceneIndex )
   {
      if ( sceneIndex >= 0 && sceneIndex < sceneData.scenes.Count )
      {
         string sceneName = sceneData.scenes[sceneIndex].sceneName;
         StartCoroutine(LoadNextSceneAsync(sceneName));
      }
      else
      {
         Debug.LogErrorFormat("Invalid scene index: {0}", sceneIndex);
      }
   }

   /// <summary>
   /// Triggers a load scene by converting CoreScene Enum to a scene name
   /// </summary>
   /// <param name="scene">An enum value from CoreScene</param>
   public void LoadCoreScene( CoreScene scene )
   {
      string sceneName = GetSceneName(scene);

      if ( !string.IsNullOrEmpty(sceneName) )
      {
         Debug.LogFormat("Loading next scene: {0} (Enum: {1})", sceneName, scene);

         StartCoroutine(LoadNextSceneAsync(sceneName));
      }
      else
      {
         Debug.LogErrorFormat("Failed to load next scene: Invalid scene enum provided ({0})", scene);
      }
   }

   private IEnumerator LoadNextSceneAsync( string sceneName )
   {
      if ( !string.IsNullOrEmpty(sceneName) )
      {
         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
         asyncLoad.allowSceneActivation = false;

         // Show loading UI
         ShowLoadingUI();

         // Wait until the scene is almost fully loaded
         while ( !asyncLoad.isDone )
         {
            // Update loading UI, if necessary
            UpdateLoadingUI(asyncLoad.progress);

            // Check if the load is almost done
            if ( asyncLoad.progress >= 0.9f )
            {
               // Wait for user action or specific condition to continue
               if ( UserReadyToProceed() )
               {
                  asyncLoad.allowSceneActivation = true;
               }
            }

            yield return null;
         }

         // Hide loading UI
         HideLoadingUI();
      }
      else
      {
         Debug.LogError("Failed to load scene: Scene name is null or empty");
      }
   }

   // Methods to control the loading UI
   private void ShowLoadingUI()
   {
      GameController.Instance.sceneTransitionScreen.ShowLoadingScreen();
   }

   private void UpdateLoadingUI( float progress )
   {
      GameController.Instance.sceneTransitionScreen.UpdateLoadingScreen(progress.ToString());
   }

   private void HideLoadingUI()
   {
      GameController.Instance.sceneTransitionScreen.HideLoadingScreen();
   }

   private bool UserReadyToProceed()
   {
      //Currently: Immediately finalizes transition. Add button press if you want to wait on user.
      return true;
   }

}
