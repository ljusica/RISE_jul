using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class NamePlate : MonoBehaviour
{
    private TMP_Text namePlateText;
    private Dialogue dialogue;

    private void Start()
    {
        namePlateText = GetComponent<TMP_Text>();
        dialogue = GetComponentInParent<Dialogue>();

        string[] firstName = dialogue.name.Split(' ');

        namePlateText.text = firstName[0];

        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = Vector3.zero;
    }

    public void ShowNamePlate()
    {
        namePlateText.gameObject.SetActive(true);

        transform.DOScale(0.75f, 0.5f).SetEase(Ease.OutBounce);
        //transform.DOScale(0.75f, 0.3f).OnComplete(()=> transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 0.1f));
    }

    public void HideNamePlate()
    {
        transform.DOScale(0f, 0.3f).OnComplete(() => namePlateText.gameObject.SetActive(false));
    }
}
