using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FallingChair : MonoBehaviour
{
    private Rigidbody rigidBody;
    private bool isCollected;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!isCollected) rigidBody.AddForce(0, -4, 0);
    }

    public void CollectChair()
    {
        rigidBody.mass = 0.1f;
        isCollected = true;
    }
}
