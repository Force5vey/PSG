using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InputHandler :MonoBehaviour
{
   [Header("Input Assets")]
   private MainPlayerControls playerControls;

   [Header("Movement Values")]
   private Vector2 moveInput;
   private Vector2 orientationInput;
   private float leftTriggerInput;
   private float rightTriggerInput;

   //Movement and Orientation Delegates
   public delegate void LeftStickInput( Vector2 LeftStickInput );
   public delegate void RightStickInput( Vector2 RightStickInput );
   public delegate void LeftTriggerInput( float leftTriggerInput );
   public delegate void RightTriggerInput( float rightTriggerInput );
   public delegate void LeftShoulderInput();
   public delegate void RightShoulderInput();

   //Movement and Orientation Events
   public event LeftStickInput OnLeftStickInput;
   public event RightStickInput OnRightStickInput;
   public event LeftTriggerInput OnLeftTriggerInput;
   public event RightTriggerInput OnRightTriggerInput;
   public event LeftShoulderInput OnLeftShoulderPressed;
   public event RightShoulderInput OnRightShoulderPressed;


   private void Awake()
   {
      playerControls = new MainPlayerControls();

      //Bind Left Stick
      playerControls.PlayerControl.LeftStick.performed += context => OnLeftStickInput?.Invoke(context.ReadValue<Vector2>());
      playerControls.PlayerControl.LeftStick.canceled += _ => OnLeftStickInput?.Invoke(Vector2.zero);

      // Bind Right Stick
      playerControls.PlayerControl.RightStick.performed += context => OnRightStickInput?.Invoke(context.ReadValue<Vector2>());
      playerControls.PlayerControl.RightStick.canceled += _ => OnRightStickInput?.Invoke(Vector2.zero);

      // Bind Triggers
      playerControls.PlayerControl.LeftTrigger.performed += context => OnLeftTriggerInput?.Invoke(context.ReadValue<float>());
      playerControls.PlayerControl.LeftTrigger.canceled += _ => OnLeftTriggerInput?.Invoke(0);

      playerControls.PlayerControl.RightTrigger.performed += context => OnRightTriggerInput?.Invoke(context.ReadValue<float>());
      playerControls.PlayerControl.RightTrigger.canceled += _ => OnRightTriggerInput?.Invoke(0);

      //Bind Shoulder Buttons
      playerControls.PlayerControl.LeftShoulder.performed += _ => OnLeftShoulderPressed?.Invoke();

      playerControls.PlayerControl.RightShoulder.performed += _ => OnRightShoulderPressed?.Invoke();


      //Bind Face Buttons

      //Bind Menu and Pause Buttons

      //Bind D-Pad Directions
      //TODO: Create D-Pad Mappings

   }

   private void OnEnable()
   {
      playerControls.Enable();
   }

   private void OnDisable()
   {
      playerControls.Disable();
   }
}
