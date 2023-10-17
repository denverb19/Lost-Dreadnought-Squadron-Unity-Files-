using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private int currentScore = 0;
    static ScoreKeeper instance;
    public int GetScore()
    {
        return currentScore;
    }
    public void IncreaseScore(int increaseValue)
    {
        currentScore += increaseValue;
        Mathf.Clamp(currentScore, 0, 999999999);//Dont want more than 9 digits in the score UI
    }
    public void ResetScore()
    {
        currentScore = 0;
    }
    private void Awake() 
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
   
    }
}
