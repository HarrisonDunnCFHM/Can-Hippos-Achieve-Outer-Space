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
    [SerializeField] bool startingMenu;
    [SerializeField] bool pausesMenu;
    enum MenuDirection { Left, Top, Right, Bottom };
    [SerializeField] MenuDirection menuHome;

    //cached references
    Vector2 myTarget;
    public bool isExtending;
    public bool isRetracting;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = myHomePos;
        if (startingMenu) { ToggleMenu(); }
    }

    // Update is called once per frame
    void Update()
    {
        MenuPopOut(menuHome);
    }

    private void MenuPopOut(MenuDirection myDirection)
    {
        switch (myDirection)
        {
            case MenuDirection.Left:
                if (isExtending)
                {
                    if ((Vector2)transform.localPosition != myExtendedPos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x + (moveSpeed * Time.deltaTime), transform.localPosition.y);
                    }
                    if (transform.localPosition.x > myExtendedPos.x)
                    {
                        transform.localPosition = myExtendedPos;
                        isExtending = false;
                        if (pausesMenu) { Time.timeScale = 0f; }
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
                break;
            case MenuDirection.Right:
                if (isExtending)
                {
                    if ((Vector2)transform.localPosition != myExtendedPos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x - (moveSpeed * Time.deltaTime), transform.localPosition.y);
                    }
                    if (transform.localPosition.x < myExtendedPos.x)
                    {
                        transform.localPosition = myExtendedPos;
                        isExtending = false;
                        if (pausesMenu) { Time.timeScale = 0f; }
                    }

                }
                if (isRetracting)
                {
                    if ((Vector2)transform.localPosition != myHomePos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x + (moveSpeed * Time.deltaTime), transform.localPosition.y);
                    }
                    if (transform.localPosition.x < myHomePos.x)
                    {
                        transform.localPosition = myHomePos;
                        isRetracting = false;
                    }

                }
                break;
            case MenuDirection.Top:
                if (isExtending)
                {
                    if ((Vector2)transform.localPosition != myExtendedPos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - (moveSpeed * Time.deltaTime));
                    }
                    if (transform.localPosition.y < myExtendedPos.y)
                    {
                        transform.localPosition = myExtendedPos;
                        isExtending = false;
                        if (pausesMenu) { Time.timeScale = 0f; }
                    }

                }
                if (isRetracting)
                {
                    if ((Vector2)transform.localPosition != myHomePos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + (moveSpeed * Time.deltaTime));
                    }
                    if (transform.localPosition.y > myHomePos.y)
                    {
                        transform.localPosition = myHomePos;
                        isRetracting = false;
                    }

                }
                break;
            case MenuDirection.Bottom:
                if (isExtending)
                {
                    if ((Vector2)transform.localPosition != myExtendedPos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + (moveSpeed * Time.deltaTime));
                    }
                    if (transform.localPosition.y > myExtendedPos.y)
                    {
                        transform.localPosition = myExtendedPos;
                        isExtending = false;
                        if (pausesMenu) { Time.timeScale = 0f; }
                    }

                }
                if (isRetracting)
                {
                    if ((Vector2)transform.localPosition != myHomePos)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - (moveSpeed * Time.deltaTime));
                    }
                    if (transform.localPosition.y < myHomePos.y)
                    {
                        transform.localPosition = myHomePos;
                        isRetracting = false;
                    }

                }
                break;
            default:
                Debug.Log("no home direction set");
                break;
        }
    }

    public bool CheckExtended()
    {
        if((Vector2)transform.localPosition == myExtendedPos) {
            return true; }
        else { return false; }
    }

    public void ToggleMenu()
    {
        Time.timeScale = 1f;
        if ((Vector2)transform.localPosition == myHomePos) { isExtending = true; }
        if ((Vector2)transform.localPosition == myExtendedPos) { isRetracting = true; }
    }
}
