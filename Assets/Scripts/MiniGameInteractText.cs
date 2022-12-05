using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameInteractText : MonoBehaviour
{
    [SerializeField] TMP_Text text;    
    [SerializeField] string textString;

    private void Start()
    {
        text.text = textString;
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
