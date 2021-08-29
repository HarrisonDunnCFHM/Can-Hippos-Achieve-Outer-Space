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
    [SerializeField] Text fuelName;
    [SerializeField] Text fuelNameShadow;
    [SerializeField] Vector2 fuelGaugeHomePos;
    [SerializeField] PopOutMenu retryMenu;
    [SerializeField] Color normalText;
    [SerializeField] Color dangerText;

    //cached refs
    float fuelCurrent;
    RectTransform fuelTransform;
    float fuelGaugeXPos;
    public float fuelPercent;
    List<ScrollingBackground> scrollingBackGrounds;
    HippoRocket hippoRocket;
    DistanceTracker distanceTracker;
    bool blastOff;
    IncrementingData gameData;
    bool outOfFuel;
    float fuelTimer = .2f;
    bool flashStatus;
    HippoProfile hippoProfile;

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
        flashStatus = true;
        hippoProfile = FindObjectOfType<HippoProfile>();
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

        if (distanceTracker.tier5complete) { blastOff = false; }
        if (!blastOff) { return; }
        LowFuel();
        if (!outOfFuel) { fuelCurrent -= fuelBurnRate * Time.deltaTime; }
        if (fuelCurrent <= 0)
        {
            fuelCurrent = 0;
            if (!outOfFuel) { OutOfFuel(); }
        }
    }

    private void LowFuel()
    {
        if (fuelPercent > 20) { return; }
        if (fuelTimer > 0f)
        {
            fuelTimer -= Time.deltaTime;
        }
        else if (fuelTimer <= 0f && flashStatus)
        {
            flashStatus = false;
            fuelName.text = "Fuel Low!!!" ;
            fuelNameShadow.text = "Fuel Low!!!";
            fuelTimer = .2f;
        }
        else if (fuelTimer <= 0f && !flashStatus)
        {
            flashStatus = true;
            fuelName.text = "Fuel Low!";
            fuelNameShadow.text = "Fuel Low!";
            fuelTimer =  .2f;
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
        distanceTracker.AwardCoins();
        hippoRocket.StopEngines();
        foreach (ScrollingBackground scrollingBackground in scrollingBackGrounds)
        {
            scrollingBackground.StopAscending();
        }
        hippoProfile.SetFear(true);
        retryMenu.ToggleMenu();
    }

    public void CacheFuelStats()
    {
        gameData.fuelMax = fuelMax;
        gameData.fuelEfficiency = fuelBurnRate;
    }
}
