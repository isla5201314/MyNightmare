using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGames : MonoBehaviour
{
    public Button EG;
    
    // Start is called before the first frame update
    void Start()
    {
        EG.onClick.AddListener(games);
    }

    public void games()
    {
        //Debug.Log("退出游戏");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
