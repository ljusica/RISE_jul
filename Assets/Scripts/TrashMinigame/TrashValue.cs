using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType { plastic, metal, cardboard };

public class TrashValue : MonoBehaviour
{
    public TrashType value;
    public Material plastic, metal, cardboard;

    void Start()
    {
        int trashTypeIndex = Random.Range(0, 3);

        switch (trashTypeIndex)
        {
            case 0: value = TrashType.plastic; break;
            case 1: value = TrashType.metal; break;
            case 2: value = TrashType.cardboard; break;
        }

        switch (value)
        {
            case TrashType.plastic:
                GetComponent<MeshRenderer>().material = plastic;
                break;
            case TrashType.metal:
                GetComponent<MeshRenderer>().material = metal;
                break;
            case TrashType.cardboard:
                GetComponent<MeshRenderer>().material = cardboard;
                break;
        }
    }
}
