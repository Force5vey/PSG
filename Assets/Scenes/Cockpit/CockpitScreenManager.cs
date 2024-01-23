using UnityEngine;
using System.Collections.Generic;
using static SceneData;

public class CockpitScreenManager : MonoBehaviour
{
   public List<CockpitScreenInfo> screens;
   public GameObject labelPrefab; // Prefab for the label

   private void Start()
   {
      foreach (var screen in screens)
      {
         GameObject label = Instantiate(labelPrefab, screen.screenTransform.position, Quaternion.identity, screen.screenTransform);
         // Adjust the position as needed, relative to the screen's transform
         label.transform.localPosition = new Vector3(0, 1, 0); // Example offset
         label.GetComponentInChildren<UnityEngine.UI.Text>().text = screen.screenName;
      }
   }

   public void FocusOnScreen(int index)
   {
      // Logic to focus on a specific screen
   }
}
