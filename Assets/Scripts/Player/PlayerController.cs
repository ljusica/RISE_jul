using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public class PlayerController : MonoBehaviour
{
    public delegate void Interaction();
    public static Interaction interaction;

    private Rigidbody rigidBody;
    private Vector3 movement, clampedVelocity;
    public float speed = 10;
    public bool canMove = true;
    private float targetAngleY;
    private Vector3 camCompensatedMovement;

    void Start()
    {
        instance.inputControls.Actions.Interact.performed += InteractionEvent;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CameraLookRotation();

        if (canMove)
        {
            movement = new Vector3(instance.horizontal.ReadValue<float>(), 0, instance.vertical.ReadValue<float>()).normalized;
            rigidBody.velocity += camCompensatedMovement;

            if (rigidBody.velocity.magnitude > 10)
                clampedVelocity = new Vector3(Mathf.Clamp(rigidBody.velocity.x, -10, 10), 0, Mathf.Clamp(rigidBody.velocity.z, -10, 10)).normalized * speed;
            else
                clampedVelocity = new Vector3(Mathf.Clamp(rigidBody.velocity.x, -10, 10), 0, Mathf.Clamp(rigidBody.velocity.z, -10, 10));

            if (movement.magnitude == 0)
            {
                //camCompensatedMovement = Vector3.zero;
                camCompensatedMovement *= 0.9f;
            }


            rigidBody.velocity = clampedVelocity;
            Vector3 lookDirection = rigidBody.velocity;
            lookDirection.y = 0;
            transform.rotation = movement == Vector3.zero ? transform.rotation : Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.fixedDeltaTime * 5f);
        }
    }

    private void CameraLookRotation()
    {
        //Rotate towards input dir.
        targetAngleY = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Rotation();
    }

    private void Rotation()
    {
        if (movement != Vector3.zero)
        {
            camCompensatedMovement = Quaternion.Euler(movement.x, targetAngleY, 0f) * Vector3.forward;
        }
    }

    private void InteractionEvent(InputAction.CallbackContext context)
    {
        interaction?.Invoke();
    }

    private void OnDisable()
    {
        instance.inputControls.Actions.Interact.performed -= InteractionEvent;
        instance.inputControls.Disable();
    }
}