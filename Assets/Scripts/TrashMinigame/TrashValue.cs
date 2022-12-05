using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType { plastic, metal, cardboard };

public class TrashValue : MonoBehaviour
{
    public TrashType value;
    public GameObject metal, plastic, cardboard;

    void Start()
    {
        int trashTypeIndex = Random.Range(0, 3);
        Quaternion metalRotation = Quaternion.Euler(270, 0, 0);
        Quaternion plasticRotation = Quaternion.Euler(270, 180, 0);


        switch (trashTypeIndex)
        {
            case 0: value = TrashType.plastic; break;
            case 1: value = TrashType.metal; break;
            case 2: value = TrashType.cardboard; break;
        }

        switch (value)
        {
            case TrashType.plastic:
                Instantiate(plastic, transform.position + new Vector3(0, -0.6f, 0), plasticRotation, transform);
                break;
            case TrashType.metal:
                Instantiate(metal, transform.position + new Vector3(0, -0.425f, 0), metalRotation, transform);
                break;
            case TrashType.cardboard:
                Instantiate(cardboard, transform.position, metalRotation, transform);
                break;
        }
    }
}
