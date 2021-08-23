using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    //config

    //cached ref
    HealthManager hippoHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        hippoHealth = FindObjectOfType<HealthManager>();
    }

    /*private void OnParticleCollision(GameObject other)
    {
        Debug.Log("I found " + other.gameObject.name);
        if (other.GetComponent<HippoRocket>())
        {
            hippoHealth.TakeHit();
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
