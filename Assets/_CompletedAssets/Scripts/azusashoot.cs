using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class azusashoot : MonoBehaviour
{
    private bool isCoroutineRunning = false;
    public GameObject bolt;
    public GameObject bolt2;
    public AudioClip[] ouch;
    public AudioClip[] ouch2;
    public float nexttime =0.5f;
    public float nextFire = 0.0f;
    public float bulletSpeed = 20f; // 子弹速度，可以根据需要调整
    public AzusaMovement azusa;
    public int boltnumber = 30;
    public bool isshoot = false;
    public bool exing = false;
    //private Transform parentTransform;
    private int shootCounter = 0; // 用于追踪射击次数的计数器 
    //public float spreadAngle = 40f; // ex扇形角度  
    //public int numBullets = 60; // ex子弹数量  

    Animator anim;
    //public EnemyHealth enemyhealth;

    private Costmanager costManager; // 声明为类的成员变量


    public GameObject FireEffected;


    void Awake()
    {
        //parentTransform = transform.parent;
        Transform parentTransform = transform.parent;
        anim = parentTransform.GetComponent<Animator>();
        azusa = parentTransform.GetComponent<AzusaMovement >();

        //GameObject costObject = GameObject.FindGameObjectWithTag("Cost");
        //if (costObject != null) { Debug.Log("找到了Cost标签"); }
        //else { Debug.LogError("未找到带有 Cost 标签的游戏对象"); }
        //// 获取Costmanager脚本组件  
        //Costmanager costManager = costObject.GetComponent<Costmanager>();
        //if (costManager != null) { Debug.Log("找到了Cost标签对象的脚本"); }
        // else { Debug.LogError("未找到带有 Cost 标签的游戏对象"); }
    }
    // Update is called once per frame  
    void Update()
    {
        GameObject costObject = GameObject.FindGameObjectWithTag("Cost");
        Costmanager costManager = costObject.GetComponent<Costmanager>();
        if (Input.GetButtonDown("Fire2") && costManager.CurrentlyCost >= 5.0f) // 检测按下射击键  
        {
            costManager.CurrentlyCost -= 5;
            exing = true;
            int index = Random.Range(0, ouch.Length);
            AudioSource.PlayClipAtPoint(ouch[index], transform.position);
            int index2 = Random.Range(0, ouch2.Length);
            AudioSource.PlayClipAtPoint(ouch2[index], transform.position);
            InvokeRepeating("ShootBullets",1.7f, 0.5f);
        }
        if (shootCounter >= 1)
        {
            CancelInvoke("ShootBullets"); // 取消对 ShootBullets 方法的进一步调用
            shootCounter = 0;
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire&&boltnumber>0&&azusa.isreload == false && !exing)
        {
            nextFire = Time.time + nexttime;
            isshoot = true;
            StartCoroutine(DelayShoot()); // 启动协程来处理延迟射击 
        }
        anim.SetBool("shoot", isshoot);
        anim.SetBool("ex", exing);

        if(boltnumber ==0)
        {
            isshoot = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Azusa_Original_Normal_Attack_Ing") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !Input.GetButton("Fire1"))
        {
            isshoot = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Azusa_Original_Exs") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            exing = false;
        }
    }

    IEnumerator DelayShoot()
    {
        isCoroutineRunning = true; // 设置协程运行标志位为true  
        yield return new WaitForSeconds(0.5f); // 等待1.6s
        shoot(); // 调用Shoot方法  
        isCoroutineRunning = false; // 设置协程运行标志位为false    
    }
    public void stop()
    {
        isshoot = false;
    }

    public void shoot()
    {

        // 实例化子弹并设置其位置和旋转角度    
        GameObject newBolt = Instantiate(bolt, transform.position, transform.rotation);
        // 获取子弹当前的旋转角度  
        Quaternion currentRotation = newBolt.transform.rotation;
        // 设置子弹的旋转角度的X值为90度，保持其他轴的值不变  
        Vector3 eulerAngles = currentRotation.eulerAngles;
        eulerAngles.x = 90;
        Quaternion newRotation = Quaternion.Euler(eulerAngles);

        // 应用新的旋转角度到子弹上  
        newBolt.transform.rotation = newRotation;

        // 获取Rigidbody组件并设置速度    
        Rigidbody rb = newBolt.GetComponent<Rigidbody>();

        // 计算子弹发射的方向和力度    
        Vector3 direction = transform.forward; // 角色的前方向量    
        float force = bulletSpeed; // 子弹的速度    

        // 给子弹施加一个力，使其向前飞行    
        rb.AddForce(direction * force, ForceMode.Impulse);

        GetComponent<AudioSource>().Play();
        Instantiate(FireEffected, transform.position, transform.rotation);
        boltnumber--;
   

    }
    void ShootBullets()
    {
        shootCounter++; // 每次调用时增加计数器的值
        // 实例化子弹并设置其位置和旋转角度    
        GameObject newBolt = Instantiate(bolt2, transform.position, transform.rotation);
        // 获取子弹当前的旋转角度  
        Quaternion currentRotation = newBolt.transform.rotation;
        // 设置子弹的旋转角度的X值为90度，保持其他轴的值不变  
        Vector3 eulerAngles = currentRotation.eulerAngles;
        eulerAngles.x = 90;
        Quaternion newRotation = Quaternion.Euler(eulerAngles);

        // 应用新的旋转角度到子弹上  
        newBolt.transform.rotation = newRotation;

        // 获取Rigidbody组件并设置速度    
        Rigidbody rb = newBolt.GetComponent<Rigidbody>();

        // 计算子弹发射的方向和力度    
        Vector3 direction = transform.forward; // 角色的前方向量    
        float force = bulletSpeed; // 子弹的速度    

        // 给子弹施加一个力，使其向前飞行    
        rb.AddForce(direction * force, ForceMode.Impulse);
        exing = false;
        //GetComponent<AudioSource>().Play();

    }
    public void reload()
    {
        boltnumber = 30;
    }

}