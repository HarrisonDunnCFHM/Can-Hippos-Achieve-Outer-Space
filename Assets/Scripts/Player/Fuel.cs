using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    //config params
    [SerializeField] int fuelMax;
    [SerializeField] float fuelBurnRate;
    [SerializeField] Slider fuelSlider;
    [SerializeField] Text fuelText;
    [SerializeField] Vector2 fuelGaugeHomePos;
    [SerializeField] PopOutMenu retryMenu;

    //cached refs
    float fuelCurrent;
    RectTransform fuelTransform;
    float fuelGaugeXPos;
    float fuelPercent;
    List<ScrollingBackground> scrollingBackGrounds;
    HippoRocket hippoRocket;
    DistanceTracker distanceTracker;
    bool blastOff;
    IncrementingData gameData;
    bool outOfFuel;

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<IncrementingData>();
        scrollingBackGrounds = new List<ScrollingBackground>(FindObjectsOfType<ScrollingBackground>());
        hippoRocket = FindObjectOfType<HippoRocket>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
        blastOff = false;
        if(gameData.fuelMax != 0) { fuelMax = gameData.fuelMax; }
        if(gameData.fuelEfficiency != 0) { fuelBurnRate = gameData.fuelEfficiency; }
        fuelCurrent = fuelMax;
        fuelSlider.maxValue = fuelMax;
        fuelTransform = fuelSlider.GetComponent<RectTransform>();
        fuelGaugeXPos = fuelSlider.transform.localPosition.x;
        fuelTransform.sizeDelta = new Vector2(fuelMax, fuelTransform.sizeDelta.y);
        var newXPos = fuelGaugeXPos + (50 * ((fuelMax - 100)/100));
        fuelSlider.transform.localPosition = new Vector2(newXPos, fuelSlider.transform.localPosition.y);
        outOfFuel = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (fuelCurrent > fuelMax)
        {
            fuelCurrent = fuelMax;
        }
        fuelPercent = (fuelCurrent / fuelMax) * 100;
        fuelText.text = fuelPercent.ToString("F1") + "%";
        fuelSlider.value = fuelCurrent;
        if (!blastOff) { return; }
        if (!outOfFuel) { fuelCurrent -= fuelBurnRate * Time.deltaTime; }
        if (fuelCurrent <= 0)
        {
            fuelCurrent = 0;
            if (!outOfFuel) { OutOfFuel(); }
        }
    }
    public void UpgradeFuel(int upgradeValue)
    {
        fuelMax += upgradeValue;
        if (!blastOff) { fuelCurrent = fuelMax; }
        fuelSlider.maxValue = fuelMax;
        fuelTransform = fuelSlider.GetComponent<RectTransform>();
        fuelGaugeXPos = fuelSlider.transform.localPosition.x;
        fuelTransform.sizeDelta = new Vector2(fuelMax, fuelTransform.sizeDelta.y);
        var newXPos = fuelGaugeXPos + (50);
        fuelSlider.transform.localPosition = new Vector2(newXPos, fuelTransform.transform.localPosition.y); 
    }

    public void UpgradeFuelEfficiency(int upgradeValue)
    {
        Debug.Log("upgrade value is " + upgradeValue.ToString());
        fuelBurnRate *= (float)upgradeValue/10;
        Debug.Log("new burn rate is " + fuelBurnRate.ToString());
    }
    public void BlastOff()
    {
        blastOff = true;
    }

    public void OutOfFuel()
    {
        outOfFuel = true;
        distanceTracker.StopAscending();
        hippoRocket.StopEngines();
        foreach (ScrollingBackground scrollingBackground in scrollingBackGrounds)
        {
            scrollingBackground.StopAscending();
        }
        retryMenu.ToggleMenu();
    }

    public void CacheFuelStats()
    {
        gameData.fuelMax = fuelMax;
        gameData.fuelEfficiency = fuelBurnRate;
    }
}
