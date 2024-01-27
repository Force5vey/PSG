using UnityEngine;

public class InputLogger :MonoBehaviour
{
   private InputHandler inputHandler;

   private void Awake()
   {
      inputHandler = GameController.Instance.inputHandler;

      if ( inputHandler != null )
      {
         // Subscribe to Stick Inputs
         inputHandler.OnLeftStickInput += HandleLeftStickInput;
         inputHandler.OnRightStickInput += HandleRightStickInput;

         // Subscribe to Stick Clicks
         inputHandler.OnLeftStickClickInput += isPressed => Debug.Log("Left Stick Click: " + isPressed);
         inputHandler.OnRightStickClickInput += isPressed => Debug.Log("Right Stick Click: " + isPressed);

         // Subscribe to Trigger Inputs
         inputHandler.OnLeftTriggerInput += value => Debug.Log("Left Trigger: " + value);
         inputHandler.OnRightTriggerInput += value => Debug.Log("Right Trigger: " + value);

         // Subscribe to Shoulder Button Inputs
         inputHandler.OnLeftShoulderInput += isPressed => Debug.Log("Left Shoulder: " + isPressed);
         inputHandler.OnRightShoulderInput += isPressed => Debug.Log("Right Shoulder: " + isPressed);

         // Subscribe to D-Pad Input
         inputHandler.OnDPadInput += HandleDPadInput;

         // Subscribe to Face Buttons
         inputHandler.OnNorthButtonInput += isPressed => Debug.Log("North Button: " + isPressed);
         inputHandler.OnSouthButtonInput += isPressed => Debug.Log("South Button: " + isPressed);
         inputHandler.OnEastButtonInput += isPressed => Debug.Log("East Button: " + isPressed);
         inputHandler.OnWestButtonInput += isPressed => Debug.Log("West Button: " + isPressed);

         // Subscribe to Menu Buttons
         inputHandler.OnPauseButtonInput += isPressed => Debug.Log("Pause Button: " + isPressed);
         inputHandler.OnMenuButtonInput += isPressed => Debug.Log("Menu Button: " + isPressed);
      }
      else
      {
         Debug.LogError("InputHandler not found!");
      }
   }

   private void HandleLeftStickInput( Vector2 input )
   {
      Debug.Log("Left Stick Input: " + input);
   }

   private void HandleRightStickInput( Vector2 input )
   {
      Debug.Log("Right Stick Input: " + input);
   }

   private void HandleDPadInput( Vector2 input )
   {
      Debug.Log("D-Pad Input: " + input);
   }

   // Add other methods if needed
}
