using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private GameObject dialogueBox;

    private TMP_Text nameText;
    private TMP_Text titleText;
    private TMP_Text descriptionText;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.interaction += Talk;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.interaction -= Talk;
        }
    }

    private void Talk()
    {
        Debug.Log("Talk!!");
    }
}
