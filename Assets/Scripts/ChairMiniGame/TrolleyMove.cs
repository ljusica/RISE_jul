using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static InputManager;

public class TrolleyMove : MonoBehaviour
{
    public bool canMove;

    private Rigidbody rigidBody;
    private float speed = 7.5f;
    private Vector3 movement, clampedVelocity;

    void Start()
    {
        canMove = true;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            movement = new Vector3(instance.horizontal.ReadValue<float>(), 0, 0);
            rigidBody.AddForce(movement * speed, ForceMode.Acceleration);
        }
    }
}
