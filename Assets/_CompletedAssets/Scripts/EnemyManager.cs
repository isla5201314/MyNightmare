using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public GameObject[] enemies; // 怪物数组，用于存储三种不同的怪物    
    public float spawnTime = 2f;
    public float spawntime = 3f;
    public Transform[] spawnPoints; // 怪物生成位置数组    
    public int level = 0; // 当前关卡数，初始为0    
    public bool IsOver = false;

    public GameObject[] Enemy0;
    public int enemyCount;

    private int enemiesSpawned = 0; // 记录已生成的怪物数量  
    private int[] spawnCounts; // 存储每个等级下各种怪物的生成数量  

    bool isPlayerFound = false;

    public bool IsCreat = true;//检测怪物是否生成过
    public bool IsCreated = false;//检测怪物是否已经开始生成


    public Text levelText; // 用于显示当前关卡的文本  
    public Text enemiesRemainingText; // 用于显示剩余怪物数量的文本
    private bool allEnemiesDestroyed = true; // 标记所有敌人是否已被消灭
    public bool Isstart=false;

    bool isdoing = false;
    bool isdoing1 = false;

    void Start()
    {
        StartCoroutine(FindPlayer());

    }

    void Update()
    {
        Enemy0 = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = Enemy0.Length;
        if (allEnemiesDestroyed && !IsCreat && Isstart && !IsCreated && !isdoing) // 如果所有敌人都被销毁并且当前没有生成新的敌人  
        {
            //Debug.Log("已执行0");
            isdoing = true;
            StartCoroutine(SpawnEnemiesForLevel());
        }
        else if (allEnemiesDestroyed && !IsCreat && Isstart && IsCreated && !isdoing1)
        {
            isdoing1 = true;
            StartCoroutine(NextLevel()); // 开始下一关  
        }
        if (IsCreated)
        {
            UpdateLevelUI(); // 更新关卡UI显示  
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0  && !IsCreat)
        {
            allEnemiesDestroyed = true;
        }
        else
        {
            allEnemiesDestroyed = false;
        }
    }

    IEnumerator SpawnEnemiesForLevel()
    {
        //Debug.Log("已执行");
        SetSpawnCountsBasedOnLevel(); // 根据等级设置怪物的生成数量  

        // 生成所有怪物  
        while (spawnCounts.Sum() > 0)
        {
            Spawn();
            yield return new WaitForSeconds(spawntime); // 等待一段时间再生成下一个敌人  
        }
        isdoing1 = false;
        IsCreat = false; // 所有敌人都已生成，现在等待它们被销毁  
    }

    IEnumerator NextLevel()//下一关函数
    {
        yield return new WaitForSeconds(2f); // 等待2秒  
        level++; // 关卡数加1  
        IsCreat = false; // 允许开始生成下一波的怪物  
        StartCoroutine(SpawnEnemiesForLevel()); // 开始生成下一关的敌人
    }

        void UpdateLevelUI()//更新UI
    {
        levelText.text = "当前第 " + level +"波怪..."; // 显示当前关卡数  
        enemiesRemainingText.text = "剩余怪物数量：" + enemyCount; // 显示剩余怪物数量  
    }

    IEnumerator FindPlayer()//寻找玩家
    {
        yield return new WaitForSeconds(0.2f);
        while (!isPlayerFound)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<PlayerHealth>();
                isPlayerFound = true;
            }
            else
            {
                Debug.Log("尚未找到带有'Player'标签的游戏对象，继续搜索...");
            }
            yield return new WaitForSeconds(1f); // 等待一秒后再次搜索      
        }
        
    }

    void Spawn()//根据情况生成怪物，调用一次即生成全部再跳出
    {
        if (playerHealth == null || playerHealth.currentHealth <= 0f || IsOver || enemiesSpawned >= spawnCounts.Sum())
        {
            return; // 如果玩家不存在、玩家已死亡、游戏结束或怪物已全部生成，则不再生成怪物  
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length); // 随机选择一个生成点    
        int i = Random.Range(0, enemies.Length);
            if (spawnCounts[i] > 0) // 如果还需要生成当前类型的怪物  
            {
                Instantiate(enemies[i], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // 生成怪物
                IsCreated = true;//表明已经开始生成怪物
               
                spawnCounts[i]--; // 更新剩余需要生成的怪物数量  
                enemiesSpawned++; // 更新已生成的怪物数量  
                
            }
            else
        {
            Spawn();
        }
        
    }

    private void SetSpawnCountsBasedOnLevel() // 根据等级设置怪物的生成数量       
    {
        spawnCounts = new int[enemies.Length]; // 创建数组存储每种怪物的生成数量，数组长度与怪物种类数相同  
        switch (level) // 根据等级设置怪物的生成数量  
        {
            case 1: // 当level为1时：生成10个第一种怪物  
                spawnCounts[0] = 10;
                break;
            case 2: // 当level为2时：15个第一种怪物，8个第二种怪物；  
                spawnCounts[0] = 15;
                spawnCounts[1] = 8;
                break;
            case 3: // 当level为3时：18个第一种怪物，8个第二种怪物，1个第三种怪物；  
                spawnCounts[0] = 18;
                spawnCounts[1] = 8;
                spawnCounts[2] = 1;
                break;
            case 4: // 当level为4时：5个第三种怪物；  
                spawnCounts[2] = 5; // 只生成第三种怪物
                break;
            default: level = 0; // 其他等级不生成怪物（可选：可以根据需要添加更多等级的怪物生成规则）  
                break;
        }
    }
}