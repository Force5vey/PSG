using UnityEngine;

public class FlightModeController :MonoBehaviour
{
   [Header("Scripts")]
   [SerializeField] private InputHandler inputHandler;
   [SerializeField] private Rigidbody shipRigidbody;

   [Header("Current Input Settings")]
   private Vector2 currentLeftStickInput;
   private Vector2 currentRightStickInput;
   private float currentRightTriggerInput;
   private float currentLeftTriggerInput;

   [Header("Flight Mode Settings")]
   [SerializeField] private Transform shipTransform;
   private bool inFastMode = false;
   private bool isDodging = false;
   private bool isReversing = false;

   [Header("Ship Flight - Fast")]
   [SerializeField] private float fastThrustForce;
   [SerializeField] private float fastBrakingForce;
   [SerializeField] private float fastDefaultDrag;
   [SerializeField] private float fastRollTorque;
   [SerializeField] private float fastPitchTorque;
   [SerializeField] private float fastYawTorque;
   [Header("Fast Flight Visuals")]
   [SerializeField] private float visualFastRollTorque;
   [SerializeField] private float visualFastPitchTorque;
   [SerializeField] private float visualFastYawTorque;

   [Header("Ship Flight - Slow")]
   [SerializeField] private float slowThrustForce;
   [SerializeField] private float slowBrakingForce;
   [SerializeField] private float slowDefaultDrag;
   [SerializeField] private float slowRollTorque;
   [SerializeField] private float slowPitchTorque;
   [SerializeField] private float slowYawTorque;
   [Header("Slow Flight Visuals")]
   [SerializeField] private float visualSlowRollTorque;
   [SerializeField] private float visualSlowPitchTorque;
   [SerializeField] private float visualSlowYawTorque;




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
         inputHandler.OnLeftStickClickInput += HandleLeftStickClickInput;
         inputHandler.OnRightStickClickInput += HandleRightStickClickInput;

