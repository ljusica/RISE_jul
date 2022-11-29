using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class NamePlate : MonoBehaviour
{
    [SerializeField] TMP_Text namePlateText;
    private Dialogue dialogue;

    private void Start()
    {
        namePlateText = GetComponentInChildren<TMP_Text>();
        dialogue = GetComponent<Dialogue>();

        namePlateText.text = dialogue.name;
    }

    public void ShowNamePlate()
    {
        namePlateText.gameObject.SetActive(true);
    }

    public void HideNamePlate()
    {
        namePlateText.gameObject.SetActive(false);
    }

}
