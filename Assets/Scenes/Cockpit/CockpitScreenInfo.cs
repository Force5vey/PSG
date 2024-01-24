// Ignore Spelling: Indices

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitScreenInfo : MonoBehaviour
{
   [Header("Screen Info")]
   public CockpitController.CockpitScreenName screenName;
   public int screenIndex;
   public int rowIndex;
   public Transform screenTransform;

   //Delegates and Events
   public delegate void ScreenSelectedAction();
   public event ScreenSelectedAction OnScreenSelected;

   public void SetIndices(int screenIdx, int rowIdx)
   {
      screenIndex = screenIdx;
      rowIndex = rowIdx;
   }

   public void TriggerScreenSelected()
   {
      OnScreenSelected?.Invoke();
   }
}
