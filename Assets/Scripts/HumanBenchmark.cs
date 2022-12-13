using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using TMPro;

public class HumanBenchmark : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]  bool gameStarted;
    [SerializeField] float timeSinceGameStart;
    [SerializeField] float timeGreen;
    [SerializeField] Image arduinoImage;
    [SerializeField] Image SpaceButtonDownImage;
    private int state = 1;
    private float timeTillGreen;

    private Objectives objectiveHandler;
    private bool hasBeenPlayed = false;

    [Header("TMP Fields")]
    [SerializeField] TMP_Text startText;
    [SerializeField] TMP_Text stateText;
    [SerializeField] TMP_Text subText;


    [Header("Audio Clips")]
    [SerializeField] AudioSource goodSound;
    [SerializeField] AudioSource badSound;

    [Header("Sprites")]
    [SerializeField] Sprite greenSprite;
    [SerializeField] Sprite blueSprite;
    [SerializeField] Sprite redSprite;



    private void Start()
    {
        objectiveHandler = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objectives>();

        arduinoImage.sprite = greenSprite;
    }

    void Update()
    {
        if (gameStarted)
            timeSinceGameStart += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceButtonDownImage.gameObject.SetActive(true);
            Click();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
            SpaceButtonDownImage.gameObject.SetActive(false);    

        Game();

    }

    public void Click()
    {
        if (!hasBeenPlayed)
        {
            objectiveHandler.AddMiniGamesPlayedProgress(this.name);
            hasBeenPlayed = true;
        }

        switch (state)
        {
            case 1:
                startText.gameObject.SetActive(false);
                stateText.gameObject.SetActive(true);
                subText.gameObject.SetActive(true);
                subText.text = "";
                arduinoImage.sprite = redSprite;
                timeSinceGameStart = 0;
                timeTillGreen = Random.Range(3f, 4.5f);
                gameStarted = true;
                state = 2;
                timeSinceGameStart = 0;
                break;
            case 2:
                gameStarted = false;
                arduinoImage.sprite = blueSprite;
                timeSinceGameStart = 0;
                stateText.text = "Too soon!";
                subText.text = "Click to keep going";
                badSound.Play();
                state = 1;
                break;
            case 3:
                gameStarted = false;
                timeGreen *= 1000;
                Mathf.Round(timeGreen);
                string[] timeString = new string[2];
                if (timeGreen.ToString().Contains("."))
                    timeString = timeGreen.ToString().Split(".");
                else if(timeGreen.ToString().Contains(","))
                    timeString = timeGreen.ToString().Split(",");
                stateText.text = timeString[0] + " ms";
                subText.text = "Click to keep going";
                timeGreen = 0;
                goodSound.Play();                
                state = 1;
                GameDataManager.instance.playerScoreData.gameName = "Human Benchmark";
                GameDataManager.instance.playerScoreData.score = stateText.text;
                GameDataManager.instance.PostScore();
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
            arduinoImage.sprite = redSprite;
            state = 2;
            stateText.text = "Wait for green";
        }
        else
        {
            timeGreen += Time.deltaTime;
            arduinoImage.sprite = greenSprite;
            state = 3;
            stateText.text = "Click!";
        }
    }

}
