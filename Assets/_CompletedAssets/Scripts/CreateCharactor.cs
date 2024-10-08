using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateCharactor : MonoBehaviour
{
    public GameObject[] Charactors;
    public int charactor;

    private void Awake()
    {
        charactor = PlayerPrefs.GetInt("CS", 0);
        Instantiate(Charactors[charactor], transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {  
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
