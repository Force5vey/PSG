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
   public bool IsActive;

   //Delegates and Events
   public delegate void ScreenSelectedAction(CockpitController.CockpitScreenName screenName);
   public event ScreenSelectedAction OnScreenSelected;

   [Header("Selection Effects")]
   [SerializeField] public GameObject spotLight;

   public void SetIndices(int screenIdx, int rowIdx)
   {
      screenIndex = screenIdx;
      rowIndex = rowIdx;
   }

   public void TriggerScreenSelected()
   {
      OnScreenSelected?.Invoke(screenName);
   }
}
