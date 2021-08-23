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
    ScrollingBackground scrollingBackGround;
    HippoRocket hippoRocket;
    DistanceTracker distanceTracker;

    // Start is called before the first frame update
    void Start()
    {
        fuelCurrent = fuelMax;
        fuelSlider.maxValue = fuelMax;
        fuelTransform = fuelSlider.GetComponent<RectTransform>();
        fuelGaugeXPos = fuelSlider.transform.localPosition.x;
        Debug.Log("fuel gauge x pos is " + fuelGaugeXPos.ToString());
        fuelTransform.sizeDelta = new Vector2(fuelMax, fuelTransform.sizeDelta.y);
        var newXPos = fuelGaugeXPos + ((fuelMax-100) * 0.5f);
        fuelSlider.transform.localPosition = new Vector2(newXPos, fuelTransform.transform.localPosition.y);
        scrollingBackGround = FindObjectOfType<ScrollingBackground>();
        hippoRocket = FindObjectOfType<HippoRocket>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fuelCurrent > fuelMax)
        {
            fuelCurrent = fuelMax;
        }
        fuelPercent = (fuelCurrent / fuelMax) * 100;
        fuelText.text = fuelPercent.ToString("F2") + "%";
        fuelCurrent -= fuelBurnRate * Time.deltaTime;
        fuelSlider.value = fuelCurrent;
        if (fuelCurrent <= 0)
        {
            fuelCurrent = 0;
            OutOfFuel();
        }
    }

    public void OutOfFuel()
    {
        scrollingBackGround.StopAscending();
        distanceTracker.StopAscending();
        hippoRocket.StopEngines();
        fuelBurnRate = 0;
    }
}
