using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    ///config params
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    [SerializeField] float sideScrollSpeed = 1f;
    [SerializeField] float opacity;
    [SerializeField] float nextLevelSpeed;


    //cached references
    float backgroundScrollCached;
    Material myMaterial;
    Vector2 yOffSet;
    bool ascending;
    public bool nextLevelTriggered;
    MeshRenderer myMesh;
    float myStartY;
    float myTargetY;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        myMesh = GetComponent<MeshRenderer>();
        yOffSet = new Vector2(0, backgroundScrollSpeed);
        ascending = true;
        myStartY = transform.position.y;
        backgroundScrollCached = backgroundScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (ascending)
        {
            myMaterial.mainTextureOffset += yOffSet * Time.deltaTime;
        }
        else
        {
            myMaterial.mainTextureOffset -= yOffSet * Time.deltaTime;
        }
        
        NextLevel();

    }

    public void TriggerNextLevel()
    {
        nextLevelTriggered = true;
    }

    private void NextLevel()
    {
        if(!nextLevelTriggered) { myTargetY = myStartY - 10; }
        if(nextLevelTriggered)
        {
            backgroundScrollSpeed = 0f;
            if (transform.position.y >= myTargetY)
            {
                var newY = transform.position.y - (Time.deltaTime * nextLevelSpeed);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            if(transform.position.y <= myTargetY)
            {
                transform.position = new Vector3(transform.position.x, myTargetY, transform.position.z);
                nextLevelTriggered = false;
                myStartY = transform.position.y;
                backgroundScrollSpeed = backgroundScrollCached;
            }
        }
    }

    

    public void StopAscending()
    {
        ascending = false;
    }
}
