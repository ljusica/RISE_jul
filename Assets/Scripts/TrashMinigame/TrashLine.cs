using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public class TrashLine : MonoBehaviour
{
    public GameObject bin1, bin2, bin3;

    float width;
    Vector3[] positions = new Vector3[7];
    private float positionIndex = 3;

    void Start()
    {
        width = Screen.width;
        for(int i = 1; i < positions.Length + 1; i++)
        {
            positions[i - 1] = Camera.main.ScreenToWorldPoint(new Vector3((width / 7) * i - width / 14, 0, 0));
            positions[i - 1] = new Vector3(positions[i - 1].x, 0, 0);
        }

        transform.position = positions[(int)positionIndex];

        instance.horizontal.performed += MoveTrashLine;
    }

    private void MoveTrashLine(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        positionIndex += obj.ReadValue<float>();
        positionIndex = Mathf.Clamp(positionIndex, 0, 6);
        transform.position = positions[(int)positionIndex];
    }
}
