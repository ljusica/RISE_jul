using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;
using static CameraPriorityLevel;

public class RoboGameHandler : MonoBehaviour
{
    [SerializeField] Camera roboCamera;
    [SerializeField] CinemachineVirtualCamera roboVirtualCamera;
    [SerializeField] CinemachineVirtualCamera officeVirtualCamera;

    [SerializeField] GameObject[] cameras;

    private PlayerController playerController;
    private RobotController robotController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        robotController = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController.interaction += startGame;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController.interaction -= startGame;
    }

    private void startGame()
    {
        playerController.canMove = false;
        robotController.canRobotMove = false;
        CameraCheck();

        roboCamera.gameObject.SetActive(true);
        roboVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        CameraPriorityLevel.priorityLevel++;
    }

    private void CameraCheck()
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(!playerController.canMove);
            Debug.Log(!playerController.canMove);
        }
    }

    public void GoBackToOffice()
    {
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1f);
        playerController.canMove = true;
        robotController.canRobotMove = true;
        CameraCheck();

        officeVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        CameraPriorityLevel.priorityLevel++;
        roboCamera.gameObject.SetActive(false);
    }
}
