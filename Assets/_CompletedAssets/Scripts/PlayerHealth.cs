using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    Slider healthSlider;
    Image damageImage;
   // public AudioClip deathClip;
    public float flashSpeed = 5.0f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public bool Isdead = false;

    public int delay = 5;

    Animator anim;

    public AudioClip[] audioClips; // 确保这个数组至少有3个元素  
    private AudioSource playerAudio;
    private bool isPlayingAudio = false; // 添加一个标志来跟踪是否正在播放音频

    MomoiMovement momoiMovement;
    Momoishoot momoiShooting;
    ArisMovement arisMovement;
    Arisshoot arisShooting;
    AzusaMovement azusaMovement;
    azusashoot azusaShooting;

    // PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead = false;
    bool damaged;

    // Start is called before the first frame update  
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        momoiMovement = GetComponent<MomoiMovement>();
        momoiShooting = GetComponentInChildren<Momoishoot>();
        arisMovement = GetComponent<ArisMovement>();
        arisShooting = GetComponentInChildren<Arisshoot>();
        azusaMovement = GetComponent<AzusaMovement>();
        azusaShooting = GetComponentInChildren<azusashoot>();

        currentHealth = startingHealth;

        // 寻找标签为 "health" 的对象，并获取其上的 Slider 组件  
        GameObject healthObj = GameObject.FindGameObjectWithTag("Health");
        if (healthObj != null)
        {
            healthSlider = healthObj.GetComponent<Slider>();
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("没有找到标签为 'health' 的对象。");
        }

        // 寻找标签为 "damage" 的对象，并获取其上的 Image 组件  
        GameObject damageObj = GameObject.FindGameObjectWithTag("damage");
        if (damageObj != null)
        {
            damageImage = damageObj.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("没有找到标签为 'damage' 的对象。");
        }
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        if (!isPlayingAudio) // 检查是否没有在播放音频  
        {
            StartCoroutine(PlayRandomAudio()); // 开始协程来随机播放音频并设置延迟  
        }

        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        //playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    IEnumerator PlayRandomAudio()
    {
        int randomIndex = Random.Range(0, 2); // 从0（包含）到2（不包含）的随机索引，对应数组的前两个元素  
        playerAudio.clip = audioClips[randomIndex]; // 播放随机选择的音频  
        playerAudio.Play();
        isPlayingAudio = true; // 设置标志为true，表示正在播放音频  
        yield return new WaitForSeconds(3); // 等待3秒  
        isPlayingAudio = false; // 设置标志为false，表示可以播放下一个音频了  
    }

    public void Death()
    {
        string objectName = gameObject.name;

        isDead = true;
        //playerShooting.DisableEffects();
        anim.SetTrigger("dead");

        playerAudio.clip = audioClips[2]; // 直接播放第三个音频（确保数组至少有3个元素）  
        playerAudio.Play(); // 播放Death音效

        if (objectName == "Momoi_Original(Clone)")
        {
            momoiMovement.enabled = false;
            momoiShooting.enabled = false;
        }
        if (objectName == "Aris_Original(Clone)")
        {
            arisMovement.enabled = false;
            arisShooting.enabled = false;
        }
        if (objectName == "Azusa_Original(Clone)")
        {
            azusaMovement.enabled = false;
            azusaShooting.enabled = false;
        }

        Isdead = true;
    }

}