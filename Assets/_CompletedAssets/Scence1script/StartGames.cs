using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGames : MonoBehaviour
{
    public Button ST;
    public PlayerHealth PH;
    public AudioClip[] audioClips;
    private AudioSource audioSource; // 创建一个 AudioSource 类型的变量  
    public int spawntime=0;

    public GameObject menu;
    public Image SelectMission;

    // Start is called before the first frame update  
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件的引用  
        PH = FindObjectOfType<PlayerHealth>();
        ST.onClick.AddListener(wait);
    }

    public void wait()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]); // 随机选择一个音频并播放  
        Invoke("games", spawntime);
    }

    public void games()
    {
        menu.gameObject.SetActive(false);
        SelectMission.gameObject.SetActive(true);
        //PH.RestartLevel();  

    }

    // Update is called once per frame  
    void Update()
    {

    }
}