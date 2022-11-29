using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using TMPro;

public class HumanBenchmark : MonoBehaviour
{
    [SerializeField] bool gameStarted;
    [SerializeField] bool isGreen;
    [SerializeField] float timeSinceGameStart;
    [SerializeField] float timeGreen;

    [SerializeField] TMP_Text startText;
    [SerializeField] TMP_Text stateText;
    [SerializeField] TMP_Text subText;

    private int state = 1;
    private float timeTillGreen;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.blue;
    }

    void Update()
    {
        if (gameStarted)
            timeSinceGameStart += Time.deltaTime;


        Game();
    }

    public void Click()
    {

        switch (state)
        {
            case 1:
                startText.gameObject.SetActive(false);
                stateText.gameObject.SetActive(true);
                subText.gameObject.SetActive(true);
                subText.text = "";
                image.color = Color.red;
                isGreen = false;
                timeSinceGameStart = 0;
                timeTillGreen = Random.Range(3f, 4.5f);
                gameStarted = true;
                state = 2;
                timeSinceGameStart = 0;
                break;
            case 2:
                gameStarted = false;
                image.color = Color.blue;
                timeSinceGameStart = 0;
                stateText.text = "Too soon!";
                subText.text = "Click to keep going";
                state = 1;
                break;
            case 3:
                gameStarted = false;
                timeGreen *= 1000;
                Mathf.Round(timeGreen);
                string[] timeString = timeGreen.ToString().Split(".");
                stateText.text = timeString[0] + " ms";
                subText.text = "Click to keep going";
                timeGreen = 0;
                state = 1;
                break;
            default:
                break;
        }

    }

    private void Game()
    {
        if (!gameStarted) return;


        if (timeSinceGameStart < timeTillGreen)
        {
            isGreen = false;
            image.color = Color.red;
            state = 2;
            stateText.text = "Wait for green";
        }
        else
        {
            timeGreen += Time.deltaTime;
            isGreen = true;
            image.color = Color.green;
            state = 3;
            stateText.text = "Click!";
        }
    }

}