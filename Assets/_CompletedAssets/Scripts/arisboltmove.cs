using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arisboltmove : MonoBehaviour
{
    public float destroytime = 5.0f;
    public int damagePerShot = 100;
    public EnemyHealth enemyHealth;
    public int damrate = 1;
    public ArisMovement aris;

    //public GameObject putonggongjitexiao;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", destroytime);

        GameObject arisObject = GameObject.FindGameObjectWithTag("Player");
        aris = arisObject.GetComponent<ArisMovement>();
        //aris =GetComponent<ArisMovement>();
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (aris.power)
        {
            case 0: damrate = 1; break;
            case 1: damrate = 2; break;
            case 2: damrate = 3;  break;
            default:break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth.currentHealth >=0)
            {
                enemyHealth.TakeDamage(damagePerShot * damrate);
                //Instantiate(putonggongjitexiao, transform.position, transform.rotation);
            }
        }
    }
}
