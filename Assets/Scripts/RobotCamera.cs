using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCamera : MonoBehaviour
{
    private GameObject objectToFollow;

    void Start()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Robot");
    }

    void Update()
    {
        
    }
}
