using TMPro;
using UnityEngine;
using static PlayerController;

public class HumanBenchMarkHandler : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    private PlayerController playerController;

    [SerializeField] TMP_Text[] objectiveTexts;

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

    private void TurnOffOnObjectiveTexts()
    {
        bool mode = !objectiveTexts[0].gameObject.activeInHierarchy;
        foreach (var text in objectiveTexts)
        {
            text.gameObject.SetActive(mode);
        }
    }

    private void startGame()
    {
        TurnOffOnObjectiveTexts();
        gameCanvas.SetActive(true);
        playerController.canMove = false;
    }

    public void stopGame()
    {
        TurnOffOnObjectiveTexts();
        gameCanvas.SetActive(false);
        playerController.canMove = true;
    }
}
