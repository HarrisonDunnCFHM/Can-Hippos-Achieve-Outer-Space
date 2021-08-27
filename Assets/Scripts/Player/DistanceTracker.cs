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
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float endTier1;
    [SerializeField] float endTier2;

    [Header("Blast Offs")]
    [SerializeField] GameObject blastOffButton;
    [SerializeField] HippoRocket hippoRocket;
    [SerializeField] Fuel fuel;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] ScrollingBackground[] scrollingBackgrounds;
    [SerializeField] PopOutMenu upgradeMenu;

    [SerializeField] GameObject launchPad;
    [SerializeField] GameObject mountainFront;
    [SerializeField] GameObject mountainBack;
    [SerializeField] float bOParallaxDelay = 0.5f;
    [SerializeField] float bOParallaxSpeed = 1f;
    

    //cached references
    bool tier1complete;
    bool tier2complete;
    float currentDistance;
    bool ascending;
    bool descending;
    List<ScrollingBackground> backgrounds;
    bool launchPadMove;
    bool mountainFrontMove;
    bool mountainBackMove;

    // Start is called before the first frame update
    void Start()
    {
        ascending = false;
        backgrounds = new List<ScrollingBackground>(FindObjectsOfType<ScrollingBackground>());
        tier1complete = false;
        tier2complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceTicker();
        TierUpCheck();
        HandleTerrainParallax();
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
            currentDistance += baseMoveSpeed * Time.deltaTime;
            distanceText.text = "Altitude: " + currentDistance.ToString("F1") + " ft.";
            textShadow.text = "Altitude: " + currentDistance.ToString("F1") + " ft.";
        }
        else if (descending)
        {
            distanceText.text = "You went " + currentDistance.ToString("F1") + " feet!";
            textShadow.text = "You went " + currentDistance.ToString("F1") + " feet!";
        }
        else
        {
            distanceText.text = "Altitude: " + currentDistance.ToString("F1") + " ft.";
            textShadow.text = "Altitude: " + currentDistance.ToString("F1") + " ft.";
        }
    }

    private void TierUpCheck()
    {
        if(currentDistance >= endTier1 && !tier1complete)
        {
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier1complete = true;
        }
        if (currentDistance >= endTier2 && !tier2complete)
        {
            foreach (ScrollingBackground background in backgrounds)
            {
                background.TriggerNextLevel();
            }
            tier2complete = true;
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
        Time.timeScale = 1f;
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

}
