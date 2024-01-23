using UnityEngine;
using UnityEngine.InputSystem; // Make sure to include this for the new Input System

public class CameraPanControl : MonoBehaviour
{
   [Header("Input System")]
   private MainPlayerControls controls;
   private Vector2 rightStickInput;

   [Header("Pan Settings")]
   [SerializeField] private float panSpeed = 5f;
   [SerializeField] private float panBoundaryLeft = -5f;
   [SerializeField] private float panBoundaryRight = 5f;
   [SerializeField] private float panBoundaryUp = 3f;
   [SerializeField] private float panBoundaryDown = -3f;

   [Header("Return to Center Settings")]
   [SerializeField] private float returnSpeed = 2f;
   private Quaternion originalRotation;

   [SerializeField] private Camera mainCamera;

   private void OnEnable()
   {
      if (controls == null)
      {
         controls = new MainPlayerControls();
         controls.PlayerControl.RightStick.performed += context => rightStickInput = context.ReadValue<Vector2>();
         controls.PlayerControl.RightStick.canceled += context => rightStickInput = Vector2.zero;
      }
      controls.Enable();
   }

   private void OnDisable()
   {
      controls.Disable();
   }

   void Awake()
   {
      //mainCamera = GetComponent<Camera>();
      originalRotation = mainCamera.transform.rotation; // Store the original position
   }

   void Update()
   {
      // Update the rotation vector based on right stick input
      Vector2 rotationInput = rightStickInput;

      // Calculate new rotation around the Y axis (for horizontal input)
      float newRotationY = mainCamera.transform.localEulerAngles.y + rotationInput.x * panSpeed * Time.deltaTime;

      // Calculate new rotation around the X axis (for vertical input)
      float newRotationX = mainCamera.transform.localEulerAngles.x + rotationInput.y * panSpeed * Time.deltaTime;

      // Convert the new rotation X to a value between -180 and 180 for proper clamping
      newRotationX = NormalizeAngle(newRotationX);

      // Normalize Y-axis angle
      newRotationY = NormalizeAngle(newRotationY);

      // Clamp the X rotation within the specified boundaries
      newRotationX = ClampAngle(newRotationX, originalRotation.eulerAngles.x - panBoundaryUp, originalRotation.eulerAngles.x + panBoundaryDown);

      // Clamp the Y rotation within the specified boundaries
      newRotationY = ClampAngle(newRotationY, originalRotation.eulerAngles.y - panBoundaryLeft, originalRotation.eulerAngles.y + panBoundaryRight);

      // Apply the new clamped rotation to the camera
      mainCamera.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0f);

      // If no input is detected, smoothly return to the original rotation
      if (rotationInput == Vector2.zero)
      {
         mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalRotation, returnSpeed * Time.deltaTime);
      }
   }
   float NormalizeAngle(float angle)
   {
      while (angle > 180f) angle -= 360f;
      while (angle < -180f) angle += 360f;
      return angle;
   }

   float ClampAngle(float angle, float min, float max)
   {
      return Mathf.Clamp(angle, min, max);
   }
}
