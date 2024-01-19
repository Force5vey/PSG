using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    private InputHandler inputHandler;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float yawSpeed;
    private Vector2 currentMoveInput;

    [Header("Flight Dynamics")]
    [Tooltip("Roll: X Axis: Wings dip and rise, axis is from nose to tail.")]
    [SerializeField] private bool allowRoll;
    [Tooltip("Pitch: Y Axis: Nose up or down, Climb or descend, the axis is from wing tip to wing tip.")]
    [SerializeField] private bool allowPitch;
    [Tooltip("Yah: Z Axis: The nose 'turns' left or right.")]
    [SerializeField] private bool allowYaw;
    [SerializeField] private float maxRollAngle;
    [SerializeField] private float maxPitchAngle;
    [SerializeField] private float maxAltitudeDeviation;
    [Header("Flight Recovery")]
    [SerializeField] private float rollRecoverySpeed;
    [SerializeField] private float pitchRecoverySpeed;
    [SerializeField] private float altitudeRecoverySpeed;
    [SerializeField] private float altitudeRecoveryStrength;
    private bool isRecoveringRoll = false;
    private bool isRecoveringPitch = false;
    private bool isRecoveringAltitude = false;


    /// Term Definitions:
    /// Rollout: Come out of a roll / bank and return to desired state
    /// Level Off: Come out of a climb or descent and return Pitch to desired State
    /// Recover: Return all axis to desired state.

    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        if (inputHandler != null)
        {
            inputHandler.OnMoveInput += HandleMoveInput;
        }
    }

    private void OnDestroy()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMoveInput -= HandleMoveInput;
        }
    }

    private void Start()
    {
        //TODO: Get start values from the level controller so the ship state can be effected by individual levels
        //will probably be an addition to LevelData scriptables 

    }

    private void HandleMoveInput(Vector2 movementInput)
    {
        currentMoveInput = movementInput;
    }

    //private void Update()
    //{
    //    Debug.Log("Current Yaw: " + rb.rotation.eulerAngles.z);

    //}

    private void FixedUpdate()
    {
        if (inputHandler != null)
        {
            MovePlayer(currentMoveInput);
            ConstrainAndRecover();
        }
    }

    void MovePlayer(Vector2 movementInput)
    {
        Vector3 moveDirection = new Vector3(movementInput.x, movementInput.y, 0).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveSpeed;
            rb.velocity = new Vector3(moveVelocity.x, moveVelocity.y, rb.velocity.z);

            // Optional: Handle rotation here if needed
        }

        if (allowYaw)
        {
            YawToTravelDirection();
        }
    }

    void YawToTravelDirection()
    {
        if (currentMoveInput.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(currentMoveInput.y, currentMoveInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle - 90);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, yawSpeed * Time.deltaTime);
        }
    }

    void ConstrainAndRecover()
    {
        if (!allowRoll)
        {
            RecoverRoll();
        }

        if (!allowPitch)
        {
            RecoverPitch();
        }

        ConstrainAltitude();
    }

    void RecoverRoll()
    {
        float currentRollAngle = rb.rotation.eulerAngles.x;
        if (!isRecoveringRoll && Mathf.Abs(currentRollAngle) > maxRollAngle)
        {
            StartCoroutine(RecoverRollCoroutine());
        }
    }

    IEnumerator RecoverRollCoroutine()
    {
        isRecoveringRoll = true;
        Quaternion startRotation = rb.rotation;
        // Target rotation: keep current yaw and pitch, reset roll
        Quaternion endRotation = Quaternion.Euler(0, startRotation.eulerAngles.y, startRotation.eulerAngles.z);

        float elapsedTime = 0;
        while (elapsedTime < rollRecoverySpeed)
        {
            rb.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rollRecoverySpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.rotation = endRotation;
        isRecoveringRoll = false;

    }

    void RecoverPitch()
    {
        float currentPitchAngle = rb.rotation.eulerAngles.y;
        if (!isRecoveringPitch && Mathf.Abs(currentPitchAngle) > maxPitchAngle)
        {
            StartCoroutine(RecoverPitchCoroutine());
        }
    }

    IEnumerator RecoverPitchCoroutine()
    {
        isRecoveringPitch = true;
        Quaternion startRotation = rb.rotation;
        // Target rotation: keep current yaw and roll, reset pitch
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles.x, 0, startRotation.eulerAngles.z);

        float elapsedTime = 0;
        while (elapsedTime < pitchRecoverySpeed)
        {
            rb.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / pitchRecoverySpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.rotation = endRotation;
        isRecoveringPitch = false;

    }

    void ConstrainAltitude()
    {
        // Check if the ship's altitude is within the allowed range
        float altitudeDeviation = transform.position.z;
        if (Mathf.Abs(altitudeDeviation) > maxAltitudeDeviation)
        {
            // Apply a force that pulls the ship towards z = 0
            float recoveryForceDirection = altitudeDeviation > 0 ? -1 : 1;
            Vector3 recoveryForce = new Vector3(0, 0, recoveryForceDirection * altitudeRecoveryStrength);
            rb.AddForce(recoveryForce);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected. Current Yaw: " + rb.rotation.eulerAngles.z);
        // Additional debugging and collision handling...
    }

}
