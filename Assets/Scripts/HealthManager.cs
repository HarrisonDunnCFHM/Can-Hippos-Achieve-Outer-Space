using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    //config params
    [SerializeField] Image[] healthCapsules;
    [SerializeField] Sprite fullCapsule;
    [SerializeField] Sprite emptyCapsule;
    public int health;
    public int numberOfCapsules;

    //cached references
    HippoRocket hippoRocket;
    Fuel fuel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = GetComponent<HippoRocket>();
        fuel = GetComponent<Fuel>();
    }

    public void TakeHit()
    {
        health--;
        if(health <= 0)
        {
            hippoRocket.StopEngines();
            fuel.OutOfFuel();
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestoreHealth()
    {
        health++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            ResetLevel();
        }
        if (health > numberOfCapsules)
        {
            health = numberOfCapsules;
        }
        for (int i = 0; i < healthCapsules.Length; i++)
        {
            if(i<health)
            {
                healthCapsules[i].sprite = fullCapsule;
            }
            else
            {
                healthCapsules[i].sprite = emptyCapsule;
            }
            
            if(i<numberOfCapsules)
            {
                healthCapsules[i].enabled = true;
            }
            else { healthCapsules[i].enabled = false; }
        }
    }
}
