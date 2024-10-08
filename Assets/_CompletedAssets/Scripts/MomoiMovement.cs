using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomoiMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public Momoishoot reload;
    private float animationDuration = 2.2f; // 动画持续时间  
    public bool isreload = false;
    Vector3 movement;
    Animator anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        reload = GameObject.Find("Shoot").GetComponent<Momoishoot>();
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
        bool walking = h != 0f || v != 0f;
        if (reload.boltnumber == 0 && !walking)
        {
            isreload = true;
        }
        if (Input.GetKey("r") && reload.boltnumber != 45)
        {
            isreload = true;
        }
        anim.SetBool("reload", isreload);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Momoi_Original_Normal_Reload") &&
      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            OnReloadComplete();
        }

    }
    public void OnReloadComplete()
    {
        reload.reload();
        isreload = false;
    }

    void Move(float h, float v)
    {
        if (isreload == false && reload.isshoot == false && reload.exing == false)
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
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }

    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("iswalking", walking);
    }
}
// Start is called before the first frame update
