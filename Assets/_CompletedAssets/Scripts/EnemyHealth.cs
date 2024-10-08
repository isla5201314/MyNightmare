using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public Image HealthBar;
    float hpwidth;

    public GameObject deathFX;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    public ScoreManager SM;
    bool isDead;
    bool isSinking;
    // Start is called before the first frame update
    void Awake()
    {

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        
        SM = FindObjectOfType<ScoreManager>();
        currentHealth = startingHealth;

        hpwidth = HealthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        //float trueWidth = currentHealth / startingHealth * hpwidth;
        float trueWidth = (float)currentHealth / startingHealth * hpwidth;
        HealthBar.rectTransform.sizeDelta = new Vector2(trueWidth, HealthBar.rectTransform.rect.height);
    }

    public void TakeDamage(int amount)
    {
        if(isDead)
            return;

        enemyAudio.Play();
        //Debug.Log(amount);
        //Debug.Log("受伤前:"+currentHealth);
        currentHealth -= amount;
        //Debug.Log("受伤后:"+currentHealth);
        if (currentHealth <=0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        SM.AddFun();
        Instantiate(deathFX, transform.position, transform.rotation);
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
