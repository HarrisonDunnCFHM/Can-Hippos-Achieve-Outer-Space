using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public enum CurrencyType {Scale, Fire, Rain, Spark};
    [SerializeField] CurrencyType myType;

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
        GetCurrencyType(myType);
        myCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageCurrencyDisplay(myCurrent, myMax, myImages, myFull, myEmpty);
    }

    private void GetCurrencyType(CurrencyType myType)
    {
        switch (myType)
        {
            case CurrencyType.Scale:
                myImages = scaleImages;
                myFull = scaleFull;
                myEmpty = scaleEmpty;
                myMax = scaleMax;
                myCurrent = scaleCurrent;
                break;
            case CurrencyType.Fire:
                myImages = fireImages;
                myFull = fireFull;
                myEmpty = fireEmpty;
                myMax = fireMax;
                myCurrent = fireCurrent;
                break;
            case CurrencyType.Rain:
                myImages = rainImages;
                myFull = rainFull;
                myEmpty = rainEmpty;
                myMax = rainMax;
                myCurrent = rainCurrent;
                break;
            case CurrencyType.Spark:
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

    private void ManageCurrencyDisplay(int current, int max, Image[] image, Sprite full, Sprite empty)
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
}
