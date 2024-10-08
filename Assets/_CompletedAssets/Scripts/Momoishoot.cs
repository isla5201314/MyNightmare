using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Momoishoot : MonoBehaviour
{
    public GameObject bolt;
    public GameObject bolt2;
    public AudioClip[] ouch;
    public AudioClip[] ouch2;
    public float nexttime = 0.2f;
    public float nextFire = 0.0f;
    public float bulletSpeed = 20f; // 子弹速度，可以根据需要调整
    public MomoiMovement  momoi;
    public int boltnumber = 45;
    public bool isshoot = false;
    public bool exing = false;
    private int shootCounter = 0; // 用于追踪射击次数的计数器 
    public float spreadAngle = 40f; // ex扇形角度  
    public int numBullets = 50; // ex子弹数量  
    private float delay = 1.0f; // 延迟时间（秒）  
    //private float timer = 0.0f; // 计时器  

    public int MaxBoltNum = 45;

    public GameObject putonggongjitexiao;

    private Costmanager costManager; // 声明为类的成员变量

    Animator anim;
    //public EnemyHealth enemyhealth;


    void Awake()
    {
        Transform parentTransform = transform.parent;
        anim = parentTransform.GetComponent<Animator>();
        momoi = parentTransform.GetComponent<MomoiMovement>();
        boltnumber = MaxBoltNum;
    }
    // Update is called once per frame  
    void Update()
    {
        
        GameObject costObject = GameObject.FindGameObjectWithTag("Cost");
        Costmanager costManager = costObject.GetComponent<Costmanager>();
        if (Input.GetButtonDown("Fire2") && costManager.CurrentlyCost >= 3.0f) // 检测按下射击键  
        {
            costManager.CurrentlyCost -= 3;
            exing = true;
            int index = Random.Range(0, ouch.Length);
            AudioSource.PlayClipAtPoint(ouch[index], transform.position);
            int index2 = Random.Range(0, ouch2.Length);
            AudioSource.PlayClipAtPoint(ouch2[index], transform.position);
            InvokeRepeating("ShootBullets", 1.2f, 0.5f);
        }
        if (shootCounter >= 3)
        {
            CancelInvoke("ShootBullets"); // 取消对 ShootBullets 方法的进一步调用
            shootCounter = 0;
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire&&boltnumber>0&&momoi.isreload ==false && !exing)
        {
            //Debug.Log("sheji");
            isshoot = true;
            shoot();
        }
        anim.SetBool("shoot", isshoot);
        anim.SetBool("ex", exing);

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Momoi_Original_Normal_Attack_Ing") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !Input.GetButton("Fire1"))
        {
            isshoot = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Momoi_Original_Exs") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            exing = false;
        }
  
    }

    //~~~
    //public void stop()
    //{
    //    isshoot = false;
    //}

    public void shoot()
    {
        
        nextFire = Time.time + nexttime;

        // 实例化子弹并设置其位置和旋转角度  
        GameObject newBolt = Instantiate(bolt, transform.position, transform.rotation);

        // 获取角色的旋转角度并将其设置为子弹的旋转角度  
        Quaternion rotation = transform.rotation;
        newBolt.transform.rotation = rotation;

        //普通攻击特效
        //GameObject currentObject = this.gameObject;
        //Transform parentTransform = currentObject.transform.parent;
        //Vector3 adjustedPosition = parentTransform.position;
        //adjustedPosition.y += 1f;
        //adjustedPosition.z += 3f; // 如果需要的话，还可以对Z轴坐标进行调整  
        //Instantiate(putonggongjitexiao, adjustedPosition, newBolt.transform.rotation);

        // 获取Rigidbody组件并设置速度  
        Rigidbody rb = newBolt.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed; // 设置子弹沿着角色前方的速度  

        GetComponent<AudioSource>().Play();
        boltnumber -- ;
        //timer += Time.deltaTime;
        //if (timer >= delay&& Input.GetButton("Fire1"))
        //{

        //    // 延迟时间已过，执行操作  
        //    isshoot = false;
        //    timer = 0.0f; // 重置计时器  
        //}


    }
    void ShootBullets()
    {
        shootCounter++; // 每次调用时增加计数器的值    
        float angleStep = spreadAngle / (numBullets - 1); // 计算每颗子弹之间的角度间隔    

        // 获取角色的前方向量  
        Vector3 characterForward = transform.parent.forward;

        for (int i = 0; i < numBullets; i++)
        {
            float angle = -spreadAngle / 2 + i * angleStep; // 计算每颗子弹的发射角度    
            Quaternion rotation = Quaternion.Euler(0, angle, 0); // 设置子弹的旋转角度    

            // 使用角色的前方向和旋转来计算子弹的发射方向  
            Vector3 direction = rotation * characterForward;

            // 从发射点的位置发射子弹  
            Vector3 position = transform.position;

            GameObject bullet = Instantiate(bolt2, position, rotation); // 实例化子弹预制体    
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>(); // 获取子弹的刚体组件    
            rigidbody.AddForce(direction * 10f, ForceMode.Impulse); // 设置子弹的速度和方向    
        }
    }
    public void reload()
    {
        boltnumber = MaxBoltNum;
    }

}