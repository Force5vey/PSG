using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEditor.Experimental.GraphView;

using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class LoadingDependencyChecker :MonoBehaviour
{
   private void Start()
   {
      Debug.LogWarning("Start() of LoadingDependencyChecker");

      StartCoroutine(CheckDependencies());

   }
   private IEnumerator CheckDependencies()
   {
      // Initial delay before the first check
      yield return new WaitForSeconds(5);
      LoadingSceneController loadingScene = GetComponent<LoadingSceneController>();

      if(GameController.Instance == null || loadingScene == null)
      {
         HandleCriticalError("Critical Error, Shutting Down");
      }
      yield return null;
   }

   private void HandleCriticalError( string errorMessage )
   {
      Debug.LogError(errorMessage);

      SceneManager.LoadScene("ErrorScene");
   }
}
