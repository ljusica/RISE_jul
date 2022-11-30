using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FallingChair : MonoBehaviour
{
    private Rigidbody rigidBody;
    private bool isSetInPlace;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!isSetInPlace) rigidBody.AddForce(0, -5, 0);
    }

    public async Task SetChairInPlace(Transform trolley)
    {
        float endTime = Time.time + 2f;

        while(Time.time < endTime)
        {
            await Task.Yield();
        }

        gameObject.transform.SetParent(trolley);
        rigidBody.isKinematic = true;
    }
}
