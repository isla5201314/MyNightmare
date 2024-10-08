using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomoiAmmo : MonoBehaviour
{
    public float destroytime = 3.0f;
    public int damagePerShot = 30;
    public EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", destroytime);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.GetComponent<EnemyHealth>();


            if (enemyHealth.currentHealth >= 0)
            {

                enemyHealth.TakeDamage(damagePerShot);

               // Destroy(gameObject);
            }
        }
    }
}
