using System;
using System.Collections.Generic;

using UnityEngine;

public class CockpitController :MonoBehaviour
{
   private Dictionary<CockpitScreenName, CockpitFunctionName> screenFunctionMapping;

   [SerializeField] private CockpitScreenManager screenManager;

   private MainPlayerControls mainPlayerControls;

   //Individual Screen Open Events
   public event Action OnMapSelected;
   public event Action OnRadarSelected;
   public event Action OnAttachmentsSelected;
   public event Action OnTDriveStatusSelected;
   public event Action OnLevelStatusSelected;
   public event Action OnSettingsSelected;
   public event Action OnUnassignedSelected;

   public enum CockpitScreenName
   {
      Left_Aux_Bottom,
      Left_Aux_Left,
      Left_Aux_Right,
      Console_Left,
      Console_Right,
      Console_Center,
      Right_Aux_Bottom,
      Right_Aux_Left,
      Right_Aux_Right,
      Console_Aux_Center,
      Console_Aux_Right,
      Console_Aux_Left,
      APillar_Left,
      APillar_Right,
      Overhead_Right,
      Overhead_Left
   }

   public enum CockpitFunctionName
   {
      Map,
      Radar,
      Attachments,
      TDriveStatus,
      LevelStatus,
      Settings,
      Unassigned
   }

   private void Awake()
   {
      //TODO: Update this to the master dictionary from GameData when that is implemented.
      screenFunctionMapping = new Dictionary<CockpitScreenName, CockpitFunctionName>
      {
         { CockpitScreenName.Overhead_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Overhead_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.APillar_Left , CockpitFunctionName.TDriveStatus},
         { CockpitScreenName.APillar_Right, CockpitFunctionName.LevelStatus },
         { CockpitScreenName.Console_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Console_Aux_Center, CockpitFunctionName.Settings},
         { CockpitScreenName.Console_Aux_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Console_Left, CockpitFunctionName.Radar },
         { CockpitScreenName.Console_Center, CockpitFunctionName.Map },
         { CockpitScreenName.Console_Right, CockpitFunctionName.Attachments },
         { CockpitScreenName.Left_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Left_Aux_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Left_Aux_Bottom, CockpitFunctionName.Unassigned },
         { CockpitScreenName.Right_Aux_Bottom , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Right_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Right_Aux_Right, CockpitFunctionName.Unassigned },
      };


      mainPlayerControls = new MainPlayerControls();
      Debug.Log("Cockpit Controller > Awake > Just after mainPlayerControls = new");
   }

   private void Start()
   {
      SubscribeToScreenSelectionEvents();
   }

   private void OnEnable()
   {
      mainPlayerControls.Enable();
      mainPlayerControls.PlayerControl.ButtonEast.performed += _ => HandleCloseButton();

   }
   private void OnDisable()
   {
      CloseCockpit();

      mainPlayerControls.Disable();
   }

   private void HandleCloseButton()
   {
      Debug.Log("CockpitController > HandleCloseButton");
      CloseCockpit();
   }

   private void SubscribeToScreenSelectionEvents()
   {
      // For each screen in the cockpit, subscribe to its OnScreenSelected event
      foreach ( var screenName in screenFunctionMapping.Keys )
      {
         GameObject screenObj;
         if ( screenManager.TryGetScreen(screenName, out screenObj) )
         {
            var screenInfo = screenObj.GetComponent<CockpitScreenInfo>();
            if ( screenInfo != null )
            {
               screenInfo.OnScreenSelected += ( selectedScreenName ) => HandleScreenSelection(selectedScreenName);
            }
         }
      }

      Debug.Log($"CockpitController / Subscribe to screen selection events - Complete");
   }

   private void HandleScreenSelection( CockpitScreenName screenName )
   {
      Debug.Log($"cockpit controller > HandleScreenSelection: {screenName}");

      if ( screenFunctionMapping.TryGetValue(screenName, out CockpitFunctionName functionName) )
      {
         switch ( functionName )
         {
            case CockpitFunctionName.Map:
            Debug.Log("Switch Statement > Map");
            OnMapSelected?.Invoke();
            break;

            case CockpitFunctionName.Radar:
            OnRadarSelected?.Invoke();
            break;

            case CockpitFunctionName.Attachments:
            OnAttachmentsSelected?.Invoke();
            break;

            case CockpitFunctionName.TDriveStatus:
            OnTDriveStatusSelected?.Invoke();
            break;

            case CockpitFunctionName.LevelStatus:
            OnLevelStatusSelected?.Invoke();
            break;

            case CockpitFunctionName.Settings:
            OnSettingsSelected?.Invoke();
            break;

            case CockpitFunctionName.Unassigned:
            OnUnassignedSelected?.Invoke();
            break;

            default:

            // Handle unassigned or default case
            break;
         }
      }
   }

   public void CloseCockpit()
   {
      Debug.Log($"CockpitController > CloseCockpit > Pre-Close Screen Manager");
      screenManager.CloseScreenManager();

      Debug.Log($"CockpitController > CloseCockpit > Post-Close Screen Manager");
      //Go back to level, for now just go to scene-load of level1_1
      GameController.Instance.sceneController.LoadNextScene(GameController.Instance.sceneController.sceneData.scenes[4].sceneName);
   }
}