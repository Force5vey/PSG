// Ignore Spelling: Indices

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class CockpitScreenManager : MonoBehaviour
{
   #region //Class Level Variables

   [Header("Screen Rows")]
   public List<GameObject> overheadRow;

   public List<GameObject> aPillarRow;
   public List<GameObject> consoleAuxRow;
   public List<GameObject> consoleRow;
   public List<GameObject> auxRow;
   private Dictionary<CockpitController.CockpitScreenName, GameObject> screenLookup;

   [Header("Screen Indexes")]
   public List<List<GameObject>> screenRows; // Nested list for organizing screens by rows

   private int currentRowIndex; // Current row index
   private int currentScreenIndex; // Current screen index within the row

   //Use in the event their was an issue loading from Pilot Script.
   private int defaultScreenIndex = 1;

   private int defaultRowINdex = 3;

   [Header("Screen Navigation")]
   private MainPlayerControls controls;

   private GameObject currentScreen; // Currently selected screen
   [SerializeField] private float horizontalThreshold;
   [SerializeField] private float verticalThreshold;
   [SerializeField] private float navigationCooldown;
   private float timeSinceLastNavigation = 0.0f;
   private bool canNavigate;

   [Header("Screen Effects")]
   [SerializeField] private List<GameObject> screenLights;

   [SerializeField] private float delayBetweenLights;

   #endregion //Class Level Variables

   private void Awake()
   {
      // Initialize the input system
      controls = new MainPlayerControls();
      controls.PlayerControl.MovePlayer.performed += ctx => NavigateScreens(ctx.ReadValue<Vector2>());
      controls.PlayerControl.ButtonSouth.performed += _ => SelectCurrentScreen();

      screenRows = new List<List<GameObject>>
      {
         overheadRow,
         aPillarRow,
         consoleAuxRow,
         consoleRow,
         auxRow
      };

      //Set the Screen Selection Objects Mesh Renderer to disabled;
      //TODO: Update this when settled on screen selection indicator.
      foreach (List<GameObject> list in screenRows)
      {
         foreach (GameObject obj in list)
         {
            if (obj != null)
            {
               obj.gameObject.GetComponent<MeshRenderer>().enabled = false;

               obj.gameObject.GetComponent<CockpitScreenInfo>().IsActive = true;
            }
         }
      }

      AssignScreenIndices();

      // Initialize the starting screen (use saved data or default)
      SetInitialScreen();

      InitializeScreenLookup();
   }

   private void OnEnable()
   {
      controls.Enable();
   }

   private void OnDisable()
   {
      controls.Disable();
   }

   private void Start()
   {
      StartCoroutine(EnableLightsWithDelay());
   }

   private void Update()
   {
      timeSinceLastNavigation += Time.deltaTime;
      if (timeSinceLastNavigation >= navigationCooldown)
      {
         canNavigate = true;
      }
   }

   private void InitializeScreenLookup()
   {
      screenLookup = new Dictionary<CockpitController.CockpitScreenName, GameObject>();
      foreach (var row in screenRows)
      {
         foreach (var screen in row)
         {
            var screenInfo = screen.GetComponent<CockpitScreenInfo>();
            if (screenInfo != null)
            {
               screenLookup[screenInfo.screenName] = screen;
            }
         }
      }
   }

   private void NavigateScreens(Vector2 direction)
   {
      if (!canNavigate) { return; }

      // Handle navigation between screens based on input direction
      if (Mathf.Abs(direction.x) > horizontalThreshold)
      {
         // Horizontal navigation
         int newScreenIndex = currentScreenIndex + (int)Mathf.Sign(direction.x);
         newScreenIndex = Mathf.Clamp(newScreenIndex, 0, screenRows[currentRowIndex].Count - 1);
         UpdateCurrentScreen(newScreenIndex);

         timeSinceLastNavigation = 0f;
         canNavigate = false;
      }
      else if (Mathf.Abs(direction.y) > verticalThreshold)
      {
         // Vertical navigation (switching rows)
         int newRow = currentRowIndex - (int)Mathf.Sign(direction.y);
         newRow = Mathf.Clamp(newRow, 0, screenRows.Count - 1);
         if (newRow != currentRowIndex)
         {
            currentRowIndex = newRow;
            if (currentScreenIndex >= screenRows[currentRowIndex].Count - 1)
            {
               currentScreenIndex = screenRows[currentRowIndex].Count - 1;
            }

            UpdateCurrentScreen(currentScreenIndex);
         }

         timeSinceLastNavigation = 0f;
         canNavigate = false;
      }
   }






   private void UpdateCurrentScreen(int newScreenIndex)
   {
      // Update the currently selected screen based on the new index
      if (currentScreen != null)
      {
         // De-select the current screen
         DeselectScreen(currentScreen);
      }

      currentScreenIndex = newScreenIndex;
      currentScreen = screenRows[currentRowIndex][currentScreenIndex];

      // Select the new screen
      SelectScreen(currentScreen);
   }

   private void SelectScreen(GameObject screen)
   {
      //TODO: Temp indicator, add animation / light / glow / border / etc
      CockpitScreenInfo screenInfo = screen.GetComponent<CockpitScreenInfo>();
        if (screenInfo.spotLight !=null)
        {
           screenInfo.spotLight.SetActive(true); 
        }
        else
      {
         screen.GetComponent<MeshRenderer>().enabled = true;
      }
        
     
   }

   private void DeselectScreen(GameObject screen)
   {
      CockpitScreenInfo screenInfo = screen.GetComponent<CockpitScreenInfo>();
      if(screenInfo.spotLight != null)
      {
         screenInfo.spotLight.SetActive(false);
      }
      //TODO: temp indicator, deactivate whatever the new update method is.
      screen.GetComponent<MeshRenderer>().enabled = false;
   }

   private void SelectCurrentScreen()
   {
      // Logic to handle the action when a screen is selected
      Debug.Log("Selected Screen: " + currentScreen.name + " >> In ScreenManager/SelectCurrentScreen");

      var screenInfo = currentScreen.GetComponent<CockpitScreenInfo>();
      if (screenInfo != null)
      {
         screenInfo.TriggerScreenSelected();
      }
   }

   private void SetInitialScreen()
   {
      if (PlayerController.Instance != null)
      {
         currentScreenIndex = PlayerController.Instance.pilotController.lastSelectedScreenIndex;
         currentRowIndex = PlayerController.Instance.pilotController.lastSelectedRowIndex;
         Debug.Log($"{currentRowIndex} <> {currentScreenIndex}");
      }
      else
      {
         currentScreenIndex = defaultScreenIndex;
         currentRowIndex = defaultRowINdex;
      }

      currentScreen = screenRows[currentRowIndex][currentScreenIndex];
      SelectScreen(currentScreen);
   }

   public void AssignScreenIndices()
   {
      for (int rowIndex = 0; rowIndex < screenRows.Count; rowIndex++)
      {
         for (int screenIndex = 0; screenIndex < screenRows[rowIndex].Count; screenIndex++)
         {
            CockpitScreenInfo screenInfo = screenRows[rowIndex][screenIndex].GetComponent<CockpitScreenInfo>();
            if (screenInfo != null)
            {
               screenInfo.SetIndices(screenIndex, rowIndex);
            }
         }
      }
   }

   public void SelectScreenByName(CockpitController.CockpitScreenName screenName)
   {
      if (screenLookup.TryGetValue(screenName, out GameObject screen))
      {
         var screenInfo = screen.GetComponent<CockpitScreenInfo>();
         if (screenInfo != null)
         {
            currentRowIndex = screenInfo.rowIndex;
            currentScreenIndex = screenInfo.screenIndex;
            SelectCurrentScreen();
         }
      }
   }

   private IEnumerator EnableLightsWithDelay()
   {
      foreach (var light in screenLights)
      {
         if (light != null)
         {
            light.SetActive(true);
            yield return new WaitForSeconds(delayBetweenLights);
         }
      }
   }

   public bool TryGetScreen(CockpitController.CockpitScreenName screenName, out GameObject screenObj)
   {

      screenObj = null;

      // Loop through each row and each screen in the nested list
      foreach (var row in screenRows)
      {
         foreach (var screen in row)
         {
            var screenInfo = screen.GetComponent<CockpitScreenInfo>();
            if (screenInfo != null && screenInfo.screenName == screenName)
            {
               screenObj = screen;
               return true; // Screen found
            }
         }
      }

      return false; // Screen not found
   }
}