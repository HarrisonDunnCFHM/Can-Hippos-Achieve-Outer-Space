using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    //config params
    [SerializeField] Text coinText;
    [SerializeField] Text coinShadow;

    //cached ref
    int myCoins;
    
    // Start is called before the first frame update
    void Start()
    {
        myCoins = 0;
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
        GainCoins(1);
    }

    public void GainCoins(int coinsEarned)
    {
        myCoins += coinsEarned;
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
}
