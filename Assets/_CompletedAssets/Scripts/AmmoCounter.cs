using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public Text Ammo;
    int CurrentAmmo;
    int MaxAmmo;

    Momoishoot momoiShooting;
    Arisshoot arisShooting;
    azusashoot azusaShooting;

    // Start is called before the first frame update    
    IEnumerator Start()
    {
        // 等待0.1秒    
        yield return new WaitForSeconds(0.1f);

        // 查找标签为 "Player" 的游戏对象    
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            // 查找名为 "Shoot" 的子对象    
            Transform shootObject = playerObject.transform.Find("Shoot");
            if (shootObject != null)
            {
                // 获取子对象上的组件    
                momoiShooting = shootObject.GetComponent<Momoishoot>();
                arisShooting = shootObject.GetComponent<Arisshoot>();
                azusaShooting = shootObject.GetComponent<azusashoot>();
            }
            else
            {
                Debug.LogError("没有找到名为 'Shoot' 的子对象");
            }
        }
        else
        {
            Debug.LogError("没有找到带有 'Player' 标签的游戏对象");
        }
    }

    // Update is called once per frame    
    void Update()
    {
        // 检查组件是否已被获取，然后更新文本    
        if (momoiShooting != null)
        {
            Ammo.text = "当前弹匣弹药量:\n" + momoiShooting.boltnumber + "/ 45";
        }
        if (arisShooting != null)
        {
            Ammo.text = "当前弹匣弹药量:\n" + arisShooting.boltnumber + "/ 12";
        }
        if (azusaShooting != null)
        {
            Ammo.text = "当前弹匣弹药量:\n" + azusaShooting.boltnumber + "/ 30";
        }
    }
}