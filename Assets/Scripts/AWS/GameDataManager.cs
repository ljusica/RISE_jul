using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance { get; private set; }

    public PlayerScoreData playerScoreData = new PlayerScoreData();

    private PostToWebServer postToWebServer;

    private void Start()
    {
        if(instance == null) instance = this;
        else Destroy(instance);

        postToWebServer = GetComponent<PostToWebServer>();
    }

    public void PostScore()
    {
        postToWebServer.PostScore();
    }
}
