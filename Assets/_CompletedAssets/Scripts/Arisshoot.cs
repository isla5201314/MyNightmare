using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Arisshoot : MonoBehaviour
{
    private bool isCoroutineRunning = false;
    public GameObject bolt;
    public GameObject bolt2;
    public AudioClip[] ouch;
    public AudioClip[] ouch2;
    public float nexttime = 4.0f;
    public float nextFire = 0.0f;
    public float bulletSpeed = 20f; // 子弹速度，可以根据需要调整
    public ArisMovement  aris;
    public int boltnumber = 12;
    public bool isshoot = false;
    public bool exing = false;
    //private Transform parentTransform;
    private int shootCounter = 0; // 用于追踪射击次数的计数器 

    public GameObject ball;
    public GameObject squa;
    public GameObject putonggongjitexiao;
    public GameObject dazhaojitexiao;

    private Costmanager costManager; // 声明为类的成员变量

    Animator anim;
    //public EnemyHealth enemyhealth;

    private float scaleUpDuration = 2.5f; // 缩放时间，可以根据需要调整  
    private float interval = 25f; // 25秒的时间间隔  
    private float timer;

    void Awake()
    {
        //parentTransform = transform.parent;
        Transform parentTransform = transform.parent;
        anim = parentTransform.GetComponent<Animator>();
        aris = parentTransform.GetComponent<ArisMovement >();

        timer = interval; // 初始化计时器
    }

    void Change()
    {
        if (aris.power < 2)
        {
            aris.power += 1;
        }
        //根据不同的power值显示不同特效；
        aris.genxinGameObjects();
    }

    void sprite()
    {
        //Debug.Log("gg");
        ArisMovement arisMovement = transform.parent.GetComponent<ArisMovement>();
        arisMovement.elec();

        GameObject instantiatedObject = Instantiate(ball, transform.position, transform.rotation);
        instantiatedObject.transform.SetParent(transform, false);
        instantiatedObject.transform.localPosition = Vector3.zero; // 重置坐标为0,0,0  
        instantiatedObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f); // 缩小为原来的0.05倍
        // 调用协程以平滑地改变缩放  
        StartCoroutine(ScaleUpOverTime(instantiatedObject.transform, scaleUpDuration, 0.01f));
        Destroy(instantiatedObject, 2.8f);

        GameObject instantiatedObject1 = Instantiate(squa, transform.position, Quaternion.identity);
        instantiatedObject1.transform.SetParent(transform, false);
        instantiatedObject1.transform.localPosition = Vector3.zero; // 重置坐标为0,0,0  
        instantiatedObject1.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f); // 缩小为原来的0.05倍
        Destroy(instantiatedObject1, 2.8f);
    }

    IEnumerator ScaleUpOverTime(Transform targetTransform, float duration, float targetScale)
    {
        float elapsedTime = 0;
        Vector3 startingScale = targetTransform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 增加经过的时间  
            float t = Mathf.Clamp01(elapsedTime / duration); // 获取t值，用于Lerp函数  
            Vector3 newScale = Vector3.Lerp(startingScale, new Vector3(targetScale, targetScale, targetScale), t); // 使用Lerp计算新的缩放值  
            targetTransform.localScale = newScale; // 设置新的缩放值  
            yield return null; // 在下一帧继续执行  
        }
        // 确保在结束时达到目标缩放值（由于浮点精度问题，Lerp可能不会完全达到目标值）  
        targetTransform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    // Update is called once per frame  
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // 每一帧减少时间  
        }
        else
        {
            // 当计时器达到0时调用Change函数  
            Change();
            // 重置计时器以再次计数  
            timer = interval;
        }

        GameObject costObject = GameObject.FindGameObjectWithTag("Cost");
        Costmanager costManager = costObject.GetComponent<Costmanager>();
        if (Input.GetButtonDown("Fire2") && costManager.CurrentlyCost >= 6.0f) // 检测按下射击键  
        {
            sprite();
            costManager.CurrentlyCost -= 6;
            exing = true;
            int index = Random.Range(0, ouch.Length);
            AudioSource.PlayClipAtPoint(ouch[index], transform.position);
            int index2 = Random.Range(0, ouch2.Length);
            AudioSource.PlayClipAtPoint(ouch2[index2], transform.position);
            InvokeRepeating("ShootBullets",2.7f, 0.5f);
        }
        if (shootCounter >= 1)
        {
            CancelInvoke("ShootBullets"); // 取消对 ShootBullets 方法的进一步调用
            shootCounter = 0;
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire&&boltnumber>0&&aris.isreload == false && !exing)
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Aris_Original_Normal_Attack_Ing") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !Input.GetButton("Fire1"))
        {
            isshoot = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Aris_Original_Exs") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            exing = false;
        }
    }

    IEnumerator DelayShoot()
    {
        isCoroutineRunning = true; // 设置协程运行标志位为true  
        yield return new WaitForSeconds(1.8f); // 等待1.6s
        shoot(); // 调用Shoot方法  
        isCoroutineRunning = false; // 设置协程运行标志位为false    
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
        boltnumber--;
        Instantiate(putonggongjitexiao, transform.position, transform.rotation);
    }
    void ShootBullets()
    {
        ArisMovement arisMovement = transform.parent.GetComponent<ArisMovement>();

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
        Instantiate(putonggongjitexiao, transform.position, transform.rotation);
        Instantiate(dazhaojitexiao, transform.position, transform.rotation);
        arisMovement.bolang();
        aris.power = 0;
        aris.genxinGameObjects();
        //GetComponent<AudioSource>().Play();

    }
    public void reload()
    {
       
        boltnumber = 12;
        
    }

}