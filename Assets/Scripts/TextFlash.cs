using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlash : MonoBehaviour
{
    public TMP_Text thisText;
    private IEnumerator TextFlashEnum()
    {
        while (true)
        {
            Color color = thisText.color;
            color.a = 0f;
            thisText.color = color;
            yield return new WaitForSeconds(1f);
            color.a = 1f;
            thisText.color = color;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TextFlashEnum());
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        Color color = thisText.color;
        color.a = 0f;
        thisText.color = color;
    }

}
