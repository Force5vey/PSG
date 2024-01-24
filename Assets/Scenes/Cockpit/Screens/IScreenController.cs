/// <summary>
/// Interface for screen controllers in the cockpit.
/// </summary>
public interface ICockpitScreenController
{

   /// <summary>
   /// Initialize the screen. This method should set up any necessary data and prepare the screen for display.
   /// </summary>
   void InitializeScreen();

   /// <summary>
   /// This method will enable the necessary UI elements and interaction mechanics within the screen. It could include enabling buttons, sliders, and other interactive elements.
   /// </summary>
   void EnableScreenInteractions();

   /// <summary>
   /// Display the screen. This method should handle animations and UI elements to show the screen.
   /// </summary>
   void DisplayScreen();

   /// <summary>
   /// Close the screen. This method should handle any cleanup, animations, and transitions required to hide the screen.
   /// </summary>
   void CloseScreen();

   /// <summary>
   /// Refresh the screen data. This method is used to update the screen's content dynamically (e.g., updating radar blips).
   /// </summary>
   void RefreshScreenData();

   /// <summary>
   /// Save changes made in the screen. This method is responsible for persisting any changes or settings adjusted in the screen.
   /// </summary>
   void SaveScreenChanges();

   /// <summary>
   /// Receive input from other game systems. This method should handle input or data from other parts of the game that affect the screen.
   /// </summary>
   void ReceiveExternalInput();

   /// <summary>
   /// This method will manage any audio effects specific to the screen, like button clicks or screen-specific sound effects.
   /// </summary>
   void HandleAudioEffects(); 
}
