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
    public int healthCurrent;
    public int healthMax;
    [SerializeField] float invulnerableCoolDown = 1f;
    [SerializeField] PopOutMenu retryMenu;
    [SerializeField] TokenManager tokenManager;
    [SerializeField] Fuel fuelManager;
    [SerializeField] CoinManager coinManager;
    [SerializeField] DistanceTracker distanceTracker;
    

    //cached references
    HippoRocket hippoRocket;
    Fuel fuel;
    bool invulnerable;
    float invulnerableTimer;
    Animator myAnimator;
    IncrementingData gameData;
    List<Research> allResearch;
    HippoProfile hippoProfile;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = GetComponent<HippoRocket>();
        fuel = GetComponent<Fuel>();
        invulnerable = false;
        invulnerableTimer = invulnerableCoolDown;
        myAnimator = GetComponent<Animator>();
        gameData = FindObjectOfType<IncrementingData>();
        if(gameData.healthMax < 1 )
        {
            gameData.healthMax = 1;
        }
        healthMax = gameData.healthMax;
        healthCurrent = healthMax;
        allResearch = new List<Research>(FindObjectsOfType<Research>());
        distanceTracker = GetComponent<DistanceTracker>();
        hippoProfile = FindObjectOfType<HippoProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetLevel();
        }
        DisplayHealth();
        InvulnerabilityReset();
    }

    private void DisplayHealth()
    {
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }
        for (int i = 0; i < healthCapsules.Length; i++)
        {
            if (i < healthCurrent)
            {
                healthCapsules[i].sprite = fullCapsule;
            }
            else
            {
                healthCapsules[i].sprite = emptyCapsule;
            }

            if (i < healthMax)
            {
                healthCapsules[i].enabled = true;
            }
            else { healthCapsules[i].enabled = false; }
        }
    }

    public void TakeHit()
    {
        if (invulnerable) { return; }
        healthCurrent--;
        myAnimator.SetBool("invulnerable", true);
        if(healthCurrent <= 0)
        {
            hippoRocket.StopEngines();
            fuel.OutOfFuel(); //this function will toggle the retry menu open
            Time.timeScale = 1f;
            invulnerable = true;
            hippoProfile.SetFear(invulnerable);
        }
        invulnerable = true;
        hippoProfile.SetFear(invulnerable);
    }

    public void ResetLevel()
    {
        UpdateHealthData();
        tokenManager.CacheTokenData();
        foreach(Research research in allResearch)
        {
            research.CacheResearchStats();
        }
        fuelManager.CacheFuelStats();
        coinManager.CacheCoinInfo();
        distanceTracker.CacheRocketSpeed();
        gameData.experimentsRun++;
        gameData.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void UpdateHealthData()
    {
        gameData.healthMax = healthMax;
    }

    public void UpgradeHealth(int upgradeValue)
    {
        healthMax += upgradeValue;
        healthCurrent = healthMax;

    }

    public void RestoreHealth()
    {
        healthCurrent++;
    }


    private void InvulnerabilityReset()
    {
        if (!invulnerable) { return; }
        if (healthCurrent <= 0) { return; }
        if (invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;
        }
        if (invulnerableTimer <=0 )
        {
            invulnerable = false;
            myAnimator.SetBool("invulnerable", invulnerable);
            invulnerableTimer = invulnerableCoolDown;
            hippoProfile.SetFear(invulnerable);
        }

    }
}
