using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static PlayerController;
using static InputManager;

public class TrashGameHandler : MonoBehaviour
{
    [SerializeField] Camera trashCamera;
    [SerializeField] Camera mainCamera;

    [SerializeField] TrashLine trashLine;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text[] objectiveText;

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
        objectiveHandler.AddMiniGamesPlayedProgress(this.name);
        trashLine.FreshStart();
        trashLine.canRestart = false;
        playerController.canMove = false;

        foreach (var objectiveText in objectiveText)
        {
            objectiveText.gameObject.SetActive(false);
        }

        scoreText.gameObject.SetActive(true);

        trashCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }

    private void StopGame(InputAction.CallbackContext context)
    {
        trashLine.canRestart = true;
        playerController.canMove = true;

        foreach (var objectiveText in objectiveText)
        {
            objectiveText.gameObject.SetActive(true);
        }

        scoreText.gameObject.SetActive(false);

        trashCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

}
