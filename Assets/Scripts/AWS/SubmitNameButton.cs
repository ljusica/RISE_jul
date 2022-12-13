using UnityEngine;
using TMPro;
using static GameDataManager;

public class SubmitNameButton : MonoBehaviour
{
    public TMP_Text nameInputField;
    public PlayerController playerController;

    public void SubmitName()
    {
        
        instance.playerScoreData.userName = nameInputField.text.TrimEnd('\u200b');
       
        playerController.canMove = true;
    }

    private void Start()
    {
        playerController.canMove = false;
    }
}
