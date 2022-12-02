using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollection : MonoBehaviour
{
    public TrashLine trashManager;
    public TrashType value;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TrashValue>().value == value)
        {
            trashManager.CollectTrash();
        }
        else
        {
            trashManager.MoveTrashToSide(value);
        }
    }
}
