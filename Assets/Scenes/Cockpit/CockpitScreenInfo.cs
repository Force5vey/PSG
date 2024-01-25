// Ignore Spelling: Indices

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitScreenInfo : MonoBehaviour
{
   [Header("Screen Info")]
   public CockpitController.CockpitScreenName screenName;

   [Header("Selection Indexes")]
   public int screenIndex;
   public int rowIndex;
   public bool IsActive;

   [Header("Location References")]
   public Transform screenTransform;

   //Delegates and Events
   public delegate void ScreenSelectedAction(CockpitController.CockpitScreenName scrnName);
   public event ScreenSelectedAction OnScreenSelected;

   [Header("Selection Effects")]
   [SerializeField] public GameObject spotLight;
   [SerializeField] public GameObject cockpitLight;

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
