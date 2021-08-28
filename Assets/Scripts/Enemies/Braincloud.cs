using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braincloud : MonoBehaviour
{
    //config params
    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange = 1f;
    [SerializeField] GameObject brainLightning;
    [SerializeField] GameObject brainSpark;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackDuration;
    [SerializeField] AudioClip[] mySparking;
    [SerializeField] AudioClip[] myBolting;
    [SerializeField] AudioSource myAudioSource;

    //cached references
    HippoRocket hippoRocket;
    float attackRangeSqr;
    bool isAttacking;
    bool chargingUp;
    bool sparkReady;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = FindObjectOfType<HippoRocket>();
        isAttacking = false;
        sparkReady = true;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        var playerDistance = Vector2.SqrMagnitude(transform.position - hippoRocket.transform.position);
        attackRangeSqr = attackRange * attackRange;
        if ( playerDistance <= attackRange && !isAttacking)
        {
            StartCoroutine(FireAttack());
        }
        if(chargingUp && sparkReady)
        {
            StartCoroutine(Spark());
        }

    }

    private IEnumerator Spark()
    {
        var spark = Instantiate(brainSpark, transform.position, Quaternion.identity);
        spark.transform.parent = gameObject.transform;
        var randomX = Random.Range(-360f, 360f);
        var randomY = Random.Range(-360f, 360f);
        spark.transform.rotation = new Quaternion(randomX, randomY, 0, 0);
        sparkReady = false;
        var randomTime = Random.Range(0.01f, 0.1f);
        yield return new WaitForSeconds(randomTime);
        sparkReady = true;
        while (chargingUp) { yield return null; }
        Destroy(spark);
    }

    private IEnumerator FireAttack()
    {
        isAttacking = true;
        chargingUp = true;
        var clipToPlay = UnityEngine.Random.Range(0, mySparking.Length);
        AudioSource.PlayClipAtPoint(mySparking[clipToPlay], Camera.main.transform.position, audioManager.effectVolume * audioManager.masterVolume);
        yield return new WaitForSeconds(attackCooldown);
        chargingUp = false;
        var newAttack = Instantiate(brainLightning, transform.position, Quaternion.identity);
        newAttack.transform.parent = gameObject.transform;
        var attackToPlay = UnityEngine.Random.Range(0, myBolting.Length);
        myAudioSource.clip = myBolting[attackToPlay];
        myAudioSource.volume = audioManager.effectVolume * audioManager.masterVolume;
        myAudioSource.Play();
        yield return new WaitForSeconds(attackDuration);
        Destroy(newAttack);
        isAttacking = false;
        myAudioSource.Stop();
        yield return null;
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * moveSpeed));
    }
}
