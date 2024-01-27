using System.Collections;

using UnityEngine;

public class LevelController : MonoBehaviour
{
     [Header("Level Scripts")]
     public LevelData levelData;

     public CameraFollowController cameraFollowScript;

     [Header("Player Scripts")]
     public PlayerSpawner playerSpawner;

     //[Header("Player Effects")]


     private void Start()
     {
      if ( playerSpawner != null && PlayerController.Instance != null )
      {
         //cameraFollowScript.target = PlayerController.Instance.transform;
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
          if (!PlayerController.Instance.gameObject.activeSelf)
          {
               PlayerController.Instance.gameObject.SetActive(true);
          }
          //Pass on levelData to player controller for things like position checks compared to boundary
          PlayerController.Instance.SetLevelData(levelData);

          // Position the player at the spawner's location
          PlayerController.Instance.transform.position = playerSpawner.spawnPosition;

      //Tell the ship to initialize its boundary
      //PlayerController.Instance.playerMovementController.InitializeBoundary();


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
            playerCamera.tag = "MainCamera"; // Optionally, ensure it's tagged as MainCamera

            // Disable other cameras if necessary
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
          if (levelSpecificScripts.Length == 1)
          {
               // Optionally check if the script name matches the level name
               if (levelSpecificScripts[0].GetType().Name == levelData.levelName)
               {
                    levelSpecificScripts[0].CustomStart();
               }
               else
               {
                    Debug.LogError("The level-specific script name does not match the level name in LevelData.");
               }
          }
          else if (levelSpecificScripts.Length > 1)
          {
               Debug.LogError("Multiple level-specific scripts implementing ILevelSpecific found. There should only be one.");
          }
          else
          {
               Debug.LogWarning("No level-specific script implementing ILevelSpecific found on this GameObject.");
          }
     }

     private IEnumerator CameraZoom(Vector3 newZoom, float waitTimeSeconds, float zoomDuration)
     {
          yield return new WaitForSeconds(waitTimeSeconds);
          //cameraFollowScript.StartZoom(newZoom, zoomDuration);
     }

     private void EndLevel()
     {
       
          //TODO: Everything should be 'saved' with the player and game data, but when switching scenes trigger a save to disk game save.
          //this will need to be managed between loading the cockpit scene async and if actually switching levels.
     }

     /// <summary>
     /// Ship rotation needed at a minimum to get ship oriented for game play.
     /// </summary>
     /// <param name="targetRotation">-90,0,0 gets ship to standard orientation.</param>
     /// <param name="duration">Time in seconds to take to rotate ship</param>
     /// <returns></returns>
     private IEnumerator AlignShipRotation(Quaternion targetRotation, float duration)
     {
          Transform shipTransform = PlayerController.Instance.GetShipTransform();
          if (shipTransform == null)
          {
               Debug.LogError("Ship GameObject not found");
               yield break;
          }

          float currentTime = 0f;
          Quaternion startRotation = shipTransform.rotation;

          while (currentTime < duration)
          {
               currentTime += Time.deltaTime;
               float fraction = currentTime / duration; // This will go from 0 to 1

               shipTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, fraction);

               yield return null; // Wait for the next frame
          }

          // Ensure the rotation is exactly the target rotation
          shipTransform.rotation = targetRotation;
     }
}