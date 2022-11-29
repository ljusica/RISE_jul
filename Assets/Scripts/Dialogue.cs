using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;

    public string name;
    public string title;
    public string description;

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
            dialogueBox.gameObject.SetActive(false);
        }
    }

    private void Talk()
    {
        for (int i = 0; i < dialogueBox.transform.childCount; i++)
        {
            switch (i)
            {
                case 1:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = name;
                    break;

                case 2:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = title;
                    break;

                case 3:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = description;
                    break;

                default:
                    Debug.Log("Whoopsiedoodle");
                    break;
            }

        }

        dialogueBox.gameObject.SetActive(true);

    }
}