         //Triggers
         inputHandler.OnLeftTriggerInput += HandleLeftTriggerInput;
         inputHandler.OnRightTriggerInput += HandleRightTriggerInput;
         inputHandler.OnLeftShoulderInput += HandleLeftShoulderPressed;

      }
   }

   private void OnDisable()
   {
      if ( inputHandler != null )
      {
         //Sticks
         inputHandler.OnLeftStickInput -= HandleLeftStickInput;
         inputHandler.OnRightStickInput -= HandleRightStickInput;
         inputHandler.OnLeftStickClickInput -= HandleLeftStickClickInput;
         inputHandler.OnRightStickClickInput -= HandleRightStickClickInput;

         //Triggers
         inputHandler.OnLeftTriggerInput -= HandleLeftTriggerInput;
         inputHandler.OnRightTriggerInput -= HandleRightTriggerInput;
         inputHandler.OnLeftShoulderInput -= HandleLeftShoulderPressed;

      }
   }

   private void Start()
   {
   }

   private void FixedUpdate()
   {

      ProcessFlightModeThrust();
      ProcessBreaking();
      ProcessPitch();
      ProcessRoll();
      ProcessYaw();
      ProcessVisualPitch();
      ProcessVisualRoll();
      ProcessDodge();
      ProcessReversal();
      ProcessVisualYaw();
   }

   private void ProcessFlightModeThrust()
   {
      float totalThrust = inFastMode ? fastThrustForce : slowThrustForce;

      shipRigidbody.AddForce(currentRightTriggerInput * totalThrust * transform.forward, ForceMode.Acceleration);
   }
   private void ProcessBreaking()
   {
      float totalBraking;

      if ( currentLeftTriggerInput !=0 )
      {
         totalBraking = inFastMode ? fastBrakingForce : slowBrakingForce;
      }
      else
      {
         totalBraking = inFastMode ? fastDefaultDrag : slowDefaultDrag;
      }
      shipRigidbody.drag = totalBraking;
   }

   private void ProcessPitch()
   {
      float pitchRate = inFastMode ? fastPitchTorque : slowPitchTorque;
      transform.Rotate(Vector3.right, currentLeftStickInput.y * pitchRate * Time.fixedDeltaTime, Space.Self);
   }

   private void ProcessRoll()
   {
      float rollRate = inFastMode ? fastRollTorque : slowRollTorque;
      transform.Rotate(Vector3.forward, -currentLeftStickInput.x * rollRate * Time.fixedDeltaTime, Space.Self);
   }

   private void ProcessYaw()
   {
      float yawRate = inFastMode ? fastYawTorque : slowYawTorque;
      transform.Rotate(Vector3.up, currentRightStickInput.x * yawRate * Time.fixedDeltaTime, Space.Self);
   }


   private void ProcessVisualPitch()
   {
      float visualPitchLimit = inFastMode ? visualFastPitchTorque : visualSlowPitchTorque;
      float targetVisualPitch = Mathf.Clamp(currentLeftStickInput.y * visualPitchLimit, -visualPitchLimit, visualPitchLimit);

      // Calculate the visual rotation in local space
      Quaternion visualTargetRotation = Quaternion.Euler(targetVisualPitch, 0, 0);

      // Apply visual rotation
      shipTransform.localRotation = currentLeftStickInput.y != 0
          ? Quaternion.Slerp(shipTransform.localRotation, visualTargetRotation, Time.deltaTime)
          : Quaternion.Slerp(shipTransform.localRotation, Quaternion.identity, Time.deltaTime);
   }

   private void ProcessVisualRoll()
   {
      float visualRollLimit = inFastMode ? visualFastRollTorque : visualSlowRollTorque;
      float targetVisualRoll = Mathf.Clamp(-currentLeftStickInput.x * visualRollLimit, -visualRollLimit, visualRollLimit);

      // Calculate the visual rotation in local space
      Quaternion visualTargetRotation = Quaternion.Euler(0, 0, targetVisualRoll);

      // Apply visual rotation
      shipTransform.localRotation = currentLeftStickInput.x != 0
          ? Quaternion.Slerp(shipTransform.localRotation, visualTargetRotation, Time.deltaTime)
          : Quaternion.Slerp(shipTransform.localRotation, Quaternion.identity, Time.deltaTime);
   }

   private void ProcessVisualYaw()
   {
      float visualYawLimit = inFastMode ? visualFastYawTorque : visualSlowYawTorque;
      float targetVisualYaw = Mathf.Clamp(currentRightStickInput.x * visualYawLimit, -visualYawLimit, visualYawLimit);

      // Calculate the visual rotation in local space
      Quaternion visualTargetRotation = Quaternion.Euler(0, targetVisualYaw, 0);

      // Apply visual rotation
      shipTransform.localRotation = currentRightStickInput.x != 0
          ? Quaternion.Slerp(shipTransform.localRotation, visualTargetRotation, Time.deltaTime)
          : Quaternion.Slerp(shipTransform.localRotation, Quaternion.identity, Time.deltaTime);
   }

   private void ProcessDodge()
   {
      if ( isDodging )
      {
         //TODO: create CoRoutine that temp disables controls and performs a short altitude jump
      }
   }

   private void ProcessReversal()
   {
      if ( isReversing )
      {
         //TODO: create a coroutine that temp disables controls and performs a flight direction reversal
      }
   }


   #region // Handle Controller Input

   private void HandleLeftStickInput( Vector2 leftStickInput )
   {
      currentLeftStickInput = leftStickInput;
   }

   private void HandleLeftStickClickInput( bool isPressed )
   {
      if ( isPressed )
      {
         isDodging = !isDodging;
      }
   }

   private void HandleRightStickInput( Vector2 rightStickInput )
   {
      currentRightStickInput = rightStickInput;
   }

   private void HandleRightStickClickInput( bool isPressed )
   {
      if ( isPressed )
      {
         isReversing = !isReversing;
      }
   }

   private void HandleLeftTriggerInput( float leftTriggerInput )
   {
      currentLeftTriggerInput = leftTriggerInput;
   }

   private void HandleRightTriggerInput( float rightTriggerInput )
   {
      currentRightTriggerInput = rightTriggerInput;
   }

   private void HandleLeftShoulderPressed( bool isPressed )
   {
      if ( isPressed )
      {
         inFastMode = !inFastMode;

         if ( inFastMode )
         {
            //sett more distant camera

         }
         else
         {
            //set close camera.         
         }

         Debug.Log($"inHighSpeed bool Value: {inFastMode}");
      }
   }

   #endregion
}
