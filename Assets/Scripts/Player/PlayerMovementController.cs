using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    private InputHandler inputHandler;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float yawSpeed;
    [SerializeField] private float maxYawSpeed;
    private Vector2 currentMoveInput;
    private float leftTrigger;
    private float rightTrigger;

    [Header("Flight Dynamics")]
    [SerializeField] private bool allowYaw;
    //[SerializeField] private bool allowRoll;
    //[SerializeField] private bool allowPitch;
    //[SerializeField] private float maxRollAmount;
    //[SerializeField] private float rollRecoverySpeed;
    [SerializeField] private bool constrainPitchAndRoll;
    [SerializeField] private bool maintainAltitude;
    [SerializeField] private float maxAltitudeDeviation;

    [Header("Flight Recovery")]
    [SerializeField] private float altitudeRecoveryStrength;

    [Header("Boundary Control")]
    private Vector3 centerPoint;
    private float maxRadius;
    public bool isOutsideBoundary;

    //Events
    public event Action<bool> OnBoundaryCrossed;

    /// Term Definitions:
    /// Rollout: Come out of a roll / bank and return to desired state
    /// Level Off: Come out of a climb or descent and return Pitch to desired State
    /// Recover: Return all axis to desired state.
    /// Term Definitions:
    /// Roll - dip wings
    /// Pitch - climb or dive
    /// Yaw - spin / nose left / right

    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();

    }

    private void OnEnable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMoveInput += HandleMoveInput;
            inputHandler.OnStrafeInput += HandleYawInput;
        }
    }

    private void Start()
    {
        InitializeBoundary();
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMoveInput -= HandleMoveInput;
            inputHandler.OnStrafeInput -= HandleYawInput;
        }
    }

    private void HandleMoveInput(Vector2 movementInput)
    {
        currentMoveInput = movementInput;
    }
    void HandleYawInput(float left, float right)
    {
        leftTrigger = left;
        rightTrigger = right;
    }

    private void FixedUpdate()
    {
        MovePlayer(currentMoveInput, leftTrigger, rightTrigger);

        if (maintainAltitude)
        {
            ConstrainAltitude();
        }

        if (constrainPitchAndRoll)
        {
            ConstrainPitchAndRoll();
        }

        CheckBoundary();

    }


    private void InitializeBoundary()
    {
        PlayerSpawner playerSpawner = FindObjectOfType<PlayerSpawner>();
        if (playerSpawner != null)
        {
            centerPoint = playerSpawner.spawnPosition;
        }
        else
        {
            Debug.LogError("PlayerSpawner not found in the scene.");
        }

        //TODO: To implement - figuring out T-Drive state and how it effects the max radius
        float tDriveRadiusExpander = PlayerController.Instance.GetTDriveRadiusExpander();

        maxRadius = PlayerController.Instance.currentLevelData.maxRadius + tDriveRadiusExpander;
    }
    private void CheckBoundary()
    {
        Vector3 offsetFromCenter = transform.position - centerPoint;
        bool currentlyOutsideBoundary = offsetFromCenter.sqrMagnitude > maxRadius * maxRadius;

        if (currentlyOutsideBoundary != isOutsideBoundary)
        {
            isOutsideBoundary = currentlyOutsideBoundary;
            OnBoundaryCrossed?.Invoke(isOutsideBoundary); // Trigger event
        }
    }

    void MovePlayer(Vector2 movementInput, float leftTrigger, float rightTrigger)
    {
        // Determine forward/backward movement speed based on stick input
        Vector3 moveDirection = new Vector3(movementInput.x, movementInput.y, 0).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed * movementInput.magnitude;

        // Apply movement velocity
        rb.velocity = new Vector3(moveVelocity.x, moveVelocity.y, rb.velocity.z);

        // Apply yaw rotation based on triggers
        if (allowYaw)
        {
            ApplyYawRotation(leftTrigger, rightTrigger);
        }
    }

    void ApplyYawRotation(float leftTrigger, float rightTrigger)
    {
        // Calculate yaw rotation amount (left: counterclockwise, right: clockwise)
        float yawRotationAmount = ((leftTrigger - rightTrigger) * yawSpeed);

        if (Mathf.Abs(leftTrigger) > 0.02f || Mathf.Abs(rightTrigger) > 0.02f)
        {
            if (Mathf.Abs(rb.angularVelocity.z) < maxYawSpeed)
            {
                rb.AddTorque(Vector3.forward * yawRotationAmount, ForceMode.VelocityChange);
            }
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    void ConstrainPitchAndRoll()
    {
        Quaternion constrainedRotation = Quaternion.Euler(0, 0, rb.rotation.eulerAngles.z);
        rb.rotation = constrainedRotation;
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
}
