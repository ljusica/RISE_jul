using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraPriorityLevel;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineVirtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        CinemachineVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        CameraPriorityLevel.priorityLevel++;
    }

    private void OnTriggerExit(Collider other)
    {
        CinemachineVirtualCamera.Priority = CameraPriorityLevel.priorityLevel - 10;
    }
}
