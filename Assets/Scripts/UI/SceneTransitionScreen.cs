using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class SceneTransitionScreen :MonoBehaviour
{
   [SerializeField] private CanvasGroup canvasGroup;
   [SerializeField] private TextMeshProUGUI headingText;
   [SerializeField] private TextMeshProUGUI messageText;
   [SerializeField] private float fadeDuration = 1f;

   public void ShowLoadingScreen()
   {

      headingText.text = "Loading . . .";
      messageText.text = "";
      StartCoroutine(FadeLoadingScreen(1, fadeDuration));
   }

   public void UpdateLoadingScreen(string loadProgress)
   {
      messageText.text = loadProgress;
   }

   public void HideLoadingScreen()
   {
      StartCoroutine(FadeLoadingScreen(0, fadeDuration));
   }

   private IEnumerator FadeLoadingScreen( float targetAlpha, float duration )
   {
      float startAlpha = canvasGroup.alpha;
      float time = 0;

      while ( time < duration )
      {
         canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
         time += Time.deltaTime;
         yield return null;
      }

      canvasGroup.alpha = targetAlpha;
      if ( targetAlpha == 0 )
      {
         canvasGroup.gameObject.SetActive(false);
      }
      else
      {
         canvasGroup.gameObject.SetActive(true);
      }

   }
}
