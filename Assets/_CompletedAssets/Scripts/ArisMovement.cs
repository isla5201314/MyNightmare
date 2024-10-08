using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArisMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public Arisshoot  reload;
    private float animationDuration = 2.2f; // 您的动画持续时间  
    public bool isreload = false ;
    Vector3 movement;
    Animator anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;
    public GameObject texiao;
    public GameObject dazhaohoutexiao;
    public GameObject[] chongneng;

    public int power = 0;

    void Awake()
    {
        reload = GameObject.Find("Shoot").GetComponent<Arisshoot>();
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    public void bolang()
    {
        Instantiate(texiao, transform.position, transform.rotation);
    }

    public void elec()
    {
        Instantiate(dazhaohoutexiao, transform.position, transform.rotation);
    }

    public void genxinGameObjects()
    {
        // 确保数组中有足够的游戏对象，并且power的值在有效范围内  
        if (chongneng != null && chongneng.Length >= 3 && power >= 0 && power <= 2)
        {
            // 禁用所有的游戏对象  
            for (int i = 0; i < chongneng.Length; i++)
            {
                chongneng[i].SetActive(false);
            }

            // 根据power的值启用相应的游戏对象  
            chongneng[power].SetActive(true);
        }
        else
        {
            Debug.LogError("数组未初始化或power值超出范围！");
        }
    }

    void FixedUpdate()
    {



        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
        bool walking = h != 0f || v != 0f;
        if (reload.boltnumber == 0&&!walking)
        {
            isreload = true;
        }
        if (Input.GetKey("r")&& reload.boltnumber !=12)
        {
            isreload = true;
        }
        anim.SetBool("reload", isreload);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Aris_Original_Normal_Reload") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && isreload)
        {
            OnReloadComplete();
        }

    }
    public void OnReloadComplete()
    {
        //Debug.Log("已换弹");
        reload.reload();
        isreload = false;
    }

    void Move(float h,float v)
    {
        if (isreload == false&&reload.isshoot  == false&&reload.exing ==false)
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics .Raycast (camRay ,out floorHit,camRayLength,floorMask ))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }

    }

void Animating (float h,float v)
    {
        bool walking = h!= 0f || v!= 0f;
        anim.SetBool("iswalking",walking);
    }
}
    // Start is called before the first frame update

