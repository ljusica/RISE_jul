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
    private Objectives objectiveHandler;

    private void Start()
    { 
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        robotController = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotController>();
        objectiveHandler = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objectives>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController.interaction += StartGame;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController.interaction -= StartGame;
    }

    private void StartGame()
    {
        objectiveHandler.AddMiniGamesPlayedProgress();
        playerController.canMove = false;
        robotController.canRobotMove = true;
        CameraCheck();

        roboCamera.gameObject.SetActive(true);
        officeVirtualCamera.Priority = (CameraPriorityLevel.priorityLevel - 10);
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
        robotController.canRobotMove = false;
        roboCamera.gameObject.SetActive(false);
        CameraPriorityLevel.priorityLevel++;
        officeVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        CameraCheck();

    }
}
