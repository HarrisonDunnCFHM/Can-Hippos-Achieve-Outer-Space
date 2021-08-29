using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    //config params
    [SerializeField] Text distanceText;
    [SerializeField] Text textShadow;
    [SerializeField] float rocketSpeed;
    [SerializeField] float endTier1;
    [SerializeField] float endTier2;
    [SerializeField] float endTier3;
    [SerializeField] float endTier4;
    [SerializeField] float endTier5;
    [SerializeField] CoinManager coinManager;

    [Header("Blast Offs")]
    [SerializeField] GameObject blastOffButton;
    [SerializeField] HippoRocket hippoRocket;
    [SerializeField] Fuel fuel;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] ScrollingBackground[] scrollingBackgrounds;
    [SerializeField] PopOutMenu upgradeMenu;
    [SerializeField] PopOutMenu winScreen;

    [SerializeField] GameObject launchPad;
    [SerializeField] GameObject mountainFront;
    [SerializeField] GameObject mountainBack;
    [SerializeField] float bOParallaxDelay = 0.5f;
    [SerializeField] float bOParallaxSpeed = 1f;


    //cached references
    public bool tier1complete;
    public bool tier2complete;
    public bool tier3complete;
    public bool tier4complete;
    public bool tier5complete;
    public float currentDistance;
    bool ascending;
    bool descending;
    List<ScrollingBackground> backgrounds;
    bool launchPadMove;
    bool mountainFrontMove;
    bool mountainBackMove;
    int coinsToAward;
    int coinsAwarded;
    IncrementingData gameData;
    HippoProfile hippoProfile;

    // Start is called before the first frame update
    void Start()
    {
        ascending = false;
        backgrounds = new List<ScrollingBackground>(FindObjectsOfType<ScrollingBackground>());
        tier1complete = false;
        tier2complete = false;
        tier3complete = false;
        tier4complete = false;
        tier5complete = false;
        gameData = FindObjectOfType<IncrementingData>();
        hippoProfile = FindObjectOfType<HippoProfile>();
        if (gameData.rocketSpeed != 0)
        {
            rocketSpeed = gameData.rocketSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceTicker();
        TierUpCheck();
        HandleTerrainParallax();
    }

    public void AwardCoins()
    {
        coinsToAward = Mathf.RoundToInt(currentDistance / coinManager.coinDivider) * coinManager.coinMultiplier;
        coinManager.AwardCoins(coinsToAward);
        coinsAwarded += coinsToAward;
    }

    private void HandleTerrainParallax()
    {
        if(launchPadMove)
        {
            var newY = launchPad.transform.position.y - (Time.deltaTime * bOParallaxSpeed);
            launchPad.transform.position = new Vector2(launchPad.transform.position.x, newY);
        }
        if (mountainFrontMove)
        {
            var newY = mountainFront.transform.position.y - (Time.deltaTime * bOParallaxSpeed);
            mountainFront.transform.position = new Vector2(mountainFront.transform.position.x, newY);
        }
        if (mountainBackMove)
        {
            var newY = mountainBack.transform.position.y - (Time.deltaTime * bOParallaxSpeed);
            mountainBack.transform.position = new Vector2(mountainBack.transform.position.x, newY);
        }
    }

    private void UpdateDistanceTicker()
    {
        if (ascending)
        {
            currentDistance += rocketSpeed * Time.deltaTime;
            distanceText.text = "Altitude: " + currentDistance.ToString("n0") + " ft.";
            textShadow.text = "Altitude: " + currentDistance.ToString("n0") + " ft.";
        }
        else if (descending)
        {
            distanceText.text = "You went " + currentDistance.ToString("n0") + " feet!";
            textShadow.text = "You went " + currentDistance.ToString("n0") + " feet!";
        }
        else
        {
            distanceText.text = "Altitude: " + currentDistance.ToString("n0") + " ft.";
            textShadow.text = "Altitude: " + currentDistance.ToString("n0") + " ft.";
        }
    }

    public void IncreaseRocketSpeed(int speedIncrease)
    {
        rocketSpeed *= speedIncrease;
    }

    private void TierUpCheck()
    {
        if(currentDistance >= endTier1 && !tier1complete)
        {
            obstacleSpawner.IncreaseDifficulty();
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier1complete = true;
        }
        else if (currentDistance >= endTier2 && !tier2complete)
        {
            obstacleSpawner.IncreaseDifficulty();
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier2complete = true;
        }
        else if (currentDistance >= endTier3 && !tier3complete)
        {
            obstacleSpawner.IncreaseDifficulty();
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier3complete = true;
        }
        else if (currentDistance >= endTier4 && !tier4complete)
        {
            obstacleSpawner.IncreaseDifficulty();
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier4complete = true;
        }
        else if (currentDistance >= endTier5 && !tier5complete)
        {
            obstacleSpawner.StopSpawns();
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier5complete = true;
            winScreen.ToggleMenu();
            hippoProfile.SetLaugh(true);
        }
    }

    public void BlastOff()
    {
        ascending = true;
        hippoRocket.BlastOff();
        fuel.BlastOff();
        obstacleSpawner.BlastOff();
        foreach (ScrollingBackground scroller in scrollingBackgrounds)
        {
            scroller.BlastOff();
        }
        StartCoroutine(TerrainParallax());
        if(upgradeMenu.CheckExtended()) { upgradeMenu.ToggleMenu(); }
        blastOffButton.gameObject.SetActive(false);
    }

    private IEnumerator TerrainParallax()
    {
        launchPadMove = true;
        yield return new WaitForSeconds(bOParallaxDelay);
        mountainFrontMove = true;
        yield return new WaitForSeconds(bOParallaxDelay * 2);
        mountainBackMove = true;
        yield return new WaitForSeconds(10f);
        launchPad.SetActive(false);
        mountainBack.SetActive(false);
        mountainFront.SetActive(false);
    }

    public void StopAscending()
    {
        ascending = false;
        descending = true;
    }

    public void CacheRocketSpeed()
    {
        gameData.rocketSpeed = rocketSpeed;
    }
}
