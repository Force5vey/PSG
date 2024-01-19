using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Level Scripts")]
    public LevelData levelData;
    public CameraFollow cameraFollowScript;

    [Header("Player Scripts")]
    public PlayerSpawner playerSpawner;

    [Header("Player Effects")]
    [SerializeField] private float shipAlignDuration = 1.0f;


    void Start()
    {
        if (playerSpawner != null && PlayerController.Instance != null)
        {
            cameraFollowScript.target = PlayerController.Instance.transform;
                        StartLevel();
        }
        else
        {
            Debug.LogError("Critical Error at LevelController Start");
        }

    }

    void StartLevel()
    {
        // Activate the player object if it's not already active
        if (!PlayerController.Instance.gameObject.activeSelf)
        {
            PlayerController.Instance.gameObject.SetActive(true);
        }
        // Position the player at the spawner's location
        PlayerController.Instance.transform.position = playerSpawner.spawnPosition;

        StartCoroutine(AlignShipRotation(Quaternion.Euler(-90,0,0), shipAlignDuration));
    }

    void EndLevel()
    {
        StartCoroutine(AlignShipRotation(Quaternion.Euler(0, 0, 0), shipAlignDuration));
    }

    IEnumerator AlignShipRotation(Quaternion targetRotation, float duration)
    {
        Transform shipTransform = PlayerController.Instance.GetShipTransform();
        if (shipTransform == null)
        {
            Debug.LogError("Ship GameObject not found");
            yield break;
        }

        float currentTime = 0f;
        Quaternion startRotation = shipTransform.rotation;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float fraction = currentTime / duration; // This will go from 0 to 1

            shipTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, fraction);

            yield return null; // Wait for the next frame
        }

        // Ensure the rotation is exactly the target rotation
        shipTransform.rotation = targetRotation;
    }



}
