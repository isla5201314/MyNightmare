using System.Collections;
using System.Collections.Generic;
//using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    public string targetTag = "Player"; // 要寻找的目标Tag  
    public GameObject[] playerObjects; // 存储所有带有目标Tag的游戏对象的数组
    public int index = 0;

    //Start is called before the first frame update
    void Awake()
    {
       
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        playerObjects = GameObject.FindGameObjectsWithTag(targetTag);
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.activeInHierarchy)
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(player.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
        //else
        //{
        //        for (index = 0; index < 3; index++)
        //        {
        //            player = playerObjects[index];
        //            if (player.activeInHierarchy)
        //            {
        //                break;//找到活跃的玩家则跳出寻找;
        //            }

        //        }
            
        //    //player = playerObjects[index];
        //}
    }
}
