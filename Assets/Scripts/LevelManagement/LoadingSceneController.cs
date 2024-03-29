using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
   [Header("UI Elements")]
   [SerializeField] private CanvasGroup notificationCanvasGroup;
   public TextMeshProUGUI headingText;
   public TextMeshProUGUI messageText;
   public TextMeshProUGUI titleText;
   [SerializeField] private Button leftButton;
   [SerializeField] private Button rightButton;
   [SerializeField] private TextMeshProUGUI leftButtonText;
   [SerializeField] private TextMeshProUGUI rightButtonText;

   private void Awake()
   {
      //ensure Notification group and text is hidden and empty in case something was left on in the unity editor.
      notificationCanvasGroup.gameObject.SetActive(false);
      messageText.text = string.Empty;
   }

   private void Start()
   {
      if (GameController.Instance != null)
      {
         GameController.Instance.dataController.onLoadingError.AddListener(HandleLoadingError);
         GameController.Instance.dataController.onGameDataLoadedSuccessfully.AddListener(HandleSuccessfulLoad);
         GameController.Instance.dataController.onGameDataSaveError.AddListener(HandleGameDataSaveError);
      }
      else { Debug.LogError($"{nameof(LoadingSceneController)} > {nameof(Start)} \n GameController instance not found."); }

      if (PlayerController.Instance != null)
      {
         headingText.color = GameController.Instance.uiController.textColorSelected;
         messageText.color = GameController.Instance.uiController.textColorSelected;
         titleText.color = GameController.Instance.uiController.textColorSelected;
      }
      else { Debug.LogError($"{nameof(LoadingSceneController)} > {nameof(Start)} \n PlayerController instance not found."); }

      InitiateGameLoadSequence();
   }

   private void HandleLoadingError()
   {
      ShowNotification("Notice", "No save game file found. Loading new game.", HandleSuccessfulLoad, QuitGame, "OK", "Quite");
   }
   private void HandleGameDataSaveError()
   {
      ShowNotification("Error", "Unable to save file to disk. The cause of this problem is unknown. You can continue to see if it saves later or quit and check the installation.",
         HandleSuccessfulLoad, QuitGame, "Continue", "Quit");
   }

   private void HandleSuccessfulLoad()
   {
      notificationCanvasGroup.gameObject.SetActive(false);

      GameController.Instance.sceneController.LoadCoreScene(SceneController.CoreScene.MainMenu);
   }

   private void OnDestroy()
   {
      GameController.Instance.dataController.onLoadingError.RemoveAllListeners();
   }

   private void InitiateGameLoadSequence()
   {
      GameController.Instance.dataController.LoadGameData();

      //Player controller is a singleton, once data is transfered, set inactive until the player game-object is needed again in a level.
      PlayerController.Instance.gameObject.SetActive(false);
   }

   public void OnOKButtonClick()
   {
      notificationCanvasGroup.gameObject.SetActive(false);

      HandleSuccessfulLoad();
   }

   public void QuitGame()
   {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
   }

   public void ShowNotification(string title, string message, UnityAction leftAction, UnityAction rightAction, string leftBtnText = "OK", string rightBtnText = "Quit")
   {
      titleText.text = title;
      messageText.text = message;
      leftButtonText.text = leftBtnText;
      rightButtonText.text = rightBtnText;

      leftButton.onClick.RemoveAllListeners();
      rightButton.onClick.RemoveAllListeners();
    

      if (leftAction != null) { leftButton.onClick.AddListener(leftAction); }
      if (rightAction != null) { rightButton.onClick.AddListener(rightAction); }

      notificationCanvasGroup.gameObject.SetActive(true);
      if (EventSystem.current != null)
      {
         EventSystem.current.SetSelectedGameObject(null);
         EventSystem.current.SetSelectedGameObject(leftButton.gameObject);
      }
   }

}