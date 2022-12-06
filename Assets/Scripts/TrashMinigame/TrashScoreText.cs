using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashScoreText : MonoBehaviour
{
    public TrashLine trashLine;

    private TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        scoreText.text = string.Format("Score: {0}", trashLine.score.ToString());
    }
}
