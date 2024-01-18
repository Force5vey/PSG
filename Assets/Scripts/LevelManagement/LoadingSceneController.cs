using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//#if UNITY_EDITOR

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] CanvasGroup notificationCanvasGroup;
    public TextMeshProUGUI headingText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI titleText;

    //Scene Transition effects
    [SerializeField] Image sceneTransitionImage;
    [SerializeField] float fadeDuration;

    private void Awake()
    {
        notificationCanvasGroup.gameObject.SetActive(false);
        messageText.text = string.Empty;

    }

    private void Start()
    {
        GameController.Instance.InitializeLoadingSceneControllerInGameController();
        InitiateGameLoadSequence();

        headingText.color = GameController.Instance.uiController.textColorSelected;
        messageText.color = GameController.Instance.uiController.textColorSelected;
        titleText.color = GameController.Instance.uiController.textColorSelected;
    }

    private void Update()
    {
        //Keep Debug.Logs while checking for varying data status testing
        Debug.Log("Reached LoadingScene Controller Update().  >  " + "Game Data Load Status: " + GameController.Instance.dataController.gameDataLoadStatus +
            "  >  Waiting Game Data Acknowledgement: " + GameController.Instance.dataController.waitingGameDataAcknowledgement);


        if (GameController.Instance.dataController.gameDataLoadStatus == DataController.LoadingStatus.Success &&
            GameController.Instance.dataController.waitingGameDataAcknowledgement == false)
        {
            Debug.Log("All checks validate true. Loading MainMenu Scene");
            //fade in the scene transition panel
            StartCoroutine(FadeOutScene(fadeDuration));


        }
        else
        {
            Debug.Log("At least one check validates false. Awaiting Player acknowledgment or other.");
        }
    }

    IEnumerator FadeOutScene(float duration)
    {
        float currentTime = 0f;
        Color startColor = sceneTransitionImage.color;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, currentTime / duration);
            sceneTransitionImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            currentTime += Time.deltaTime;

            yield return null;
        }

        GameController.Instance.sceneController.LoadNextScene(GameController.Instance.sceneController.sceneData.scenes[1].sceneName);
        GameController.Instance.SetLoadingSceneControllerToNullInGameController();

    }


    private void InitiateGameLoadSequence()
    {
        GameController.Instance.dataController.LoadGameData();

        GameController.Instance.dataController.UpdatePlayerDataWithSavedGameData();

        //Player controller is a singleton, once data is tranfered, set inactive until the player gameobject is needed again in a level.
        PlayerController.Instance.gameObject.SetActive(false);
    }

    public void OnOKButtonClick()
    {
        notificationCanvasGroup.gameObject.SetActive(false);
        GameController.Instance.dataController.waitingGameDataAcknowledgement = false;
    }

    public void OnQuitButtonClick()
    {
        //#if UNITY_EDITOR
        //        EditorApplication.isPlaying = false;
        //#else
        Application.Quit();
        //#endif
    }

    public void ShowNotification(string title, string message)
    {

        notificationCanvasGroup.gameObject.SetActive(true);
        messageText.text = message;
        titleText.text = title;
    }

}

//#endif