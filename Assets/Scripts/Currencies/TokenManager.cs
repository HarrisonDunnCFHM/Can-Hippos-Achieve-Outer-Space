using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    public enum TokenType {Coin, Scale, Fire, Rain, Spark};
    [SerializeField] TokenType myType;

    [Header("Dragon Scales")]
    [SerializeField] Image[] scaleImages;
    [SerializeField] Sprite scaleFull;
    [SerializeField] Sprite scaleEmpty;
    [SerializeField] int scaleMax;
    int scaleCurrent;
    [Header("Dragon Fire")]
    [SerializeField] Image[] fireImages;
    [SerializeField] Sprite fireFull;
    [SerializeField] Sprite fireEmpty;
    [SerializeField] int fireMax;
    int fireCurrent;
    [Header("Brain Rain")]
    [SerializeField] Image[] rainImages;
    [SerializeField] Sprite rainFull;
    [SerializeField] Sprite rainEmpty;
    [SerializeField] int rainMax;
    int rainCurrent;
    [Header("Brain Sparks")]
    [SerializeField] Image[] sparkImages;
    [SerializeField] Sprite sparkFull;
    [SerializeField] Sprite sparkEmpty;
    [SerializeField] int sparkMax;
    int sparkCurrent;

    //cached references
    Image[] myImages;
    Sprite myFull;
    Sprite myEmpty;
    int myMax;
    int myCurrent;

    // Start is called before the first frame update
    void Start()
    {
        GetTokenType(myType);
        myCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageTokenDisplay(myCurrent, myMax, myImages, myFull, myEmpty);
    }

    private void GetTokenType(TokenType myType)
    {
        switch (myType)
        {
            case TokenType.Scale:
                myImages = scaleImages;
                myFull = scaleFull;
                myEmpty = scaleEmpty;
                myMax = scaleMax;
                myCurrent = scaleCurrent;
                break;
            case TokenType.Fire:
                myImages = fireImages;
                myFull = fireFull;
                myEmpty = fireEmpty;
                myMax = fireMax;
                myCurrent = fireCurrent;
                break;
            case TokenType.Rain:
                myImages = rainImages;
                myFull = rainFull;
                myEmpty = rainEmpty;
                myMax = rainMax;
                myCurrent = rainCurrent;
                break;
            case TokenType.Spark:
                myImages = sparkImages;
                myFull = sparkFull;
                myEmpty = sparkEmpty;
                myMax = sparkMax;
                myCurrent = sparkCurrent;
                break;
            default:
                break;

        }
    }

    private void ManageTokenDisplay(int current, int max, Image[] image, Sprite full, Sprite empty)
    {
        if (current > max)
        {
            current = max;
        }
        for (int i = 0; i < image.Length; i++)
        {
            if (i < current)
            {
                image[i].sprite = full;
            }
            else
            {
                image[i].sprite = empty;
            }

            if (i < max)
            {
                image[i].enabled = true;
            }
            else { image[i].enabled = false; }
        }
    }

    public bool AddTokens(int toAdd)
    {
        if (myCurrent + toAdd <= myMax)
        { 
            myCurrent += toAdd;
            return true;
        }
        else
        { return false; }
    }

    public void SpendTokens(int toRemove)
    {
        myCurrent -= toRemove;
    }

    public bool CheckAvailable(int toRemove)
    {
        if(toRemove > myCurrent)
        { return false; }
        else { return true; }
    }

    public TokenType ReturnTokenType()
    {
        return myType;
    }
}
