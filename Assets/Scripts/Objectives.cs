using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;

public class Objectives : MonoBehaviour
{
    public int talkToObjective;
    public int miniGamesObjective;
    public int objectiveProgress = 0;
    public int miniGamesPlayed = 0;

    public List<string> npcNames = new List<string>();
    public List<string> miniGames = new List<string>();
    [SerializeField] Transform npcParent;
    [SerializeField] Transform miniGamesParent;
    [SerializeField] TMP_Text talkToObjectiveText;
    [SerializeField] TMP_Text miniGamesPlayedText;

    private static Objectives instance;
    public static Objectives Instance { get { return instance; } }

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        talkToObjective = npcParent.childCount;
        miniGamesObjective = miniGamesParent.childCount;
    }

    public void CheckObjective(string name)
    {
        if (npcNames.Contains(name)) return;
    
        npcNames.Add(name);
        objectiveProgress++;
        talkToObjectiveText.text = "Talk to your colleagues: " + objectiveProgress + " / " + talkToObjective;
        if (objectiveProgress >= talkToObjective)
            Debug.Log("You won!");

        return;

    }

    public void AddMiniGamesPlayedProgress(string gameName)
    {
        if (miniGames.Contains(gameName)) return;

        miniGames.Add(gameName);

        miniGamesPlayed++;
        miniGamesPlayedText.text = "Minigames Played: " + miniGamesPlayed + " / " + miniGamesObjective;
        Debug.Log(miniGamesPlayed + "/" + miniGamesObjective);
    }
}
