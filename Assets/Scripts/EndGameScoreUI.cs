using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI thisEndGameScoreTM;
    ScoreKeeper thisScoreKeeper;
    void Awake()
    {
        thisScoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        thisEndGameScoreTM.text = "Final Score: " + thisScoreKeeper.GetScore();
    }
}
