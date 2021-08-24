using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    //config

    //cached ref
    
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        var ps = GetComponent<ParticleSystem>();
        var coll = ps.collision;
        coll.enabled = false;
    }

    private void OnParticleTrigger()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
