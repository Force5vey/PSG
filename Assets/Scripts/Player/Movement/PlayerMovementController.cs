using UnityEngine;

public class PlayerMovementController :MonoBehaviour
{
   [Header("Scripts")]
   private InputHandler inputHandler;
   [SerializeField] private FlightModeController flightModeController;
   [SerializeField] private HoverModeController hoverModeController;

   [Header("Flight Mode Settings")]
   [SerializeField] private ControlMode currentControlMode;
   private enum ControlMode
   {
      FlightMode,
      HoverMode
   }

   private void Awake()
   {
      
   }

   private void OnEnable()
   {
      if ( inputHandler != null )
      {
         inputHandler.OnRightShoulderInput += HandleRightShoulderInput;
      }
   }

   private void OnDisable()
   {
      if ( inputHandler != null )
      {
         inputHandler.OnRightShoulderInput -= HandleRightShoulderInput;
      }
   }

   private void Start()
   {
      inputHandler = GameController.Instance.inputHandler;
   }

   void Update()
   {
      float lineLength = 5f;
      float duration = 0.1f; // Duration to display the line (in seconds)

      Debug.DrawRay(transform.position, transform.forward * lineLength, Color.blue, duration); // Forward - Blue
      Debug.DrawRay(transform.position, transform.right * lineLength, Color.red, duration);    // Right - Red
      Debug.DrawRay(transform.position, transform.up * lineLength, Color.green, duration);     // Up - Green
   }

   private void FixedUpdate()
   {
      if ( inputHandler == null )
      { return; }

      switch ( currentControlMode )
      {
         case ControlMode.FlightMode:
         HandleFlightMode();
         break;
         case ControlMode.HoverMode:
         HandleHoverMode();
         break;
      }
   }


   private void HandleFlightMode()
   {
      hoverModeController.enabled = false;
      flightModeController.enabled = true;
   }

   private void HandleHoverMode()
   {
      flightModeController.enabled = false;
      hoverModeController.enabled = true;
   }

   private void HandleRightShoulderInput(bool isPressed)
   {
      if ( isPressed )
      {
         if ( currentControlMode == ControlMode.FlightMode )
         {
            currentControlMode = ControlMode.HoverMode;
         }
         else if ( currentControlMode == ControlMode.HoverMode )
         {
            currentControlMode = ControlMode.FlightMode;
         }
      }
   }

   internal void InitializeBoundary()
   {
      Debug.Log("TODO: Initialize Play Level Boundary for the player.");
   }
}
