using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private int movesMade;
    private bool canMove = true;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        for(int i = 1; i < columns.Length + 1; i++)
        {
            columns[i - 1] = Camera.main.ScreenToWorldPoint
                (
                    new Vector3
                    (
                        (width / 7) * i - width / 14, 0, 0
                    )
                );

            columns[i - 1] = new Vector3(columns[i - 1].x, 0, 0);
        }

        for(int i = 1; i < rows.Length + 1; i++)
        {
            rows[i - 1] = Camera.main.ScreenToWorldPoint
                (
                    new Vector3
                    (
                        0, (height / 8) * i - height / 16, 0
                    )
                );

            rows[i - 1] = new Vector3(0, rows[i - 1].y, 0);
        }

        transform.position = columns[(int)positionIndex];

        instance.horizontal.performed += MoveTrashLine;

        SpawnTrash();
    }

    private void Update()
    {
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

        for (int i = 0; i < trashPieces.Count; i++)
        {
            trashPieces[i].transform.position = 
                new Vector3
                (
                    trashPieces[i].transform.position.x, 
                    rows[trashHeight[i]].y, 
                    trashPieces[i].transform.position.z
                );

            trashHeight[i]--;
        }
        yield return new WaitForSeconds(1);
        movesMade++;
        canMove = true;
    }

    private void MoveTrashLine(InputAction.CallbackContext obj)
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

    public void CollectTrash()
    {
        GameObject trash = RemoveTrashFromList();
        Destroy(trash);
    }

    public void MoveTrashToSide()
    {
        GameObject trash = RemoveTrashFromList();
        trash.transform.position = 
            new Vector3
            (
                columns[(int)positionIndex - 1].x, 
                rows[0].y, 
                transform.position.z
            );
    }

    private GameObject RemoveTrashFromList()
    {
        GameObject trash = trashPieces[0];
        trashPieces.Remove(trash);
        trashHeight.RemoveAt(0);

        return trash;
    }
}
