using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Move Assets")]
    private MainPlayerControls playerControls;

    [Header("Movement Values")]
    private Vector2 moveInput;
    private float leftTriggerInput;
    private float rightTriggerInput;

    //Movement Delegates
    public delegate void MoveInput(Vector2 movement);
    public delegate void StrafeInput(float leftTrigger, float rightTrigger);

    //Movement Events
    public event MoveInput OnMoveInput;
    public event StrafeInput OnStrafeInput;

    private void Awake()
    {
        playerControls = new MainPlayerControls();

        //Bind move action
        playerControls.PlayerControl.MovePlayer.performed += context => OnMoveInput?.Invoke(context.ReadValue<Vector2>());
        playerControls.PlayerControl.MovePlayer.canceled += context => OnMoveInput?.Invoke(Vector2.zero);

        // Bind trigger actions
        playerControls.PlayerControl.LeftTrigger.performed += context => leftTriggerInput = context.ReadValue<float>();
        playerControls.PlayerControl.LeftTrigger.canceled += context => leftTriggerInput = 0;
        playerControls.PlayerControl.RightTrigger.performed += context => rightTriggerInput = context.ReadValue<float>();
        playerControls.PlayerControl.RightTrigger.canceled += context => rightTriggerInput = 0;

    }

    private void Update()
    {
        OnStrafeInput?.Invoke(leftTriggerInput, rightTriggerInput);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
