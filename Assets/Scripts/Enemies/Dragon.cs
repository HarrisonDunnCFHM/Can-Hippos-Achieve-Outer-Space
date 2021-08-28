using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    //config params
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject myFireBreather;
    [SerializeField] float attackRange = 1f;
    [SerializeField] AudioClip[] myAttacks;

    //cached references
    HippoRocket hippoRocket;
    bool fireShot;
    AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = FindObjectOfType<HippoRocket>();
        myFireBreather.SetActive(false);
        fireShot = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FacePlayer();
        ShootFire();
    }

    private void ShootFire()
    {
        if (fireShot) { return; }
        var distToRocket = transform.position.y - hippoRocket.transform.position.y;
        if (Mathf.Abs(distToRocket) < attackRange)
        {
            myFireBreather.SetActive(true);
            fireShot = true;
            var clipToPlay = UnityEngine.Random.Range(0, myAttacks.Length);
            AudioSource.PlayClipAtPoint(myAttacks[clipToPlay], Camera.main.transform.position, 1.5f * audioManager.effectVolume * audioManager.masterVolume);
        }
    }

    private void FacePlayer()
    {
        var distToRocket = transform.position.x - hippoRocket.transform.position.x;
        if (distToRocket > 0 && transform.localScale.x > 0)
        {
            var newFace = transform.localScale.x * -1;
            transform.localScale = new Vector2(newFace, transform.localScale.y);
        }
        if (distToRocket < 0 && transform.localScale.x < 0)
        {
            var newFace = transform.localScale.x * -1;
            transform.localScale = new Vector2(newFace, transform.localScale.y);
        }
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * moveSpeed));
    }
}
