using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;

    public string riseName;
    public string title;
    public string description;

    [SerializeField] NamePlate namePlate;

    private AudioSource chatter;

    private void Start()
    {
        chatter = GetComponent<AudioSource>();
        namePlate = GetComponentInChildren<NamePlate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            namePlate.ShowNamePlate();
            PlayerController.interaction += Talk;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            namePlate.HideNamePlate();
            PlayerController.interaction -= Talk;
            dialogueBox.gameObject.SetActive(false);
        }
    }

    private void Talk()
    {
        chatter.Play();

        for (int i = 0; i < dialogueBox.transform.childCount; i++)
        {
            switch (i)
            {
                case 1:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = riseName;
                    break;

                case 2:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = title;
                    break;

                case 3:
                    dialogueBox.transform.GetChild(i).GetComponent<TMP_Text>().text = description;
                    break;

                default:
                    break;
            }

        }
        Objectives.Instance.CheckObjective(riseName);
        dialogueBox.gameObject.SetActive(true);

    }
}
