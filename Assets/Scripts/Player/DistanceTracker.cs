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
    [SerializeField] HippoRocket hippoRocket;
    [SerializeField] Fuel fuel;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] ScrollingBackground[] scrollingBackgrounds;

    [SerializeField] GameObject launchPad;
    [SerializeField] GameObject mountainFront;
    [SerializeField] GameObject mountainBack;
    

    //cached references
    bool tier1complete;
    bool tier2complete;
    float currentDistance;
    bool ascending;
    bool descending;
    List<ScrollingBackground> backgrounds;

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
        TierUpCheck();
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
    }

    public void StopAscending()
    {
        ascending = false;
        descending = true;
    }

}
