using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float deadZoneRadius = 1f;

    private Vector3 targetOffset;
    private float zoomSpeed;
    private bool isZooming;

    private Coroutine currentZoomCoroutine;

    private void Start()
    {
        if (target != null)
        {
            transform.position = offset;
            targetOffset = offset;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z);

        Vector3 targetPositionWithoutZOffset = new Vector3(target.position.x, target.position.y, offset.z);
        Vector3 cameraPositionWithoutZOffset = new Vector3(transform.position.x, transform.position.y, offset.z);

        // Calculate the distance in the X and Y plane
        Vector3 cameraToTargetXY = targetPositionWithoutZOffset - cameraPositionWithoutZOffset;

        // Check if the target is outside the dead zone in the X and Y plane
        if (cameraToTargetXY.magnitude > deadZoneRadius)
        {
            // Calculate new X and Y positions for the camera
            Vector3 newPositionXY = Vector3.Lerp(cameraPositionWithoutZOffset, targetPositionWithoutZOffset, smoothSpeed * Time.deltaTime);

            // Update the camera's position
            transform.position = new Vector3(newPositionXY.x, newPositionXY.y, offset.z); // Maintain static Z
        }

    }

    public void StartZoom(Vector3 newOffset, float zoomDuration)
    {
        Debug.Log("in StartZoom");

        if (currentZoomCoroutine != null)
        {
            StopCoroutine(currentZoomCoroutine);
        }
        currentZoomCoroutine = StartCoroutine(ZoomToOffset(newOffset, zoomDuration));
    }

    private IEnumerator ZoomToOffset(Vector3 newOffset, float duration)
    {
        Debug.Log("In ZoomToOffset");

        float time = 0;
        Vector3 startOffset = offset;

        while (time < duration)
        {
            time += Time.deltaTime;
            offset = Vector3.Lerp(startOffset, newOffset, time / duration);

            Debug.Log($"{offset.z}");

            yield return null;
        }

        offset = newOffset;
    }
}
