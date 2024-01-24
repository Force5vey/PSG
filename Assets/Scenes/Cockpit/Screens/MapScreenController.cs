using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapScreenController : MonoBehaviour
{
   public CockpitController.CockpitScreenName assignedScreen;
   [SerializeField] private CockpitScreenManager screenManager;


   private void OnEnable()
   {
      if (screenManager != null)
      {
         // Find the screen GameObject that matches the assigned screen
         GameObject assignedScreenGameObject = FindAssignedScreenGameObject();
         if (assignedScreenGameObject != null)
         {
            var screenInfo = assignedScreenGameObject.GetComponent<CockpitScreenInfo>();
            if (screenInfo != null)
            {
               screenInfo.OnScreenSelected += OnScreenSelected;
            }
         }
      }
   }

   private void OnDisable()
   {
      if (screenManager != null)
      {
         GameObject assignedScreenGameObject = FindAssignedScreenGameObject();
         if (assignedScreenGameObject != null)
         {
            var screenInfo = assignedScreenGameObject.GetComponent<CockpitScreenInfo>();
            if (screenInfo != null)
            {
               screenInfo.OnScreenSelected -= OnScreenSelected;
            }
         }
      }
   }

   private GameObject FindAssignedScreenGameObject()
   {
      foreach (var row in screenManager.screenRows)
      {
         foreach (var screen in row)
         {
            var screenInfo = screen.GetComponent<CockpitScreenInfo>();
            if (screenInfo != null && screenInfo.screenName == assignedScreen)
            {
               return screen; // Return the GameObject that matches the assigned screen
            }
         }
      }
      return null; // Return null if no matching screen is found
   }

   private void OnScreenSelected()
   {
      // Logic when the assigned screen is selected
      DisplayMap();
   }

   private void DisplayMap()
   {
      // Implement the logic to display the map
      Debug.Log("Map Screen Controller: Displaying map on " + assignedScreen);
   }
}
