using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMController : MonoBehaviour
{
    public bool Isstop=false;
    public bool Isplay = false;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    public Image targetImage;
    private PlayerHealth playerHealth; // 存储对 PlayerHealth 脚本的引用  

    // Start is called before the first frame update  
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件的引用  
        audioSource.PlayOneShot(audioClips[0]); // 播放第一个音乐  

        // 寻找标签为 "Player" 的游戏对象，并获取其上的 PlayerHealth 脚本  
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("未找到标签为 'Player' 的游戏对象。");
        }
    }

    // Update is called once per frame  
    void Update()
    {
        // 检查 playerHealth 是否为 null，以确保成功获取了 PlayerHealth 脚本的引用  
        if (playerHealth != null && playerHealth.Isdead&&!Isplay) // 检测 PlayerHealth 脚本中的 Isdead 属性是否为 true  
        {
            if (!Isstop)
            {
                audioSource.Stop(); // 停止当前播放的音乐  
                Isstop = true;
            }
            audioSource.PlayOneShot(audioClips[1]); // 播放第二个音乐  
            Isplay = true;
        }
    }
}