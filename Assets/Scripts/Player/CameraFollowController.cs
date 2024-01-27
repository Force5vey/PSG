using UnityEngine;

public class CameraFollowController :MonoBehaviour
{
   public Transform target; // The target (ship) the camera is following
   public Vector3 offset; // Position offset relative to the target
   public Vector3 rotationOffset; // Rotation offset relative to the target
   public float distanceSubtract = 10f; // Amount to subtract from the distance to set the near clipping plane

   private Camera cam;

   private void Awake()
   {
      cam = GetComponent<Camera>(); // Get the camera component
   }

   private void LateUpdate()
   {
      if ( target == null )
         return;

      // Update camera position with offset
      transform.position = target.position + target.TransformDirection(offset);

      // Update camera rotation with offset
      transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);

      // Calculate distance from the camera to the target and adjust near clipping plane
      float distanceToTarget = Vector3.Distance(transform.position, target.position);
      cam.nearClipPlane = Mathf.Max(distanceToTarget - distanceSubtract, 0.01f); // Ensure the near clipping plane is not negative
   }

   public void UpdateCameraOffset(Vector3 newOffset, Vector3 newRotation)
   {
      offset = newOffset;
      rotationOffset = newRotation;
   }
}
