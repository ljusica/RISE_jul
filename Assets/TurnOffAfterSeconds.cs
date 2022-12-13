using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfterSeconds : MonoBehaviour
{

    public float timerSeconds;

    void Update()
    {
        timerSeconds -= Time.deltaTime;

        if (timerSeconds <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
