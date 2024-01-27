using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InputHandler :MonoBehaviour
{
   [Header("Input Assets")]
   private MainPlayerControls ctr;


   //Sticks
   public delegate void LeftStickInput( Vector2 LeftStickInput );
   public delegate void RightStickInput( Vector2 RightStickInput );
   public delegate void LeftStickClickInput();
   public delegate void RightStickClickInput();

   //Triggers and Shoulders
   public delegate void LeftTriggerInput( float leftTriggerInput );
   public delegate void RightTriggerInput( float rightTriggerInput );
   public delegate void LeftShoulderInput();
   public delegate void RightShoulderInput();

   //DPad
   public delegate void DPadInput( Vector2 DPadInput );

   //FaceButtons - Performed
   public delegate void NorthButton();
   public delegate void SouthButton();
   public delegate void EastButton();
   public delegate void WestButton();

   //FaceButtons - IsInProgress

   //Sticks
   public event LeftStickInput OnLeftStickInput;
   public event RightStickInput OnRightStickInput;
   public event LeftStickClickInput OnLeftStickClickInput;
   public event RightStickClickInput OnRightStickClickInput;

   //Triggers and Shoulders
   public event LeftTriggerInput OnLeftTriggerInput;
   public event RightTriggerInput OnRightTriggerInput;
   public event LeftShoulderInput OnLeftShoulderPerformed;
   public event RightShoulderInput OnRightShoulderPerformed;

   //DPad
   public event DPadInput OnDPadInput;

   //FaceButtons Performed
   public event NorthButton OnNorthButtonPerformed;
   public event SouthButton OnSouthButtonPerformed;
   public event EastButton OnEastButtonPerformed;
   public event WestButton OnWestButtonPerformed;

   //FaceButtons IsInProgress


   private void Awake()
   {
      ctr = new MainPlayerControls();

      //Bind Left Stick
      ctr.PlayerControl.LeftStick.performed += context => OnLeftStickInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.LeftStick.canceled += _ => OnLeftStickInput?.Invoke(Vector2.zero);
      ctr.PlayerControl.L3.performed += _ => OnLeftStickClickInput?.Invoke();

      // Bind Right Stick
      ctr.PlayerControl.RightStick.performed += context => OnRightStickInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.RightStick.canceled += _ => OnRightStickInput?.Invoke(Vector2.zero);
      ctr.PlayerControl.R3.performed += context => OnRightStickClickInput?.Invoke();

      // Bind Triggers
      ctr.PlayerControl.LeftTrigger.performed += context => OnLeftTriggerInput?.Invoke(context.ReadValue<float>());
      ctr.PlayerControl.LeftTrigger.canceled += _ => OnLeftTriggerInput?.Invoke(0);

      ctr.PlayerControl.RightTrigger.performed += context => OnRightTriggerInput?.Invoke(context.ReadValue<float>());
      ctr.PlayerControl.RightTrigger.canceled += _ => OnRightTriggerInput?.Invoke(0);

      //Bind Shoulder Buttons
      ctr.PlayerControl.LeftShoulder.performed += _ => OnLeftShoulderPerformed?.Invoke();
      ctr.PlayerControl.RightShoulder.performed += _ => OnRightShoulderPerformed?.Invoke();

      //Bind D-Pad
      ctr.PlayerControl.DPad.performed += context => OnDPadInput?.Invoke(context.ReadValue<Vector2>());
      ctr.PlayerControl.DPad.canceled += _ => OnDPadInput?.Invoke(Vector2.zero);

      //Bind Face Buttons
      ctr.PlayerControl.NorthButton.performed += _ => OnNorthButtonPerformed?.Invoke();
      ctr.PlayerControl.SouthButton.performed += _ => OnSouthButtonPerformed?.Invoke();

      //Bind Menu and Pause Buttons


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
