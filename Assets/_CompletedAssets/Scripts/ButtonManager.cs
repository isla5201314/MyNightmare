using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject GOVER;
    public Button Restart;
    public Button Returnback;
    public Button Stop;
    public GameObject targetObject; // 目标游戏对象，由你来初始化设置  
    private Image targetImage; // 目标游戏对象的 Image 组件  
    
    public Image menu;
    public Button backmenu;
    public Button rstar;

    // Start is called before the first frame update  
    void Start()
    {
        Restart.onClick.AddListener(restart);
        Returnback.onClick.AddListener(returnback);
        Stop.onClick.AddListener(stop);

        backmenu.onClick.AddListener(returnback);
        rstar.onClick.AddListener(restart);
        // 获取目标游戏对象的 Image 组件  
        targetImage = targetObject.GetComponent<Image>();

        // 初始时确保 GOVER 是非激活状态  
        GOVER.SetActive(false);
    }

    void stop()
    {
        
            if (Time.timeScale == 1f) // 如果游戏正在运行  
            {
                Time.timeScale = 0f; // 暂停游戏  
                menu.gameObject.SetActive(true);

        }
            else
            {
                Time.timeScale = 1f; // 恢复游戏  
                menu.gameObject.SetActive(false);
        }

    }

    void restart()
    {
        SceneManager.LoadScene("main");
        Time.timeScale = 1f;
    }

    void returnback()
    {
        SceneManager.LoadScene("start");
        Time.timeScale = 1f;
    }

    // Update is called once per frame  
    void Update()
    {
        // 检查目标游戏对象的 Image 组件的 alpha 值是否不为0  
        if (targetImage.color.a != 0)
        {
            // 如果 alpha 值不为0，激活 GOVER  
            GOVER.SetActive(true);
        }
        else
        {
            // 如果 alpha 值为0，确保 GOVER 是非激活状态  
            GOVER.SetActive(false);
        }
    }
}