using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public delegate void Interaction();
    public static Interaction interaction;

    private Rigidbody rigidBody;
    private InputControls inputController;
    private InputAction vertical, horizontal;
    private Vector3 movement, clampedVelocity;
    private float speed = 10;
    private bool canMove = true;

    void Start()
    {
        inputController = new InputControls();
        inputController.Enable();
        vertical = inputController.Actions.Vertical;
        horizontal = inputController.Actions.Horizontal;
        inputController.Actions.Interact.performed += InteractionEvent;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            movement = new Vector3(horizontal.ReadValue<float>(), 0, vertical.ReadValue<float>()).normalized;
            rigidBody.velocity += movement;

            if (rigidBody.velocity.magnitude > 10)
                clampedVelocity = new Vector3(Mathf.Clamp(rigidBody.velocity.x, -10, 10), 0, Mathf.Clamp(rigidBody.velocity.z, -10, 10)).normalized * speed;
            else
                clampedVelocity = new Vector3(Mathf.Clamp(rigidBody.velocity.x, -10, 10), 0, Mathf.Clamp(rigidBody.velocity.z, -10, 10));

            if (movement.magnitude == 0)
                clampedVelocity *= 0.9f;

            rigidBody.velocity = clampedVelocity;
            Vector3 lookDirection = rigidBody.velocity;
            lookDirection.y = 0;
            transform.rotation = movement == Vector3.zero ? transform.rotation : Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.fixedDeltaTime * 5f);
        }
    }

    private void InteractionEvent(InputAction.CallbackContext context)
    {
        interaction?.Invoke();
    }

    private void OnDisable()
    {
        inputController.Actions.Interact.performed -= InteractionEvent;
        inputController.Disable();
    }
}