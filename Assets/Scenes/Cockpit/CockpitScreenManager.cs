// Ignore Spelling: Indices

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CockpitScreenManager :MonoBehaviour
{
   #region //Class Level Variables

   [Header("Screen Rows - Masters")]
   [SerializeField] private List<GameObject> overheadRowMaster;
   [SerializeField] private List<GameObject> aPillarRowMaster;
   [SerializeField] private List<GameObject> consoleAuxRowMaster;
   [SerializeField] private List<GameObject> consoleRowMaster;
   [SerializeField] private List<GameObject> auxRowMaster;

   private Dictionary<CockpitController.CockpitScreenName, GameObject> screenLookup;

   [Header("Screen Rows - Active")]
   [HideInInspector] public List<GameObject> overheadRow;
   [HideInInspector] public List<GameObject> aPillarRow;
   [HideInInspector] public List<GameObject> consoleAuxRow;
   [HideInInspector] public List<GameObject> consoleRow;
   [HideInInspector] public List<GameObject> auxRow;

   [Header("Screen Indexes")]
   public List<List<GameObject>> screenRows; // Nested list for organizing screens by rows
   private int currentRowIndex; // Current row index
   private int currentScreenIndex; // Current screen index within the row

   //Use in the event their was an issue loading from Pilot Script.
   [SerializeField] private GameObject defaultScreen;

   [Header("Screen Navigation")]
   private MainPlayerControls controls;

   private GameObject currentScreen; // Currently selected screen
   [SerializeField] private float horizontalThreshold;
   [SerializeField] private float verticalThreshold;
   [SerializeField] private float navigationCooldown;
   private float timeSinceLastNavigation = 0.0f;
   private bool canNavigate;

   [Header("Screen Effects")]
   [SerializeField] private List<GameObject> cockpitLights;
   [SerializeField] private float delayBetweenLights;

   #endregion //Class Level Variables

   private void Awake()
   {
      //Initialize ScreenRows with active screen Objects
      InitializeScreenRows();

      // Initialize the input system
      controls = new MainPlayerControls();


      //Make sure each screen object has an index assigned according to its position in active screen list.
      AssignScreenIndices();
      InitializeScreenLookup();

      // Initialize the starting screen (use saved data or default)
      SetInitialScreen();

      Debug.Log("ScreenManager > end of awake");
   }

   private void OnEnable()
   {
      controls.Enable();
      controls.PlayerControl.LeftStick.performed += ctx => NavigateScreens(ctx.ReadValue<Vector2>());
      controls.PlayerControl.ButtonSouth.performed += _ => SelectCurrentScreen();
   }

   private void OnDisable()
   {
      controls.PlayerControl.LeftStick.performed -= ctx => NavigateScreens(ctx.ReadValue<Vector2>());
      controls.PlayerControl.ButtonSouth.performed -= _ => SelectCurrentScreen();
      controls.Disable();
   }

   private void Start()
   {
      StartCoroutine(EnableLightsWithDelay());
   }

   private void Update()
   {
      timeSinceLastNavigation += Time.deltaTime;
      if ( timeSinceLastNavigation >= navigationCooldown )
      {
         canNavigate = true;
      }
   }

   private void InitializeScreenRows()
   {
      screenRows = new List<List<GameObject>>();

      AddActiveRow(overheadRowMaster);
      AddActiveRow(aPillarRowMaster);
      AddActiveRow(consoleAuxRowMaster);
      AddActiveRow(consoleRowMaster);
      AddActiveRow(auxRowMaster);

   }

   private void AddActiveRow( List<GameObject> masterList )
   {
      List<GameObject> activeScreens = FilterActiveScreens(masterList);
      if ( activeScreens.Count > 0 )
      {
         screenRows.Add(activeScreens);
      }
   }

   private List<GameObject> FilterActiveScreens( List<GameObject> masterList )
   {
      List<GameObject> activeScreens = new List<GameObject>();
      foreach ( GameObject obj in masterList )
      {
         CockpitScreenInfo screenInfo = obj.GetComponent<CockpitScreenInfo>();

         if ( screenInfo.IsActive || screenInfo.screenName == CockpitController.CockpitScreenName.Console_Center )
         {
            activeScreens.Add(obj);
         }
      }
      return activeScreens;
   }

   private void InitializeScreenLookup()
   {
      screenLookup = new Dictionary<CockpitController.CockpitScreenName, GameObject>();
      foreach ( var row in screenRows )
      {
         foreach ( var screen in row )
         {
            var screenInfo = screen.GetComponent<CockpitScreenInfo>();
            if ( screenInfo != null )
            {
               screenLookup[screenInfo.screenName] = screen;
            }
         }
      }
   }

   private void NavigateScreens( Vector2 direction )
   {
      if ( !canNavigate )
      { return; }

      // Handle navigation between screens based on input direction
      if ( Mathf.Abs(direction.x) > horizontalThreshold )
      {
         // Horizontal navigation
         int newScreenIndex = currentScreenIndex + (int)Mathf.Sign(direction.x);
         newScreenIndex = Mathf.Clamp(newScreenIndex, 0, screenRows[currentRowIndex].Count - 1);
         UpdateCurrentScreen(newScreenIndex);

         timeSinceLastNavigation = 0f;
         canNavigate = false;
      }
      else if ( Mathf.Abs(direction.y) > verticalThreshold )
      {
         // Vertical navigation (switching rows)
         int newRow = currentRowIndex - (int)Mathf.Sign(direction.y);
         newRow = Mathf.Clamp(newRow, 0, screenRows.Count - 1);
         if ( newRow != currentRowIndex )
         {
            currentRowIndex = newRow;
            if ( currentScreenIndex >= screenRows[currentRowIndex].Count - 1 )
            {
               currentScreenIndex = screenRows[currentRowIndex].Count - 1;
            }

            UpdateCurrentScreen(currentScreenIndex);
         }

         timeSinceLastNavigation = 0f;
         canNavigate = false;
      }
   }

   private void UpdateCurrentScreen( int newScreenIndex )
   {
      // Update the currently selected screen based on the new index
      if ( currentScreen != null )
      {
         // De-select the current screen
         DeselectScreen(currentScreen);
      }

      currentScreenIndex = newScreenIndex;
      currentScreen = screenRows[currentRowIndex][currentScreenIndex];

      // Select the new screen
      SelectScreen(currentScreen);
   }

   private void SelectScreen( GameObject screen )
   {
      //TODO: Temp indicator, add animation / light / glow / border / etc
      CockpitScreenInfo screenInfo = screen.GetComponent<CockpitScreenInfo>();
      if ( screenInfo.spotLight != null )
      {
         screenInfo.spotLight.SetActive(true);
      }
   }

   private void DeselectScreen( GameObject screen )
   {
      CockpitScreenInfo screenInfo = screen.GetComponent<CockpitScreenInfo>();
      if ( screenInfo.spotLight != null )
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
      if ( screenInfo != null )
      {
         screenInfo.TriggerScreenSelected();
      }
   }

   private void SetInitialScreen()
   {
      if ( PlayerController.Instance != null )
      {
         (int savedRowIndex, int savedScreenIndex) = SelectScreenByName(PlayerController.Instance.pilotController.lastSelectedCockpitScreen);
         if ( savedRowIndex != -1 && savedScreenIndex != -1 )
         {
            currentRowIndex = savedRowIndex;
            currentScreenIndex = savedScreenIndex;
         }
         else
         {
            // Fall back to default if screen info is not available
            SetDefaultScreen();
         }
      }
      else
      {
         // Fall back to default if player controller instance is not available
         SetDefaultScreen();
      }

      currentScreen = screenRows[currentRowIndex][currentScreenIndex];
      SelectScreen(currentScreen);
   }

   private void SetDefaultScreen()
   {

      CockpitScreenInfo screenInfo = defaultScreen.GetComponent<CockpitScreenInfo>();
      currentScreenIndex = screenInfo.screenIndex;
      currentRowIndex = screenInfo.rowIndex;
   }

   public void AssignScreenIndices()
   {
      for ( int rowIndex = 0; rowIndex < screenRows.Count; rowIndex++ )
      {
         for ( int screenIndex = 0; screenIndex < screenRows[rowIndex].Count; screenIndex++ )
         {
            CockpitScreenInfo screenInfo = screenRows[rowIndex][screenIndex].GetComponent<CockpitScreenInfo>();
            if ( screenInfo != null )
            {
               screenInfo.SetIndices(screenIndex, rowIndex);
            }
         }
      }
   }

   public (int rowIndex, int screenIndex) SelectScreenByName( CockpitController.CockpitScreenName screenName )
   {
      if ( screenLookup.TryGetValue(screenName, out GameObject screen) )
      {

         var screenInfo = screen.GetComponent<CockpitScreenInfo>();
         if ( screenInfo != null )
         {
            return (screenInfo.rowIndex, screenInfo.screenIndex);
         }

      }

      // Return default values if not found
      return (-1, -1);
   }

   private IEnumerator EnableLightsWithDelay()
   {
      foreach ( var light in cockpitLights )
      {
         if ( light != null )
         {
            light.SetActive(true);
            yield return new WaitForSeconds(delayBetweenLights);
         }
      }
   }

   public bool TryGetScreen( CockpitController.CockpitScreenName screenName, out GameObject screenObj )
   {
      screenObj = null;

      // Loop through each row and each screen in the nested list
      foreach ( var row in screenRows )
      {
         foreach ( var screen in row )
         {
            var screenInfo = screen.GetComponent<CockpitScreenInfo>();
            if ( screenInfo != null && screenInfo.screenName == screenName )
            {
               screenObj = screen;
               return true; // Screen found
            }
         }
      }

      return false; // Screen not found
   }

   public void CloseScreenManager()
   {
      Debug.Log($"ScreenManager > CloseScreenManager");
      PlayerController.Instance.pilotController.lastSelectedCockpitScreen = screenRows[currentRowIndex][currentScreenIndex].gameObject.GetComponent<CockpitScreenInfo>().screenName;
   }
}