using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelData levelData;

    public PlayerSpawner playerSpawner;

    void Start()
    {
        // Ensure the player spawner and player instance are available
        if (playerSpawner != null && PlayerController.Instance != null)
        {
            // Position the player at the spawner's location
            PlayerController.Instance.transform.position = playerSpawner.spawnPosition;

            // Activate the player object if it's not already active
            if (!PlayerController.Instance.gameObject.activeSelf)
            {
                PlayerController.Instance.gameObject.SetActive(true);
            }

            StartCoroutine(RotateShipToStartPlay());
        }
    }


    IEnumerator RotateShipToStartPlay()
    {
        float duration = 1f; // Duration over which the rotation will happen
        float currentTime = 0f;

        // Initial and final rotations
        Quaternion startRotation = PlayerController.Instance.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(-90, 0, 0);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float fraction = currentTime / duration; // This will go from 0 to 1

            // Smoothly interpolate rotation from the startRotation to endRotation
            PlayerController.Instance.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fraction);

            yield return null; // Wait for the next frame
        }

        // Ensure the rotation is exactly the target rotation
        PlayerController.Instance.transform.rotation = endRotation;
    }


}
