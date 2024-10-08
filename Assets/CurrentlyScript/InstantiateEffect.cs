using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEffect : MonoBehaviour
{
    public GameObject[] Effected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(Effected[0], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(Effected[1], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(Effected[2], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(Effected[3], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(Effected[4], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Instantiate(Effected[5], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Instantiate(Effected[6], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Instantiate(Effected[7], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Instantiate(Effected[8], transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Instantiate(Effected[9], transform.position, transform.rotation);
        }
    }
}
