using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopOutMenu : MonoBehaviour
{
    //config params
    [SerializeField] GameObject myMenu;
    [SerializeField] Vector2 myHomePos;
    [SerializeField] Vector2 myExtendedPos;
    [SerializeField] float moveSpeed;

    //cached references
    Vector2 myTarget;
    public bool isExtending;
    public bool isRetracting;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = myHomePos;
        ToggleMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(isExtending)
        {
            if ((Vector2)transform.localPosition != myExtendedPos)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + (moveSpeed * Time.deltaTime), transform.localPosition.y);
            }
            if (transform.localPosition.x > myExtendedPos.x)
            {
                transform.localPosition = myExtendedPos;
                isExtending = false;
                Time.timeScale = 0;
            }

        }
        if (isRetracting)
        {
            if ((Vector2)transform.localPosition != myHomePos)
            {
                transform.localPosition = new Vector2(transform.localPosition.x - (moveSpeed * Time.deltaTime), transform.localPosition.y);
            }
            if (transform.localPosition.x < myHomePos.x)
            {
                transform.localPosition = myHomePos;
                isRetracting = false;
            }

        }
    }

    public bool CheckExtended()
    {
        if((Vector2)transform.position == myExtendedPos) { return true; }
        else { return false; }
    }

    public void ToggleMenu()
    {
        Time.timeScale = 1;
        if ((Vector2)transform.localPosition == myHomePos) { isExtending = true; }
        if ((Vector2)transform.localPosition == myExtendedPos) { isRetracting = true; }
    }
}
