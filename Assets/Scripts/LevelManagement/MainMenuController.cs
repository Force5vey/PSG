using System;
using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//#if UNITY_EDITOR

public class MainMenuController :MonoBehaviour
{
   public LevelData levelData;

   [Header("Main Menu Objects")]
   [SerializeField] private GameObject mainMenuPanel;
   [SerializeField] private CanvasGroup mainMenuGroup;
   [SerializeField] private TextMeshProUGUI titleText;

   private InputHandler inputHandler;

   [Header("Close Game Panel and Buttons")]
   [SerializeField] private GameObject verifyCloseMessagePanel;
   [SerializeField] private GameObject closeGameButton;
   [SerializeField] private GameObject yesCloseGameButton;
   [SerializeField] private GameObject noCloseGameButton;

   [Header("Main Menu Buttons")]
   [SerializeField] private GameObject launchButton;
   [SerializeField] private GameObject selectPilotButton;
   [SerializeField] private GameObject optionsButton;

   [Header("Pilot Selection Panel")]
   [SerializeField] private GameObject pilotSelectionPanel;
   [SerializeField] private CanvasGroup pilotSelectionGroup;
   [SerializeField] private GameObject[] pilotButtons;
   [SerializeField] private GameObject pilot1SelectedImage;
   [SerializeField] private GameObject pilot2SelectedImage;

   private void Awake()
   {
      pilotSelectionPanel.SetActive(false);

      inputHandler = GameController.Instance.inputHandler;

   }

   void Start()
   {
      verifyCloseMessagePanel.SetActive(false);
      titleText.color = GameController.Instance.uiController.textColorEnabled;

   }


   void Update()
   {

   }

   private void OnEnable()
   {
      if ( inputHandler != null )
      {
         inputHandler.OnEastButtonInput += OnHandleEastButtonInput;
      }
   }

   private void OnDisable()
   {
      if ( inputHandler != null )
      {
         inputHandler.OnEastButtonInput -= OnHandleEastButtonInput;
      }
   }

   private void OnHandleEastButtonInput( bool isPressed )
   {
      if ( isPressed )
      {
         CancelButton_Click();
      }
   }

   public void ExecuteButtonAction( string buttonActionName )
   {
      switch ( buttonActionName )
      {
         case "QuitGame":
         OnCloseButton_Click();
         break;
         case "QuitYes":
         OnQuitYesButton_Click();
         break;
         case "QuitNo":
         OnQuitNoButton_Click();
         break;
         case "SelectPilot":
         OnSelectPilot_Click();
         break;
         case "Options":
         OnOptions_Click();
         break;
         case "LaunchGame":
         OnLaunchGame_Click();
         break;
         case "SelectPilot0":
         OnPilotSelection_Click(1);
         break;
         case "SelectPilot1":
         OnPilotSelection_Click(2);
         break;
         case "SelectPilotBack":
         OnPilotSelection_Click(0);
         break;
      }
   }

   IEnumerator FadePopUp( CanvasGroup canvasGroup, float duration, float startOpacity, float endOpacity, GameObject panelToToggle = null, bool setActiveState = false )
   {
      float currentTime = 0f;

      canvasGroup.alpha = startOpacity;

      if ( panelToToggle != null && setActiveState == true )
      {
         panelToToggle.SetActive(setActiveState);
      }

      while ( currentTime < duration )
      {
         float alpha = Mathf.Lerp(startOpacity, endOpacity, currentTime / duration);
         canvasGroup.alpha = alpha;
         currentTime += Time.deltaTime;
         yield return null;
      }

      canvasGroup.alpha = endOpacity;

      if ( panelToToggle != null && setActiveState == false )
      {
         panelToToggle.SetActive(setActiveState);
      }
   }

   private void OnOptions_Click()
   {
      Debug.Log($"OnOptions_Click performed");
   }



   private void ToggleCanvasGroup( CanvasGroup canvasGroup, bool isInteractive, float opacity )
   {
      canvasGroup.interactable = isInteractive;
      canvasGroup.blocksRaycasts = isInteractive;
      canvasGroup.alpha = opacity;
   }

   private void OnLaunchGame_Click()
   {
      //Launch into the cockpit...the central game navigation environment.
      GameController.Instance.sceneController.LoadCoreScene(SceneController.CoreScene.Cockpit);

   }

   private void OnCloseButton_Click()
   {

      EventSystem.current.SetSelectedGameObject(null);

      verifyCloseMessagePanel.SetActive(true);
      //EventSystem.current.SetSelectedGameObject(launchButton);



      mainMenuPanel.SetActive(false);

      EventSystem.current.SetSelectedGameObject(noCloseGameButton);
   }

   private void OnQuitYesButton_Click()
   {
      //TODO: This needs to be centralized to avoid duplication for the in game pause menu (I might make it so you go to the main menu then quit).
      //TODO: Make sure any save functions are called first.

      //#if UNITY_EDITOR
      //        EditorApplication.isPlaying = false;
      //#else
      Application.Quit();
      //#endif

   }

   private void OnQuitNoButton_Click()
   {
      mainMenuPanel.SetActive(true);
      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(launchButton);
      //standardButtonController.OnDeselect();

      verifyCloseMessagePanel.SetActive(false);
   }


   private void OnSelectPilot_Click()
   {
      if ( PlayerController.Instance.pilotController.selectedPilotIndex == 1 )
      {
         pilot1SelectedImage.SetActive(true);
         pilot2SelectedImage.SetActive(false);
      }
      else if ( PlayerController.Instance.pilotController.selectedPilotIndex == 2 )
      {
         pilot2SelectedImage.SetActive(true);
         pilot1SelectedImage.SetActive(false);
      }


      StartCoroutine(FadePopUp(pilotSelectionGroup, .25f, 0f, 1f, pilotSelectionPanel, true));

      EventSystem.current.SetSelectedGameObject(null);
      ToggleCanvasGroup(mainMenuGroup, false, .5f);
      EventSystem.current.SetSelectedGameObject(pilotButtons[PlayerController.Instance.pilotController.selectedPilotIndex]);

   }


   private void OnPilotSelection_Click( int selectedPilotIndex )
   {
      EventSystem.current.SetSelectedGameObject(null);
      StartCoroutine(FadePopUp(pilotSelectionGroup, .25f, 1f, 0f, pilotSelectionPanel, false));
      ToggleCanvasGroup(mainMenuGroup, true, 1);
      EventSystem.current.SetSelectedGameObject(launchButton);

      if ( selectedPilotIndex != 0 )
      {
         PlayerController.Instance.pilotController.selectedPilotIndex = selectedPilotIndex;
      }

   }

   public void CancelButton_Click()
   {
      // Return to main menu
      //ensure pilot selection is closed
      OnPilotSelection_Click(0);
      //close options.

      //close quit menu
      OnQuitNoButton_Click();

   }

}
//#endif
