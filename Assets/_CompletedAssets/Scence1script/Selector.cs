using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Selector : MonoBehaviour
{
    public Button ST;
    public Button SL;
    public Button ET;
    public Button TJ;

    public Image BG;
    public Image shezhi;
    public Button C1;
    public Button C2;
    public Button C3;
    public Button RT;
    public Button Settingback;

    public Button di1guan;

    public AudioClip[] audioClips;
    private AudioSource audioSource;
    public int charactor;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件的引用  
        SL.onClick.AddListener(charactors);
        TJ.onClick.AddListener(setting);
        Settingback.onClick.AddListener(returnback);

        C1.onClick.AddListener(Aris);
        C2.onClick.AddListener(Momoi);
        C3.onClick.AddListener(Azusa);
        RT.onClick.AddListener(returnback);
        charactor = PlayerPrefs.GetInt("CS", 0);


        di1guan.onClick.AddListener(start1);
    }

    void start1()
    {
        SceneManager.LoadScene("main");
    }


    void setting()
    {
        ST.gameObject.SetActive(false);
        SL.gameObject.SetActive(false);
        TJ.gameObject.SetActive(false);
        ET.gameObject.SetActive(false);

        shezhi.gameObject.SetActive(true);
    }

    void returnback()
    {
        ST.gameObject.SetActive(true);
        SL.gameObject.SetActive(true);
        TJ.gameObject.SetActive(true);
        ET.gameObject.SetActive(true);

        BG.gameObject.SetActive(false);
        C1.gameObject.SetActive(false);
        C2.gameObject.SetActive(false);
        C3.gameObject.SetActive(false);
        RT.gameObject.SetActive(false);
        shezhi.gameObject.SetActive(false);
    }

    void charactors()
    {
        ST.gameObject.SetActive(false);
        SL.gameObject.SetActive(false);
        ET.gameObject.SetActive(false);

        BG.gameObject.SetActive(true);
        C1.gameObject.SetActive(true);
        C2.gameObject.SetActive(true);
        C3.gameObject.SetActive(true);
        RT.gameObject.SetActive(true);
    }

    void Aris()
    {
        charactor = 0;
        audioSource.PlayOneShot(audioClips[Random.Range(0, 2)]);
    }

    void Momoi()
    {
        charactor = 1;
        audioSource.PlayOneShot(audioClips[Random.Range(2,4)]);
    }

    void Azusa()
    {
        charactor = 2;
        audioSource.PlayOneShot(audioClips[Random.Range(4,6)]);
    }

    // Update is called once per frame
    void Update()
    {
      PlayerPrefs.SetInt("CS",charactor);

    }
}
