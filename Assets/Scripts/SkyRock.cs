using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRock : MonoBehaviour
{
    //config params
    [SerializeField] float moveSpeed;
    

    //cached references
    HippoRocket hippoRocket;
    bool fireShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

   
    private void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * moveSpeed));
    }
}
