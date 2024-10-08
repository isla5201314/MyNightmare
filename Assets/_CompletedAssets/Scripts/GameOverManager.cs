using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    Animator anim;

    // Start is called before the first frame update  
    void Start()
    {
        anim = GetComponent<Animator>();

        // 寻找带有"Player"标签的游戏对象  
        GameObject playerObject = GameObject.FindWithTag("Player");

        // 如果找到了带有"Player"标签的游戏对象，并且该对象有附加的PlayerHealth组件  
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // 在这里使用playerHealth  
        }
        else
        {
            Debug.LogError("在带有'Player'标签的游戏对象上没有找到PlayerHealth组件");
        }
    }

    // Update is called once per frame  
    void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}