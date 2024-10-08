//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class MiniMap : MonoBehaviour
//{
//    private RectTransform rect;
//    private Transform player;
//    //private Transform enemy;  
//    private static Image item;
//    private Image playerImage;
//    //private Image EnemyImage;  

//    // Start is called before the first frame update  
//    void Start()
//    {
//        StartCoroutine(DelayedStart());
//    }

//    IEnumerator DelayedStart()
//    {
//        yield return new WaitForSeconds(0.1f);
//        item = Resources.Load<Image>("me");
//        rect = GetComponent<RectTransform>();
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;  

//        if (player != null)
//            Instantiate(item);
//    }

//    // Update is called once per frame  
//    void Update()
//    {
//        // ...  
//    }

//    private void ShowPlayer()
//    {
//        playerImage.rectTransform.sizeDelta = new Vector2(10, 10);
//        playerImage.rectTransform.anchoredPosition = new Vector2(0,0);
//        playerImage.sprite = Resources.Load<sprite>();
//        playerImage.transform.Setparent(transform,false);
//    }

//}