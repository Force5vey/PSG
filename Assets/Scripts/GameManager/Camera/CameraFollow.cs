using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Set in LevelController script
    [HideInInspector] public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float deadZoneRadius = 1f; // Radius of the dead zone

  
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPositionWithOffset = target.position + offset;
        Vector3 cameraToTarget = targetPositionWithOffset - transform.position;

        // Check if the target is outside the dead zone
        if (cameraToTarget.magnitude > deadZoneRadius)
        {
            // Calculate position where the camera should start following
            Vector3 startFollowingPosition = targetPositionWithOffset - cameraToTarget.normalized * deadZoneRadius;
            Vector3 newPosition = Vector3.Lerp(transform.position, startFollowingPosition, smoothSpeed * Time.deltaTime);
            transform.position = newPosition;
        }

        transform.LookAt(target.position);
    }

}
