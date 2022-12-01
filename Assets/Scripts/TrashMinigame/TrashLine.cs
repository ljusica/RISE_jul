using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static InputManager;

public class TrashLine : MonoBehaviour
{
    public GameObject bin1, bin2, bin3, trashPiece;

    float width, height;
    Vector3[] columns = new Vector3[7];
    Vector3[] rows = new Vector3[8];
    List<GameObject> trashPieces = new List<GameObject>();
    List<int> trashHeight = new List<int>();
    private float positionIndex = 3;
    private bool canSpawn = true;
    private bool canMove = true;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        for(int i = 1; i < columns.Length + 1; i++)
        {
            columns[i - 1] = Camera.main.ScreenToWorldPoint(new Vector3((width / 7) * i - width / 14, 0, 0));
            columns[i - 1] = new Vector3(columns[i - 1].x, 0, 0);
        }

        for(int i = 1; i < rows.Length + 1; i++)
        {
            rows[i - 1] = Camera.main.ScreenToWorldPoint(new Vector3(0, (height / 8) * i - height / 16, 0));
            rows[i - 1] = new Vector3(0, rows[i - 1].y, 0);
        }

        transform.position = columns[(int)positionIndex];

        instance.horizontal.performed += MoveTrashLine;
    }

    private async void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            await SpawnTrash();
        }

        if (canMove)
        {
            canMove = false;
            await MoveTrashDown();
        }
    }

    private async Task SpawnTrash()
    {
        float endTime = Time.time + 2;
        while(Time.time < endTime)
        {
            await Task.Yield();
        }

        GameObject newTrash = Instantiate(trashPiece, new Vector3(transform.position.x, rows[7].y, transform.position.z), Quaternion.identity);
        trashPieces.Add(newTrash);
        trashHeight.Add(7);
        canSpawn = true;
    }

    private async Task MoveTrashDown()
    {
        float endTime = Time.time + 1;
        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        for (int i = 0; i < trashPieces.Count; i++)
        {
            trashPieces[i].transform.position = new Vector3(trashPieces[i].transform.position.x, rows[trashHeight[i]].y, trashPieces[i].transform.position.z);
            trashHeight[i]--;
        }

        canMove = true;
    }

    private void MoveTrashLine(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        positionIndex += obj.ReadValue<float>();
        positionIndex = Mathf.Clamp(positionIndex, 0, 6);
        transform.position = columns[(int)positionIndex];
        foreach(GameObject trash in trashPieces)
        {
            trash.transform.position = new Vector3(transform.position.x, trash.transform.position.y, trash.transform.position.z);
        }
    }
}
