using System.Collections;

using UnityEngine;

public class CameraFollowController :MonoBehaviour
{
   [HideInInspector] public Transform target;

  private InputHandler inputHandler;

   public float smoothSpeed = 0.125f;
   public Vector3 offset;
   public float followAngle = 25f;
   public float rotationSpeed = 5f;
   public float distanceFromTarget = 10f;

   private float currentThrustInput;
   [SerializeField] private float thrustFollowThreshold;
   [SerializeField] private float highSmoothSpeed;
   [SerializeField] private float lowSmoothSpeed;

   private Coroutine currentZoomCoroutine;

   private void Awake()
   {
      inputHandler = GameController.Instance.inputHandler;
   }

   private void OnEnable()
   {
      inputHandler.OnRightTriggerInput += HandleRightTriggerInput;
   }
   private void Start()
   {
      if ( target != null )
      {
         transform.position = offset;
      }
   }

   private void LateUpdate()
   {
      if ( target == null )
         return;

      AdjustCameraFollowSpeed();

      // Calculate the offset position at an angle
      Vector3 offsetPosition = Quaternion.Euler(followAngle, 0, 0) * -target.forward * distanceFromTarget;
      Vector3 desiredPosition = target.position + offsetPosition;

      // Smoothly move the camera to the desired position
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
      transform.position = smoothedPosition;

      // Smoothly rotate the camera to look at the target
      Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
   }

   private void AdjustCameraFollowSpeed()
   {
      if(Mathf.Abs(currentThrustInput) > thrustFollowThreshold)
      {
         smoothSpeed = highSmoothSpeed;
      }
      else
      {
         smoothSpeed = lowSmoothSpeed;
      }
   }

   public void StartZoom( Vector3 newOffset, float zoomDuration )
   {
      if ( currentZoomCoroutine != null )
      {
         StopCoroutine(currentZoomCoroutine);
      }
      currentZoomCoroutine = StartCoroutine(ZoomToOffset(newOffset, zoomDuration));
   }

   private IEnumerator ZoomToOffset( Vector3 newOffset, float duration )
   {
      float time = 0;
      Vector3 startOffset = offset;

      while ( time < duration )
      {
         time += Time.deltaTime;
         offset = Vector3.Lerp(startOffset, newOffset, time / duration);

         yield return null;
      }

      offset = newOffset;
   }

   private void HandleRightTriggerInput(float rtInput)
   {
      currentThrustInput = rtInput;
   }
}
