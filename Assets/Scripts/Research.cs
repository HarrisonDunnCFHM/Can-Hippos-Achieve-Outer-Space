using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Research : MonoBehaviour
{
    //config parameters
    [SerializeField] string researchName;
    [SerializeField] int researchIndex;
    [SerializeField] Text researchText;
    [SerializeField] int startLevel;
    [SerializeField] int researchMax;
    [SerializeField] Text levelText;
    [SerializeField] int startCoinCost;
    [SerializeField] Text coinCostText;
    [SerializeField] int startTokenCost;
    [SerializeField] int tokenCostMax = 3;
    [SerializeField] TokenManager.TokenType tokenCostType;
    [SerializeField] Text tokenCostText;
    [SerializeField] float coinCostGrowth;
    [SerializeField] int tokenCostGrowth;
    [SerializeField] int awardBase;
    [SerializeField] int awardIncrease;
    [SerializeField] string awardDescription;
    [SerializeField] Text awardText;
    [SerializeField] bool unlocked;
    [SerializeField] Research[] unlocks;

    //cached refs
    DistanceTracker distanceTracker;
    HealthManager healthManager;
    [SerializeField] TokenManager tokenManager;
    CoinManager coinManager;
    int currentCoinCost;
    int currentTokenCost;
    int researchLevel;
    TokenManager myTokenManager;
    IncrementingData gameData;
    Fuel fuelManager;

    // Start is called before the first frame update
    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        healthManager = FindObjectOfType<HealthManager>();
        fuelManager = FindObjectOfType<Fuel>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
        tokenManager = FindObjectOfType<TokenManager>();
        //researchLevel = startLevel;
        currentCoinCost = startCoinCost;
        currentTokenCost = startTokenCost;
        gameData = FindObjectOfType<IncrementingData>();
        researchLevel = gameData.researchLevels[researchIndex];
        if (gameData.researchCoinCost[researchIndex] == 0) { currentCoinCost = startCoinCost; }
        else { currentCoinCost = gameData.researchCoinCost[researchIndex]; }
        if (gameData.researchTokenCost[researchIndex] == 0) { currentTokenCost = startTokenCost; }
        else { currentTokenCost = gameData.researchTokenCost[researchIndex]; }
        if (gameData.researchAward[researchIndex] != 0) { awardBase = gameData.researchAward[researchIndex]; }
        unlocked = gameData.researchUnlocked[researchIndex];
        if (!unlocked)
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        DisplayResearchInfo();
    }

    private void DisplayResearchInfo()
    {
        
        researchText.text = researchName;
        if (researchLevel == researchMax)
        {
            levelText.text = "lvl MAX";
        }
        else
        {
            levelText.text = "lvl " + researchLevel.ToString();
        }
        if (currentCoinCost == 0 || researchLevel == researchMax) { coinCostText.text = ""; }
        else
        {
            coinCostText.text = currentCoinCost.ToString() + " coins";
        }
        if (currentTokenCost == 0 || researchLevel == researchMax) { tokenCostText.text = ""; }
        else if (currentTokenCost == 1)
        {
            tokenCostText.text = currentTokenCost.ToString() + " " + tokenCostType.ToString();
        }
        else
        {
            tokenCostText.text = currentTokenCost.ToString() + " " + tokenCostType.ToString() + "s";
        }
        awardText.text = awardDescription;
    }

    public void BuyHealthUpgrade()
    {
        var awardAmount = BuyResearch();
        if ( awardAmount == 0) { return; }
        healthManager.UpgradeHealth(awardAmount);
        awardBase += awardIncrease;
    }

    public void BuyFuelUpgrade()
    {
        var awardAmount = BuyResearch();
        if(awardAmount == 0) { return; }
        fuelManager.UpgradeFuel(awardAmount);
        awardBase += awardIncrease;
    }

    public void BuyFuelEffUpgrade()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        fuelManager.UpgradeFuelEfficiency(awardAmount);
        awardBase += awardIncrease;
    }

    public void BuyRocketSpeed()
    {
        var awardAmount = BuyResearch();
        if(awardAmount == 0) { return; }
        distanceTracker.IncreaseRocketSpeed(awardAmount);
        awardBase += awardIncrease;
    }

    public void BuyScaleMax()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        tokenManager.IncreaseScaleMax();
    }

    public void BuyFireMax()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        tokenManager.IncreaseFireMax();
    }

    public void BuyRainMax()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        tokenManager.IncreaseRainMax();
    }

    public void BuySparkMax()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        tokenManager.IncreaseSparkMax();
    }

    public void BuyCoinDividerUpgrade()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        coinManager.UpgradeCoinDivider(awardAmount);
    }

    public void BuyCoinMultiplier()
    {
        var awardAmount = BuyResearch();
        if (awardAmount == 0) { return; }
        coinManager.UpgradeCoinMultiplier();
    }

    public int BuyResearch()
    {
        if (researchLevel == researchMax) { return 0; }
        if (!coinManager.CheckAvailable(currentCoinCost)) { return 0; }
        var enoughTokens = tokenManager.SpendTokens(currentTokenCost, tokenCostType);
        if (!enoughTokens) { return 0; }
        coinManager.SpendCoins(currentCoinCost);
        currentCoinCost = Mathf.RoundToInt(coinCostGrowth * currentCoinCost);
        if (currentTokenCost < tokenCostMax)
        {
            currentTokenCost += tokenCostGrowth;
        }
        researchLevel++;
        UnlockResearch(unlocks);
        return awardBase;
    }

    public void UnlockResearch(Research[] unlockedResearches)
    {
        for (int i = 0; i < unlockedResearches.Length; i++)
        {
            unlockedResearches[i].unlocked = true;
            unlockedResearches[i].gameObject.SetActive(true);
        }
    }

    public void CacheResearchStats()
    {
        gameData.researchLevels[researchIndex] = researchLevel;
        gameData.researchCoinCost[researchIndex] = currentCoinCost;
        gameData.researchTokenCost[researchIndex] = currentTokenCost;
        gameData.researchAward[researchIndex] = awardBase;
        gameData.researchUnlocked[researchIndex] = unlocked;
    }
}
