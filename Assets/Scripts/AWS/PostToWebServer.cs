using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostToWebServer : MonoBehaviour
{
    public void PostScore()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        string json = JsonUtility.ToJson(GameDataManager.instance.playerScoreData);
        print(json);
        

        using (UnityWebRequest www = UnityWebRequest.Put("http://192.168.1.79:8080/create", json))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
