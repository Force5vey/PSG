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

   [Header("Flight Mode Settings")]
   [SerializeField] private Transform shipTransform;
   private bool inFastMode = false;

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
         inputHandler.OnLeftStickInput -= HandleLeftStickInput;
         inputHandler.OnRightStickInput -= HandleRightStickInput;
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
      ProcessPitch();
      ProcessRoll();
      ProcessYaw();
      ProcessVisualPitch();
      ProcessVisualRoll();
      ProcessVisualYaw();
   }

   private void ProcessFlightModeThrust()
   {
      //Got rid of pulse force and instead this will be fast or slow flight mode differences
      float totalThrust = currentRightTriggerInput > 0.98f ? fastThrustForce : slowThrustForce;
      shipRigidbody.AddForce(currentRightTriggerInput * totalThrust * transform.forward, ForceMode.Acceleration);
   }


   private void ProcessPitch()
   {
      //update to use slow pitch and fast pitch instead of math.
      float pitchRate = inFastMode ? fastPitchTorque * 2 : fastPitchTorque;
      transform.Rotate(Vector3.right, currentLeftStickInput.y * pitchRate * Time.fixedDeltaTime, Space.Self);
   }

   private void ProcessRoll()
   {
      transform.Rotate(Vector3.forward, -currentRightStickInput.x * fastRollTorque * Time.fixedDeltaTime, Space.Self);
   }

   private void ProcessYaw()
   {
      transform.Rotate(Vector3.up, currentLeftStickInput.x * fastYawTorque * Time.fixedDeltaTime, Space.Self);
   }

   private void ProcessVisualPitch()
   {
      float visualPitchLimit = inFastMode ? 30f : 50f;
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
      float visualRollLimit = 60f;
      float targetVisualRoll = Mathf.Clamp(-currentRightStickInput.x * visualRollLimit, -visualRollLimit, visualRollLimit);

      // Calculate the visual rotation in local space
      Quaternion visualTargetRotation = Quaternion.Euler(0, 0, targetVisualRoll);

      // Apply visual rotation
      shipTransform.localRotation = currentRightStickInput.x != 0
          ? Quaternion.Slerp(shipTransform.localRotation, visualTargetRotation, Time.deltaTime)
          : Quaternion.Slerp(shipTransform.localRotation, Quaternion.identity, Time.deltaTime);
   }

   private void ProcessVisualYaw()
   {

   }

   #region // Handle Controller Input

   private void HandleLeftStickInput( Vector2 leftStickInput )
   {
      currentLeftStickInput = leftStickInput;
   }

   private void HandleLeftStickClickInput(bool isPressed)
   {

   }

   private void HandleRightStickInput( Vector2 rightStickInput )
   {
      currentRightStickInput = rightStickInput;
   }

   private void HandleRightStickClickInput(bool isPressed)
   {

   }

   private void HandleLeftTriggerInput(float  leftTriggerInput)
   {

   }

   private void HandleRightTriggerInput( float rightTriggerInput )
   {
      currentRightTriggerInput = rightTriggerInput;
   }

   private void HandleLeftShoulderPressed(bool isPressed )
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

   #endregion
}
