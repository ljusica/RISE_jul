using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public class TrashLine : MonoBehaviour
{
    public GameObject bin1, bin2, bin3, trashPiece;
    public bool canRestart = true;

    float width, height;
    Vector3[] columns = new Vector3[7];
    Vector3[] rows = new Vector3[8];
    List<GameObject> trashPieces = new List<GameObject>();
    List<int> trashHeight = new List<int>();
    List<GameObject> trashMissed = new List<GameObject>();
    private float positionIndex = 3, gameSpeed = 1;
    private int movesMade, plasticCount, metalCount, cardboardCount, trashPlaced, score;
    private bool canMove = true;
    private bool isGameOver;

    public void Start()
    {
        FreshStart();
        width = Screen.width;
        height = Screen.height;
        for(int i = 1; i < columns.Length + 1; i++)
        {
            columns[i - 1] = Camera.main.ScreenToWorldPoint(
                new Vector3((width / 7) * i - width / 14, 0, 0));

            columns[i - 1] = new Vector3(columns[i - 1].x, 0, 0);
        }

        for(int i = 1; i < rows.Length + 1; i++)
        {
            rows[i - 1] = Camera.main.ScreenToWorldPoint(
                new Vector3(0, (height / 8) * i - height / 16, 0));

            rows[i - 1] = new Vector3(0, rows[i - 1].y, 0);
        }

        transform.position = columns[(int)positionIndex];

        instance.horizontal.performed += MoveTrashLine;

    }

    private void Update()
    {
        if (!canRestart)
        {
            if (trashPieces.Count == 0) SpawnTrash();
            if (isGameOver)
            {
                canRestart = true;
                for(int i = trashPieces.Count - 1; i >= 0; i--)
                {
                    GameObject trash = trashPieces[i];
                    trashPieces.RemoveAt(i);
                    Destroy(trash);
                }
                trashHeight.Clear();
            }
            if (movesMade == 2)
            {
                SpawnTrash();
                movesMade = 0;
            }

            if (canMove)
            {
                canMove = false;
                StartCoroutine(MoveTrashDown());
            }

            if (plasticCount == 3 || cardboardCount == 3 || metalCount == 3) isGameOver = true;
        }
    }

    private void SpawnTrash()
    {
        GameObject newTrash = 
            Instantiate
            (
                trashPiece, 
                new Vector3
                (
                    transform.position.x, 
                    rows[7].y, 
                    transform.position.z
                ),
                Quaternion.identity
            );

        trashPieces.Add(newTrash);
        trashHeight.Add(7);
    }

    private IEnumerator MoveTrashDown()
    {

        for (int i = trashPieces.Count - 1; i >= 0; i--)
        {
            trashPieces[i].transform.position = 
                new Vector3
                (
                    trashPieces[i].transform.position.x, 
                    rows[trashHeight[i]].y, 
                    trashPieces[i].transform.position.z
                );
            int rowNumber = 0;

            if(transform.position.x == columns[0].x)
                rowNumber = cardboardCount;
            else if (transform.position.x == columns[2].x)
                rowNumber = plasticCount;
            else if (transform.position.x == columns[4].x)
                rowNumber = metalCount;

            if (trashPieces[i].transform.position.y != rows[rowNumber].y)
                trashHeight[i]--;
            else
            {
                GameObject missedTrash = RemoveTrashFromList();
                if (missedTrash.transform.position.x == columns[0].x)
                    cardboardCount++;
                else if (missedTrash.transform.position.x == columns[2].x)
                    plasticCount++;
                else if (missedTrash.transform.position.x == columns[4].x)
                    metalCount++;
            }
        }
        yield return new WaitForSeconds(gameSpeed);
        movesMade++;
        canMove = true;
    }

    private void MoveTrashLine(InputAction.CallbackContext obj)
    {
        if (!canRestart)
        {
            positionIndex += obj.ReadValue<float>();
            positionIndex = Mathf.Clamp(positionIndex, 0, 6);
            transform.position = columns[(int)positionIndex];
            foreach(GameObject trash in trashPieces)
            {
                trash.transform.position = 
                    new Vector3
                    (
                        transform.position.x, 
                        trash.transform.position.y, 
                        trash.transform.position.z
                    );
            }
        }
    }

    public void CollectTrash()
    {
        GameObject trash = RemoveTrashFromList();
        Destroy(trash);
        trashPlaced++;
        score++;
        print(score);
        if (trashPlaced % 3 == 1)
        {
            gameSpeed = gameSpeed * 0.9f;
        }
    }

    public void MoveTrashToSide(TrashType value)
    {
        int rowIndex = 0;
        switch (value)
        {
            case TrashType.plastic: plasticCount++; rowIndex = plasticCount - 1; break;
            case TrashType.metal: metalCount++; rowIndex = metalCount - 1; break;
            case TrashType.cardboard: cardboardCount++; rowIndex = cardboardCount - 1; break;
        }

        GameObject trash = RemoveTrashFromList();
        trash.transform.position = 
            new Vector3
            (
                columns[(int)positionIndex - 1].x, 
                rows[rowIndex].y, 
                transform.position.z
            );
        trashPlaced++;
        trashMissed.Add(trash);
    }

    private GameObject RemoveTrashFromList()
    {
        GameObject trash = trashPieces[0];
        trashPieces.Remove(trash);
        trashHeight.RemoveAt(0);

        return trash;
    }

    private void FreshStart()
    {
        gameSpeed = 1;
        positionIndex = 3;
        movesMade = 0;
        plasticCount = 0;
        metalCount = 0;
        cardboardCount = 0;
        trashPlaced = 0;
        score = 0;
        canMove = true;
        isGameOver = false;

        for(int i = trashMissed.Count - 1; i >= 0; i--)
        {
            GameObject trash = trashMissed[i];
            trashMissed.Remove(trash);
            Destroy(trash);
        }
    }
}
