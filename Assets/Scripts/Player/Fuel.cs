using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    //config params
    [SerializeField] float fuelMax;
    [SerializeField] float fuelBurnRate;
    [SerializeField] Slider fuelSlider;
    [SerializeField] Text fuelText;

    //cached refs
    float fuelCurrent;
    RectTransform fuelTransform;
    float fuelGaugeXPos;
    float fuelPercent;
    List<ScrollingBackground> scrollingBackGrounds;
    HippoRocket hippoRocket;
    DistanceTracker distanceTracker;
    bool blastOff;

    // Start is called before the first frame update
    void Start()
    {
        fuelCurrent = fuelMax;
        fuelSlider.maxValue = fuelMax;
        fuelTransform = fuelSlider.GetComponent<RectTransform>();
        fuelGaugeXPos = fuelSlider.transform.localPosition.x;
        fuelTransform.sizeDelta = new Vector2(fuelMax, fuelTransform.sizeDelta.y);
        var newXPos = fuelGaugeXPos + ((fuelMax-100) * 0.5f);
        fuelSlider.transform.localPosition = new Vector2(newXPos, fuelTransform.transform.localPosition.y);
        scrollingBackGrounds = new List<ScrollingBackground>(FindObjectsOfType<ScrollingBackground>());
        hippoRocket = FindObjectOfType<HippoRocket>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
        blastOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fuelCurrent > fuelMax)
        {
            fuelCurrent = fuelMax;
        }
        fuelPercent = (fuelCurrent / fuelMax) * 100;
        fuelText.text = fuelPercent.ToString("F1") + "%";
        fuelSlider.value = fuelCurrent;
        if (!blastOff) { return; }
        fuelCurrent -= fuelBurnRate * Time.deltaTime;
        if (fuelCurrent <= 0)
        {
            fuelCurrent = 0;
            OutOfFuel();
        }
    }

    public void BlastOff()
    {
        blastOff = true;
    }

    public void OutOfFuel()
    {
        distanceTracker.StopAscending();
        hippoRocket.StopEngines();
        fuelBurnRate = 0;
        foreach (ScrollingBackground scrollingBackground in scrollingBackGrounds)
        {
            scrollingBackground.StopAscending();
        }
    }
}
