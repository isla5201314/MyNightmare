using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public Text ScoreText;
    public int bestscore;
    // Start is called before the first frame update
    void Start()
    {
        bestscore = PlayerPrefs.GetInt("BestScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        bestscore = PlayerPrefs.GetInt("BestScore", 0);
        ScoreText.text = "BestScore:" + bestscore;
    }
}
