using System.Collections;

using UnityEngine;

public class LevelController :MonoBehaviour
{
   [Header("Level Scripts")]
   public LevelData levelData;

   [Header("Player Scripts")]
   public PlayerSpawner playerSpawner;

   private void Start()
   {
      if ( playerSpawner != null && PlayerController.Instance != null )
      {
         StartLevel();
      }
      else
      {
         Debug.LogError("Critical Error at LevelController Start");
      }
   }

   /// <summary>
   /// Focused on standard level starts that happen for every level
   /// Use Level Data for level specific variables
   /// Overly specific level logic should be handled in the level specific controller.
   /// </summary>
   private void StartLevel()
   {

      // Activate the player object if it's not already active
      if ( !PlayerController.Instance.gameObject.activeSelf )
      {
         PlayerController.Instance.gameObject.SetActive(true);
      }
      //Pass on levelData to player controller for things like position checks compared to boundary
      PlayerController.Instance.SetLevelData(levelData);

      // Position the player at the spawner's location
      PlayerController.Instance.transform.position = playerSpawner.spawnPosition;

      PlayerController.Instance.playerMovementController.InitializeBoundary();

      SetPlayerCameraAsMain();

      //Run this last or when ready for level specific controller to initialize
      RunLevelSpecificControllerStartRoutine();
   }

   private void SetPlayerCameraAsMain()
   {
      if ( PlayerController.Instance != null )
      {
         // Find the camera child in the player GameObject
         Camera playerCamera = PlayerController.Instance.GetComponentInChildren<Camera>();

         if ( playerCamera != null )
         {
            // Enable the player's camera
            playerCamera.gameObject.SetActive(true);
            playerCamera.tag = "MainCamera";

            // Disable other cameras
            foreach ( var cam in Camera.allCameras )
            {
               if ( cam != playerCamera )
               {
                  cam.gameObject.SetActive(false);
               }
            }
         }
      }
   }

   private void RunLevelSpecificControllerStartRoutine()
   {
      ILevelSpecific[] levelSpecificScripts = GetComponents<ILevelSpecific>();
      if ( levelSpecificScripts.Length == 1 )
      {
         // Check if the script name matches the level name
         if ( levelSpecificScripts[0].GetType().Name == levelData.levelName )
         {
            levelSpecificScripts[0].CustomStart();
         }
         else
         {
            Debug.LogError("The level-specific script name does not match the level name in LevelData.");
         }
      }
      else if ( levelSpecificScripts.Length > 1 )
      {
         Debug.LogError("Multiple level-specific scripts implementing ILevelSpecific found. There should only be one.");
      }
      else
      {
         Debug.LogWarning("No level-specific script implementing ILevelSpecific found on this GameObject.");
      }
   }

   private void EndLevel()
   {
      //TODO: Everything should be 'saved' with the player and game data, but when switching scenes trigger a save to disk game save.
      //this will need to be managed between loading the cockpit scene async and if actually switching levels.
   }

}