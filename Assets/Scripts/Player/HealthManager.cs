using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HealthManager : MonoBehaviour
{
    //config params
    [SerializeField] Image[] healthCapsules;
    [SerializeField] Sprite fullCapsule;
    [SerializeField] Sprite emptyCapsule;
    public int health;
    public int numberOfCapsules;
    [SerializeField] float invulnerableCoolDown = 1f;

    //cached references
    HippoRocket hippoRocket;
    Fuel fuel;
    bool invulnerable;
    float invulnerableTimer;
    Animator myAnimator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = GetComponent<HippoRocket>();
        fuel = GetComponent<Fuel>();
        invulnerable = false;
        invulnerableTimer = invulnerableCoolDown;
        myAnimator = GetComponent<Animator>();
    }

    public void TakeHit()
    {
        if (invulnerable) { return; }
        health--;
        myAnimator.SetBool("invulnerable", true);
        if(health <= 0)
        {
            hippoRocket.StopEngines();
            fuel.OutOfFuel();
        }
        invulnerable = true;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpgradeHealth(int upgradeValue)
    {
        numberOfCapsules += upgradeValue;
        health = numberOfCapsules;

    }

    public void RestoreHealth()
    {
        health++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            ResetLevel();
        }
        if (health > numberOfCapsules)
        {
            health = numberOfCapsules;
        }
        for (int i = 0; i < healthCapsules.Length; i++)
        {
            if(i<health)
            {
                healthCapsules[i].sprite = fullCapsule;
            }
            else
            {
                healthCapsules[i].sprite = emptyCapsule;
            }
            
            if(i<numberOfCapsules)
            {
                healthCapsules[i].enabled = true;
            }
            else { healthCapsules[i].enabled = false; }
        }
        InvulnerabilityReset();
    }

    private void InvulnerabilityReset()
    {
        if (!invulnerable) { return; }
        if (invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;
        }
        if (invulnerableTimer <=0 )
        {
            invulnerable = false;
            myAnimator.SetBool("invulnerable", false);
            invulnerableTimer = invulnerableCoolDown;
        }

    }
}
