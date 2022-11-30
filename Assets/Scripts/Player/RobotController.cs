using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 inputs, direction, lookDirection, savedDirection;
    private bool newDirection = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        instance.inputControls.Actions.Vertical.canceled += SaveDirectionInput;
        instance.inputControls.Actions.Horizontal.canceled += SaveDirectionInput;
        instance.inputControls.Actions.Vertical.performed += AddDirectionInput;
        instance.inputControls.Actions.Horizontal.performed += AddDirectionInput;
    }

    void FixedUpdate()
    {
        inputs = new Vector3(instance.horizontal.ReadValue<float>(), 0, instance.vertical.ReadValue<float>());
        direction = Camera.main.transform.forward * inputs.z + Camera.main.transform.right * inputs.x;

        if(inputs != Vector3.zero && !newDirection)
        {
            newDirection = true;
            savedDirection = direction;
        }

        if (inputs != Vector3.zero)
            direction = savedDirection;

        rigidBody.velocity += new Vector3(direction.x, 0, direction.z) * 0.35f;
        lookDirection = rigidBody.velocity;

        transform.rotation = inputs == Vector3.zero ? transform.rotation : Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.fixedDeltaTime * 5f);
    }

    private void SaveDirectionInput(InputAction.CallbackContext callbackContext)
    {
        if(instance.vertical.ReadValue<float>() == 0 && instance.horizontal.ReadValue<float>() == 0)
        {
            newDirection = false;
        }
    }

    private void AddDirectionInput(InputAction.CallbackContext callbackContext)
    {
        newDirection = false;
    }
}
