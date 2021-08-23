using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    //config params
    [SerializeField] Text distanceText;
    [SerializeField] float baseMoveSpeed;

    //cached references
    float currentDistance;
    bool ascending;

    // Start is called before the first frame update
    void Start()
    {
        ascending = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ascending)
        {
            currentDistance += baseMoveSpeed * Time.deltaTime;
            distanceText.text = currentDistance.ToString("F1") + " feet traveled.";
        }
        else
        {
            distanceText.text = "You went " + currentDistance.ToString("F1") + " feet!";
        }
    }

    public void StopAscending()
    {
        ascending = false;
    }

}
