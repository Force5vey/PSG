using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorScreen : MonoBehaviour
{


   private void Awake()
   {
      PlayerController playerController = FindObjectOfType<PlayerController>();
      if ( playerController != null )
      {
         GameObject playerGameObject = playerController.gameObject;

         Destroy(playerGameObject);
      }
      else
      {
         // Handle the case where no object with PlayerController was found
      }

   }
   public void QuitGame()
   {
#if UNITY_EDITOR
      // This will only be compiled and executed in the Unity Editor
      UnityEditor.EditorApplication.isPlaying = false;
#else
    // This code will run in a built game
    Application.Quit();
#endif
   }
}
