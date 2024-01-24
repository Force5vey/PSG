using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CockpitController : MonoBehaviour
{
   private Dictionary<CockpitScreenName, CockpitFunctionName> screenFunctionMapping;

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
      CurrentLevelStatus,
      Settings,
      Unassigned
   }

   private void Awake()
   {
      //TODO: Update this to the master dictionary from GameData when that is implemented.
      screenFunctionMapping = new Dictionary<CockpitScreenName, CockpitFunctionName>
      {
         { CockpitScreenName.Console_Center, CockpitFunctionName.Map },
         { CockpitScreenName.Console_Right, CockpitFunctionName.Attachments },
         { CockpitScreenName.Left_Aux_Bottom, CockpitFunctionName.Radar },
         { CockpitScreenName.Console_Left, CockpitFunctionName.TDriveStatus },
         { CockpitScreenName.APillar_Right, CockpitFunctionName.CurrentLevelStatus },
         { CockpitScreenName.Right_Aux_Right, CockpitFunctionName.Settings },
         { CockpitScreenName.Left_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Left_Aux_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Right_Aux_Bottom , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Right_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Console_Aux_Center, CockpitFunctionName.Unassigned},
         { CockpitScreenName.Console_Aux_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Console_Aux_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.APillar_Left , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Overhead_Right , CockpitFunctionName.Unassigned},
         { CockpitScreenName.Overhead_Left , CockpitFunctionName.Unassigned}
         
      };
   }

   private void Start()
   {
   }
}