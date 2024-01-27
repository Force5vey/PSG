using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingDependencyChecker :MonoBehaviour
{
   private void Start()
   {
      Debug.Log("Start() of LoadingDependencyChecker");

      StartCoroutine(CheckDependencies());

   }
   private IEnumerator CheckDependencies()
   {
      // Initial delay before the first check
      yield return new WaitForSeconds(10);
      if ( GameController.Instance == null )
      {
         HandleCriticalError("Critical Error.");
      }

      yield return null;
   }

   private void HandleCriticalError( string errorMessage )
   {
      Debug.LogError(errorMessage);

      SceneManager.LoadScene("ErrorScene");
   }
}
