using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementingData : MonoBehaviour
{
    [Header("Incrementers")]
    public float rocketSpeed;
    public float healthMax;
    public float fuelMax;
    public float fuelEfficiency;
    public float flyDistanceBest;
    
    [Header("Currencies")]
    public float money; //base resource - funding from the hippogeum
    public float skyRock; //rock resource 1 - improves fuel capacity
    public float skyMetal; //rock resource 2 - improves rocket speed;
    public float scales; //dragon resource 1 - improves hull
    public float breath; //dragon resource 2 - improves fuel eff
    
    //cached references
    public float healthCurrent;
    public float fuelCurrent;
    public float flyDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
