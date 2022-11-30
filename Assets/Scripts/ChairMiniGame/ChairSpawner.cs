using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSpawner : MonoBehaviour
{
    public bool isGameOver;
    public GameObject chairPrefab;

    private Vector3 heightPosition;
    private float time = 2;
    private float edgeValue = 6.825f;

    void Start()
    {
        heightPosition = new Vector3(0, 9.17f, 1.679f);

        GameStarted();
    }

    private void GameStarted()
    {
        for (int i = 0; i < 10; i++)
        {
            time = 2 + (i * 2);
            StartCoroutine(nameof(SpawnChair));
        }
    }

    private IEnumerator SpawnChair()
    {
        yield return new WaitForSeconds(time);
        Vector3 spawnPosition = new Vector3(Random.Range(-edgeValue, edgeValue), 0, 0) + heightPosition;
        Instantiate(chairPrefab, spawnPosition, Quaternion.identity);
    }
}
