using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class TrashGameHandler : MonoBehaviour
{
    [SerializeField] Camera trashCamera;
    [SerializeField] Camera mainCamera;

    [SerializeField] TrashLine trashLine;

    private PlayerController playerController;

    void Start()
    {
        trashCamera.gameObject.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        trashLine.FreshStart();
        trashLine.canRestart = false;

        playerController.canMove = false;
        trashCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }

    private void StopGame()
    {
        trashLine.canRestart = true;
        playerController.canMove = true;
        trashCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }

}
