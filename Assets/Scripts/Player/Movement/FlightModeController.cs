using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FlightModeController :MonoBehaviour
{
   [Header("Scripts")]
   [SerializeField] private InputHandler inputHandler;
   [SerializeField] private Rigidbody shipRigidbody;

   [Header("Flight Mode Settings")]
   [SerializeField] private float flightThrustForce;
   [SerializeField] private float flightBreakingForce;
   [SerializeField] private float defaultDrag;
   [SerializeField] private float flightRollTorque;
   [SerializeField] private float flightPitchTorque;


   [Header("Current Input Settings")]
   private Vector2 currentLeftStickInput;
   private Vector2 currentRightStickInput;
   private float currentDragInput;
   private float currentThrustInput;
   private bool currentLeftShoulderPressed;
   private bool currentRightShoulderPressed;


   private void Awake()
   {

   }

   private void OnEnable()
   {
      if ( inputHandler != null )
      {

         //Sticks
         inputHandler.OnLeftStickInput += HandleLeftStickInput;
         inputHandler.OnRightStickInput += HandleRightStickInput;
         //Triggers
         inputHandler.OnLeftTriggerInput += HandleLeftTriggerInput;
         inputHandler.OnRightTriggerInput += HandleRightTriggerInput;
         //Shoulders
         inputHandler.OnLeftShoulderPressed += HandleLeftShoulderPressed;
      }
   }

   private void OnDisable()
   {
      if ( inputHandler != null )
      {
         //Sticks
         inputHandler.OnLeftStickInput -= HandleLeftStickInput;
         inputHandler.OnRightStickInput -= HandleRightStickInput;
         //Triggers
         inputHandler.OnLeftTriggerInput -= HandleLeftTriggerInput;
         inputHandler.OnRightTriggerInput -= HandleRightTriggerInput;
         //Shoulders
         inputHandler.OnLeftShoulderPressed -= HandleLeftShoulderPressed;
      }
   }

   private void FixedUpdate()
   {
      ProcessFlightModeThrust();
      ProcessBreakingDrag();
      ProcessPitch();
      ProcessRoll();
   }

   private void ProcessFlightModeThrust()
   {
      shipRigidbody.AddForce(transform.forward * currentThrustInput * flightThrustForce, ForceMode.Acceleration);
   }
   
   private void ProcessBreakingDrag()
   {
      if ( currentDragInput > .1f )
      {
         shipRigidbody.drag = currentDragInput * flightBreakingForce;
      }
      else
      {
         shipRigidbody.drag = defaultDrag;
      }
   }

   //TODO: Try using transform rotation, or adjust mass and speeds to help control the ship getting out of hand.

   private void ProcessPitch()
   {
      Quaternion pitch = Quaternion.AngleAxis(currentRightStickInput.y * flightPitchTorque * Time.fixedDeltaTime, transform.right);

      Quaternion roll = Quaternion.AngleAxis(-currentLeftStickInput.x * flightRollTorque * Time.fixedDeltaTime, transform.forward);

      shipRigidbody.MoveRotation(shipRigidbody.rotation * pitch);
   }

   private void ProcessRoll()
   {
      Quaternion roll = Quaternion.AngleAxis(-currentLeftStickInput.x * flightRollTorque * Time.fixedDeltaTime, transform.forward);

      shipRigidbody.MoveRotation(shipRigidbody.rotation * roll);
   }



   #region //Methods to Handle Controller Input

   private void HandleLeftStickInput( Vector2 leftStickInput )
   {
      currentLeftStickInput = leftStickInput;
   }

   private void HandleRightStickInput( Vector2 rightStickInput )
   {
      currentRightStickInput = rightStickInput;
   }

   private void HandleLeftTriggerInput( float leftTriggerInput )
   {
      currentDragInput = leftTriggerInput;
   }

   private void HandleRightTriggerInput( float rightTriggerInput )
   {
      currentThrustInput = rightTriggerInput;
   }

   private void HandleLeftShoulderPressed()
   {
      //TODO: Cycle Camera Views.
   }



   #endregion
}
