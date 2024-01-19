using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private InputHandler inputHandler;
    private Vector2 currentMoveInput;

    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
      if(inputHandler != null)
        {
            currentMoveInput = inputHandler.GetMoveInput();
            
        }  

      //if(transform.position.z !=0)
      //  {
      //      transform.position = new Vector3(transform.position.x, transform.position.y, 0);
      //  }
      //if(transform.rotation.x !=-90 || transform.rotation.y !=0)
      //  {
      //      transform.rotation = Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z);
      //  }

    }

    private void FixedUpdate()
    {
        MovePlayer(currentMoveInput);
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
    }
}
