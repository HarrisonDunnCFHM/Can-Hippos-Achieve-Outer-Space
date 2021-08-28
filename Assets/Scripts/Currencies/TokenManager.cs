using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    public enum TokenType {Coin, Scale, Fire, Rain, Spark};

    [Header("Dragon Scales")]
    [SerializeField] GameObject scaleBankObject;
    [SerializeField] Text scalesBankText; //rock resource 1 - improves fuel capacity
    [SerializeField] Text scalesBankShadow; //rock resource 1 - improves fuel capacity
    [SerializeField] Image[] scaleImages;
    [SerializeField] Sprite scaleFull;
    [SerializeField] Sprite scaleEmpty;
    [SerializeField] int scaleMax;
    int scaleCurrent;
    int scaleBank;
    [Header("Dragon Fire")]
    [SerializeField] GameObject fireBankObject;
    [SerializeField] Text firesBankText; //rock resource 2 - improves rocket speed;
    [SerializeField] Text firesBankShadow; //rock resource 2 - improves rocket speed;
    [SerializeField] Image[] fireImages;
    [SerializeField] Sprite fireFull;
    [SerializeField] Sprite fireEmpty;
    [SerializeField] int fireMax;
    int fireCurrent;
    int fireBank;
    [Header("Brain Rain")]
    [SerializeField] GameObject rainBankObject;
    [SerializeField] Text rainsBankText; //dragon resource 1 - improves hull
    [SerializeField] Text rainsBankShadow; //dragon resource 1 - improves hull
    [SerializeField] Image[] rainImages;
    [SerializeField] Sprite rainFull;
    [SerializeField] Sprite rainEmpty;
    [SerializeField] int rainMax;
    int rainCurrent;
    int rainBank;
    [Header("Brain Sparks")]
    [SerializeField] GameObject sparkBankObject;
    [SerializeField] Text sparksBankText; //dragon resource 2 - improves fuel eff
    [SerializeField] Text sparksBankShadow; //dragon resource 2 - improves fuel eff
    [SerializeField] Image[] sparkImages;
    [SerializeField] Sprite sparkFull;
    [SerializeField] Sprite sparkEmpty;
    [SerializeField] int sparkMax;
    int sparkCurrent;
    int sparkBank;

    //cached references

    IncrementingData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<IncrementingData>();
        scaleBank = gameData.scaleBanked;
        fireBank = gameData.fireBanked;
        rainBank = gameData.rainBanked;
        sparkBank = gameData.sparkBanked;
        scaleMax = gameData.scaleMax;
        fireMax = gameData.fireMax;
        rainMax = gameData.rainMax;
        sparkMax = gameData.sparkMax;
        scaleCurrent = 0;
        fireCurrent = 0;
        rainCurrent = 0;
        sparkCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageTokenDisplay();
        ManageBankDisplay();
    }

    private void ManageBankDisplay()
    {
        if(scaleMax == 0)
        {
            scaleBankObject.SetActive(false);
        }
        else { scaleBankObject.SetActive(true); }
        scalesBankText.text = scaleBank.ToString();
        scalesBankShadow.text = scaleBank.ToString();
        if (fireMax == 0)
        {
            fireBankObject.SetActive(false);
        }
        else { fireBankObject.SetActive(true); }
        firesBankText.text = fireBank.ToString();
        firesBankShadow.text = fireBank.ToString();
        if (rainMax == 0)
        {
            rainBankObject.SetActive(false);
        }
        else { rainBankObject.SetActive(true); }
        rainsBankText.text = rainBank.ToString();
        rainsBankShadow.text = rainBank.ToString();
        if (sparkMax == 0)
        {
            sparkBankObject.SetActive(false);
        }
        else { sparkBankObject.SetActive(true); }
        sparksBankText.text = sparkBank.ToString();
        sparksBankShadow.text = sparkBank.ToString();
    }

    private void ManageTokenDisplay()
    {
        //scales
        if (scaleCurrent > scaleMax)
        {
            scaleCurrent = scaleMax;
        }
        for (int i = 0; i < scaleImages.Length; i++)
        {
            if (i < scaleCurrent)
            {
                scaleImages[i].sprite = scaleFull;
            }
            else
            {
                scaleImages[i].sprite = scaleEmpty;
            }

            if (i < scaleMax)
            {
                scaleImages[i].enabled = true;
            }
            else { scaleImages[i].enabled = false; }
        }
        //fires
        if (fireCurrent > fireMax)
        {
            fireCurrent = fireMax;
        }
        for (int i = 0; i < fireImages.Length; i++)
        {
            if (i < fireCurrent)
            {
                fireImages[i].sprite = fireFull;
            }
            else
            {
                fireImages[i].sprite = fireEmpty;
            }

            if (i < fireMax)
            {
                fireImages[i].enabled = true;
            }
            else { fireImages[i].enabled = false; }
        }
        //rain
        if (rainCurrent > rainMax)
        {
            rainCurrent = rainMax;
        }
        for (int i = 0; i < rainImages.Length; i++)
        {
            if (i < rainCurrent)
            {
                rainImages[i].sprite = rainFull;
            }
            else
            {
                rainImages[i].sprite = rainEmpty;
            }

            if (i < rainMax)
            {
                rainImages[i].enabled = true;
            }
            else { rainImages[i].enabled = false; }
        }
        //spark
        if (sparkCurrent > sparkMax)
        {
            sparkCurrent = sparkMax;
        }
        for (int i = 0; i < sparkImages.Length; i++)
        {
            if (i < sparkCurrent)
            {
                sparkImages[i].sprite = sparkFull;
            }
            else
            {
                sparkImages[i].sprite = sparkEmpty;
            }

            if (i < sparkMax)
            {
                sparkImages[i].enabled = true;
            }
            else { sparkImages[i].enabled = false; }
        }
    }

    public void CacheTokenData()
    {
        scaleBank += scaleCurrent;
        gameData.scaleBanked = scaleBank;
        rainBank += rainCurrent;
        gameData.rainBanked = rainBank;
        fireBank += fireCurrent;
        gameData.fireBanked = fireBank;
        sparkBank += sparkCurrent;
        gameData.sparkBanked = sparkBank;
        gameData.scaleMax = scaleMax;
        gameData.fireMax = fireMax;
        gameData.rainMax = rainMax;
        gameData.sparkMax = sparkMax;
    }

    public bool AddTokens(TokenType tokenType)
    {
        switch (tokenType)
        {
            case TokenType.Scale:
                if (scaleCurrent < scaleMax)
                {
                    scaleCurrent++;
                    return true;
                }
                else { return false; }
            case TokenType.Fire:
                if (fireCurrent < fireMax)
                {
                    fireCurrent++;
                    return true;
                }
                else { return false; }
            case TokenType.Rain:
                if (rainCurrent < rainMax)
                {
                    rainCurrent++;
                    return true;
                }
                else { return false; }
            case TokenType.Spark:
                if (sparkCurrent < sparkMax)
                {
                    sparkCurrent++;
                    return true;
                }
                else { return false; }
            default:
                return false;
        }
    }

    public bool SpendTokens(int toRemove, TokenType tokenType)
    {
       switch (tokenType)
        {
            case TokenType.Scale:
                if (toRemove <= scaleBank)
                {
                    scaleBank -= toRemove;
                    return true;
                }
                else { Debug.Log("insufficient scales."); return false;  }
            case TokenType.Fire:
                if (toRemove <= fireBank)
                {
                    fireBank -= toRemove;
                    return true;
                }
                else { return false; }
            case TokenType.Rain:
                if(toRemove <= rainBank)
                {
                    rainBank -= toRemove;
                    return true;
                }
                else { return false; }
            case TokenType.Spark:
                if(toRemove <= sparkBank)
                {
                    sparkBank -= toRemove;
                    return true;
                }
                else { return false; }
            default:
                return false;
        }
    }

    public void IncreaseScaleMax()
    {
        scaleMax++;
    }

    public void IncreaseFireMax()
    {
        fireMax++;
    }

    public void IncreaseRainMax()
    {
        rainMax++;
    }

    public void IncreaseSparkMax()
    {
        sparkMax++;
    }


}
