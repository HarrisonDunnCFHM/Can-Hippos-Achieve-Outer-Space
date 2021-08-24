using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainLightning : MonoBehaviour
{
    //config parameters;
    [SerializeField] float rotationSpeed = 1;

    //cached references
    Vector2 directionToRocket;
    HippoRocket hippoRocket;
    
    // Start is called before the first frame update
    void Start()
    {
        hippoRocket = FindObjectOfType<HippoRocket>();
    }

    // Update is called once per frame
    void Update()
    {
        directionToRocket = (Vector2)hippoRocket.transform.position - (Vector2)transform.position ;
        directionToRocket.Normalize();
        float angle = Mathf.Atan2(directionToRocket.x, directionToRocket.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (-angle + 90));
        //Quaternion rotation = Quaternion.AngleAxis(-angle + 90, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }
}
