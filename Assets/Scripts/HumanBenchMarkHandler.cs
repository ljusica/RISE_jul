using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class HumanBenchMarkHandler : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        gameCanvas.SetActive(true);
        playerController.canMove = false;
    }

    public void stopGame()
    {
        gameCanvas.SetActive(false);
        playerController.canMove = true;
    }
}
