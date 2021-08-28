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
    public int coinMultiplier = 1;
    [SerializeField] AudioClip[] myPlinks;

    //cached ref
    int myCoins;
    public int coinsToEarn;
    IncrementingData gameData;
    AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<IncrementingData>();
        myCoins = gameData.coinBanked;
        if(gameData.coinDivider != 0) { coinDivider = gameData.coinDivider; }
        if(gameData.coinMultiplier != 0) { coinMultiplier = gameData.coinMultiplier; }
        audioManager = FindObjectOfType<AudioManager>();
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
            StartCoroutine(PlayCoinPlink());
            coinsToEarn -= 100;
            myCoins += 100;
        }
        else if (coinsToEarn > 500)
        {
            StartCoroutine(PlayCoinPlink());
            coinsToEarn -= 10;
            myCoins += 10;
        }
        else if (coinsToEarn > 0)
        {
            StartCoroutine(PlayCoinPlink());
            coinsToEarn--;
            myCoins++;
        }
        if (coinsToEarn < 0)
        {
            coinsToEarn = 0;
        }
    }

    private IEnumerator PlayCoinPlink()
    {
        yield return new WaitForSeconds(0.3f);
        var clipToPlay = UnityEngine.Random.Range(0, myPlinks.Length);
        AudioSource.PlayClipAtPoint(myPlinks[clipToPlay], Camera.main.transform.position, .5f * audioManager.effectVolume * audioManager.masterVolume); ;
    }

    public void AwardCoins(int coinsEarned)
    {
        coinsToEarn += coinsEarned;
    }

    public void UpgradeCoinDivider(int awardedDivider)
    {
        coinDivider -= awardedDivider;
    }
    public void UpgradeCoinMultiplier()
    {
        coinMultiplier *= 2;
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


    public void CacheCoinInfo()
    {
        myCoins += coinsToEarn;
        gameData.coinBanked = myCoins;
        gameData.coinDivider = coinDivider;
        gameData.coinMultiplier = coinMultiplier;
    }
}
