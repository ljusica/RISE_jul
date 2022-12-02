using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollection : MonoBehaviour
{
    public TrashLine trashManager;

    private void OnTriggerEnter(Collider other)
    {
        trashManager.CollectTrash();
    }
}
