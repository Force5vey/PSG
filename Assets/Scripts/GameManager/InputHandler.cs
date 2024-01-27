using UnityEngine;

public class InputHandler :MonoBehaviour
{
   [Header("Input Assets")]
   private MainPlayerControls ctr;
   // DELEGATES
   // Sticks
   public delegate void LeftStickInput( Vector2 leftStickInput );
   public delegate void RightStickInput( Vector2 rightStickInput );
   public delegate void StickClickInput( bool isPressed );

   // Triggers and Shoulders
   public delegate void TriggerInput( float triggerInput );
   public delegate void ShoulderInput( bool isPressed );

   // DPad
   public delegate void DPadInput( Vector2 dPadInput );

   // FaceButtons - Performed and Canceled
   public delegate void FaceButtonInput( bool isPressed );
   public delegate void MenuButtonInput( bool isPressed );

   // EVENTS
   // Sticks
   public event LeftStickInput OnLeftStickInput;
   public event RightStickInput OnRightStickInput;
   public event StickClickInput OnLeftStickClickInput;
   public event StickClickInput OnRightStickClickInput;

   // Triggers and Shoulders
   public event TriggerInput OnLeftTriggerInput;
   public event TriggerInput OnRightTriggerInput;
   public event ShoulderInput OnLeftShoulderInput;
   public event ShoulderInput OnRightShoulderInput;

   // D-Pad
   public event DPadInput OnDPadInput;

   // Face Buttons
   public event FaceButtonInput OnNorthButtonInput;
   public event FaceButtonInput OnSouthButtonInput;
   public event FaceButtonInput OnEastButtonInput;
   public event FaceButtonInput OnWestButtonInput;
   // Menu Buttons
   public event MenuButtonInput OnPauseButtonInput;
   public event MenuButtonInput OnMenuButtonInput;



   private void Awake()
   {
      ctr = new MainPlayerControls();

      // Bind Left Stick
      ctr.PlayerControl.LeftStick.performed += context => OnLeftStickInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.LeftStick.canceled += _ => OnLeftStickInput?.Invoke(Vector2.zero);

      // Bind Right Stick
      ctr.PlayerControl.RightStick.performed += context => OnRightStickInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.RightStick.canceled += _ => OnRightStickInput?.Invoke(Vector2.zero);

      // Bind Stick Clicks
      ctr.PlayerControl.L3.performed += _ => OnLeftStickClickInput?.Invoke(true);
      ctr.PlayerControl.L3.canceled += _ => OnLeftStickClickInput?.Invoke(false);
      ctr.PlayerControl.R3.performed += _ => OnRightStickClickInput?.Invoke(true);
      ctr.PlayerControl.R3.canceled += _ => OnRightStickClickInput?.Invoke(false);

      // Bind Triggers
      ctr.PlayerControl.LeftTrigger.performed += context => OnLeftTriggerInput?.Invoke(context.ReadValue<float>());
      ctr.PlayerControl.LeftTrigger.canceled += _ => OnLeftTriggerInput?.Invoke(0);
      ctr.PlayerControl.RightTrigger.performed += context => OnRightTriggerInput?.Invoke(context.ReadValue<float>());
      ctr.PlayerControl.RightTrigger.canceled += _ => OnRightTriggerInput?.Invoke(0);

      // Bind Shoulder Buttons
      ctr.PlayerControl.LeftShoulder.performed += _ => OnLeftShoulderInput?.Invoke(true);
      ctr.PlayerControl.LeftShoulder.canceled += _ => OnLeftShoulderInput?.Invoke(false);
      ctr.PlayerControl.RightShoulder.performed += _ => OnRightShoulderInput?.Invoke(true);
      ctr.PlayerControl.RightShoulder.canceled += _ => OnRightShoulderInput?.Invoke(false);

      // Bind D-Pad
      ctr.PlayerControl.DPad.performed += context => OnDPadInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.DPad.canceled += _ => OnDPadInput?.Invoke(Vector2.zero);

      // Bind Face Buttons
      ctr.PlayerControl.NorthButton.performed += _ => OnNorthButtonInput?.Invoke(true);
      ctr.PlayerControl.NorthButton.canceled += _ => OnNorthButtonInput?.Invoke(false);
      ctr.PlayerControl.SouthButton.performed += _ => OnSouthButtonInput?.Invoke(true);
      ctr.PlayerControl.SouthButton.canceled += _ => OnSouthButtonInput?.Invoke(false);
      ctr.PlayerControl.EastButton.performed += _ => OnEastButtonInput?.Invoke(true);
      ctr.PlayerControl.EastButton.canceled += _ => OnEastButtonInput?.Invoke(false);
      ctr.PlayerControl.WestButton.performed += _ => OnWestButtonInput?.Invoke(true);
      ctr.PlayerControl.WestButton.canceled += _ => OnWestButtonInput?.Invoke(false);

      // Bind Menu Buttons
      ctr.PlayerControl.Pause.performed += _ => OnPauseButtonInput?.Invoke(true);
      ctr.PlayerControl.Pause.canceled += _ => OnPauseButtonInput?.Invoke(false);

      ctr.PlayerControl.Menu.performed += _ => OnMenuButtonInput?.Invoke(true);
      ctr.PlayerControl.Menu.canceled += _ => OnMenuButtonInput?.Invoke(false);
   }

   private void OnEnable()
   {
      ctr.Enable();
   }

   private void OnDisable()
   {
      ctr.Disable();
   }
}

