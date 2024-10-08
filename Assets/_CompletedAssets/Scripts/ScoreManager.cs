using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score=0;
    public Text ScoreText;
    int BestScore=0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        // ScoreText.text = "得分:" + Score;
    }

    void UpdateScore()
    {
        // Debug.Log("更新得分文本");
        ScoreText.text = "Score:" + Score;
    }

    public void AddFun()
    {
       int value = 10;
    //Debug.Log("加分");
    Score += value;
        UpdateScore();
    }


    // Update is called once per frame
    void Update()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore",BestScore );
        }
    }
}
