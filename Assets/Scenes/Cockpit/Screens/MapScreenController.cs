using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
public class MapScreenController : MonoBehaviour, ICockpitScreenController
{
   public CockpitController.CockpitScreenName assignedScreen;
   [SerializeField] private CockpitController cockpitController;
   [SerializeField] private CockpitScreenManager cockpitScreenManager;

   [SerializeField] private GameObject mapPanel;

   private void Awake()
   {
      
   }
   private void OnEnable()
   {
      if(cockpitController != null)
      {
         cockpitController.OnMapSelected += InitializeScreen;
      }
   }

   private void OnDisable()
   {
      if (cockpitController != null)
      {
         cockpitController.OnMapSelected -= InitializeScreen;
      }
   }

   public void InitializeScreen()
   {
      DisplayScreen();
   }

   public void EnableScreenInteractions()
   {
      throw new System.NotImplementedException();
   }

   public void DisplayScreen()
   {
      Debug.Log("Map Screen Controller: Displaying map on " + assignedScreen);
      mapPanel.SetActive(true);

      //TODO: Only for testing to go to level while designing cockpit.
      GameController.Instance.sceneController.LoadLevelByIndex(GameController.Instance.sceneController.sceneData.scenes[4].sceneIndex);
   }

   public void CloseScreen()
   {
      throw new System.NotImplementedException();
   }

   public void RefreshScreenData()
   {
      throw new System.NotImplementedException();
   }

   public void SaveScreenChanges()
   {
      throw new System.NotImplementedException();
   }

   public void ReceiveExternalInput()
   {
      throw new System.NotImplementedException();
   }

   public void HandleAudioEffects()
   {
      throw new System.NotImplementedException();
   }


}
