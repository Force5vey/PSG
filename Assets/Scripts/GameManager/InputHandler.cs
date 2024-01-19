using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private MainPlayerControls playerControls;
    private Vector2 moveInput;

    public delegate void MoveInput(Vector2 movement);
    public event MoveInput OnMoveInput;

    private void Awake()
    {
        playerControls = new MainPlayerControls();

        //Bind move action
        playerControls.PlayerControl.MovePlayer.performed += context => OnMoveInput?.Invoke(context.ReadValue<Vector2>());
        playerControls.PlayerControl.MovePlayer.canceled += context => OnMoveInput?.Invoke(Vector2.zero);

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
