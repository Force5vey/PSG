using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    [Header("Zoom Effect")]
    [SerializeField] private GameObject cockpitPanel;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private Vector3 zoomedScale;
    [SerializeField] private float zoomDuration = 2f;

    [Header("Fade-In Effect")]
    [SerializeField] private Image[] imagesToFadeIn;
    [SerializeField] private float[] fadeInDelays;
    [SerializeField] private float[] fadeInDurations;
    [SerializeField] private float[] startOpacities;
    [SerializeField] private float[] maxOpacities;

    [Header("Screen Panel")]
    [SerializeField] private CanvasGroup screenPanel;
    

    private void Awake()
    {
        
    }

    void Start()
    {
        screenPanel.alpha = 0f;

        // Setup Zoom Effect
        originalScale = cockpitPanel.transform.localScale;
        cockpitPanel.transform.localScale = zoomedScale;
        StartCoroutine(ZoomOut(cockpitPanel));

        // Setup Fade-In Effects
        for (int i = 0; i < imagesToFadeIn.Length; i++)
        {
            Image img = imagesToFadeIn[i];
            float delay = fadeInDelays.Length > i ? fadeInDelays[i] : 0f;
            float duration = fadeInDurations.Length > i ? fadeInDurations[i] : 0f;
            float startOpacity = startOpacities.Length > i ? startOpacities[i] : 0f;
            float maxOpacity = maxOpacities.Length > i ? maxOpacities[i] : 1f;

            img.color = new Color(img.color.r, img.color.g, img.color.b, startOpacity);

            StartCoroutine(FadeInImage(img, delay, duration, startOpacity, maxOpacity));

        }

    }

    void Update()
    {

    }

    public void ExecuteButtonAction(string buttonActionName)
    {
        switch (buttonActionName)
        {
            case "Navigate":
                OnNavigateButton_Click();
                break;
            case "MapBack":
                OnMapBackButton_Click();
                break;
            case "World1_1":
                OnWorld1_1_Click();
                break;
        }
    }

    private void OnWorld1_1_Click()
    {
        GameController.Instance.sceneController.LoadNextScene(GameController.Instance.sceneController.sceneData.scenes[3].sceneName);
    }

    private void OnNavigateButton_Click()
    {
        StartCoroutine(FadeInImage(imagesToFadeIn[2], 0, 1, imagesToFadeIn[2].color.a, 1f));

        //Show level buttons
        StartCoroutine(FadeInCanvasGroup(screenPanel, 1f, .5f, screenPanel.alpha, 1f));


        //set event system to disable console buttons and activate navigation screen buttons

    }

    private void OnMapBackButton_Click()
    {
        GameController.Instance.sceneController.LoadNextScene(GameController.Instance.sceneController.sceneData.scenes[1].sceneName);
    }

    IEnumerator ZoomOut(GameObject zoomTarget)
    {
        float currentTime = 0;

        while (currentTime < zoomDuration)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime / zoomDuration;
            zoomTarget.transform.localScale = Vector3.Lerp(zoomedScale, originalScale, progress);
            yield return null;
        }

        zoomTarget.transform.localScale = originalScale;
    }

    IEnumerator FadeInImage(Image img, float delay, float duration, float startOpacity, float maxOpacity)
    {
        yield return new WaitForSeconds(delay);
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / duration);
            float alpha = Mathf.Lerp(startOpacity, maxOpacity, progress);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set to the max opacity
        img.color = new Color(img.color.r, img.color.g, img.color.b, maxOpacity);
    }

    IEnumerator FadeInCanvasGroup(CanvasGroup group, float delay, float duration, float startOpacity, float maxOpacity)
    {
        Debug.Log("FadeINCanvasGroup");

        yield return new WaitForSeconds(delay);
        float currentTime = 0;
        
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime/duration);
            float alpha = Mathf.Lerp(startOpacity, maxOpacity, progress);
            group.alpha = alpha;

            yield return null;
        }

        group.alpha = maxOpacity;
    }

   
}
