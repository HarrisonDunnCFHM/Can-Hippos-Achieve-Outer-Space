using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    //config params
    [SerializeField] Text coinText;
    [SerializeField] Text coinShadow;
    public int coinDivider;

    //cached ref
    int myCoins;
    public int coinsToEarn;
    IncrementingData gameData;
    
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<IncrementingData>();
        myCoins = gameData.coinsBanked;
    }

    // Update is called once per frame
    void Update()
    {
        if (myCoins > 999999999)
        {
            coinText.text = "TOO MANY";
            coinShadow.text = "TOO MANY";
        }
        else
        {
            coinText.text = myCoins.ToString();
            coinShadow.text = myCoins.ToString();
        }
        if (coinsToEarn > 5000)
        {
            coinsToEarn -= 100;
            myCoins += 100;
        }
        else if (coinsToEarn > 500)
        {
            coinsToEarn -= 10;
            myCoins += 10;
        }
        else if (coinsToEarn > 0)
        {
            coinsToEarn--;
            myCoins++;
        }
        if (coinsToEarn < 0)
        {
            coinsToEarn = 0;
        }
    }

    public void AwardCoins(int coinsEarned)
    {
        coinsToEarn += coinsEarned;
    }

    public void SpendCoins(int coinsSpent)
    {
        myCoins -= coinsSpent;
    }
    public bool CheckAvailable(int toRemove)
    {
        if (toRemove > myCoins)
        { return false; }
        else { return true; }
    }

 

    public void BankCoins()
    {
        myCoins += coinsToEarn;
        gameData.coinsBanked = myCoins;
    }
}
