using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;
using static CameraPriorityLevel;
using static InputManager;
using UnityEngine.InputSystem;

public class RoboGameHandler : MonoBehaviour
{
    [SerializeField] Camera roboCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera roboVirtualCamera;
    [SerializeField] CinemachineVirtualCamera officeVirtualCamera;

    [SerializeField] GameObject[] cameras;

    public float startTime;

    private PlayerController playerController;
    private RobotController robotController;
    private RobotCollissions robotCollissions;
    private Objectives objectiveHandler;

    private Vector3 cameraStartRot;

    private void Start()
    { 
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        robotController = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotController>();
        robotCollissions = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotCollissions>();
        objectiveHandler = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objectives>();

        cameraStartRot = roboVirtualCamera.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController.interaction += StartGame;
        instance.inputControls.Actions.Escape.performed += GoBackToOffice;
        
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController.interaction -= StartGame;
        instance.inputControls.Actions.Escape.performed -= GoBackToOffice;
    }

    private void StartGame()
    {
        roboCamera.transform.position = cameraStartRot;
        robotCollissions.Respawn();
        mainCamera.gameObject.SetActive(false);
        objectiveHandler.AddMiniGamesPlayedProgress(this.name);
        playerController.canMove = false;
        robotController.canRobotMove = true;
        CameraCheck();

        roboCamera.gameObject.SetActive(true);
        officeVirtualCamera.Priority = (CameraPriorityLevel.priorityLevel - 10);
        roboVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        CameraPriorityLevel.priorityLevel++;

        startTime = Time.time;
    }

    private void CameraCheck()
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(!playerController.canMove);
        }
    }

    public void GoBackToOffice()
    {
        StartCoroutine(StopGame());
    }

    public void GoBackToOffice(InputAction.CallbackContext context)
    {
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1f);
        playerController.canMove = true;
        robotController.canRobotMove = false;
        CameraCheck();
        CameraPriorityLevel.priorityLevel++;
        officeVirtualCamera.Priority = CameraPriorityLevel.priorityLevel;
        roboCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

    }
}
