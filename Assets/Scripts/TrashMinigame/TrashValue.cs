using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType { plastic, metal, cardboard };

public class TrashValue : MonoBehaviour
{
    public TrashType value;

    void Start()
    {
        int trashTypeIndex = Random.Range(0, 3);

        switch (trashTypeIndex)
        {
            case 0: value = TrashType.plastic; break;
            case 1: value = TrashType.metal; break;
            case 2: value = TrashType.cardboard; break;
        }
    }
}
