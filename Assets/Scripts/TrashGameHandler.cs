using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;
using static InputManager;
using UnityEngine.InputSystem;

public class TrashGameHandler : MonoBehaviour
{
    [SerializeField] Camera trashCamera;
    [SerializeField] Camera mainCamera;

    [SerializeField] TrashLine trashLine;

    private PlayerController playerController;
    private Objectives objectiveHandler;

    void Start()
    {
        trashCamera.gameObject.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        objectiveHandler = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objectives>();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerController.interaction += StartGame;
        instance.inputControls.Actions.Escape.performed += StopGame;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController.interaction -= StartGame;
        instance.inputControls.Actions.Escape.performed -= StopGame;
    }

    private void StartGame()
    {
        objectiveHandler.AddMiniGamesPlayedProgress();
        trashLine.FreshStart();
        trashLine.canRestart = false;

        playerController.canMove = false;
        trashCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }

    private void StopGame(InputAction.CallbackContext context)
    {
        trashLine.canRestart = true;
        playerController.canMove = true;
        trashCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

}
