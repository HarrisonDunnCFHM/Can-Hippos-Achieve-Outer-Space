using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    ///config params
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    [SerializeField] float sideScrollSpeed = 1f;
    Material myMaterial;
    Vector2 yOffSet;
    bool ascending;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        yOffSet = new Vector2(0, backgroundScrollSpeed);
        ascending = true;
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
    }

    private void SideScroll()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * sideScrollSpeed;
        var newXPos = transform.position.x + deltaX;
        var xOffSet = new Vector2(newXPos, 0);
        myMaterial.mainTextureOffset += xOffSet * Time.deltaTime;
    }

    public void StopAscending()
    {
        ascending = false;
    }
}
