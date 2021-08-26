using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Research : MonoBehaviour
{
    //config parameters
    [SerializeField] string researchName;
    [SerializeField] Text researchText;
    [SerializeField] int startLevel;
    [SerializeField] int researchMax;
    [SerializeField] Text levelText;
    [SerializeField] int startCoinCost;
    [SerializeField] Text coinCostText;
    [SerializeField] int startTokenCost;
    [SerializeField] TokenManager.TokenType tokenCostType;
    [SerializeField] Text tokenCostText;
    [SerializeField] float coinCostGrowth;
    [SerializeField] int tokenCostGrowth;
    [SerializeField] float awardBase;
    [SerializeField] float awardIncrease;
    [SerializeField] string awardDescription;
    [SerializeField] Text awardText;

    //cached refs
    HealthManager healthManager;
    TokenManager[] tokenManagers;
    CoinManager coinManager;
    int currentCoinCost;
    int currentTokenCost;
    int researchLevel;
    TokenManager myTokenManager;
  

    // Start is called before the first frame update
    void Start()
    {
        tokenManagers = FindObjectsOfType<TokenManager>();
        GetMyManager(tokenCostType);
        coinManager = FindObjectOfType<CoinManager>();
        healthManager = FindObjectOfType<HealthManager>();
        researchLevel = startLevel;
        currentCoinCost = startCoinCost;
        currentTokenCost = startTokenCost;
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
        healthManager.UpgradeHealth((int)awardAmount);
        awardBase += awardIncrease;
    }

    public float BuyResearch()
    {
        if (researchLevel == researchMax) { return 0; }
        if (!coinManager.CheckAvailable(currentCoinCost)) { return 0; }
        if (!myTokenManager.CheckAvailable(currentTokenCost)) { return 0; }
        coinManager.SpendCoins(currentCoinCost);
        currentCoinCost = Mathf.RoundToInt(coinCostGrowth * currentCoinCost);
        myTokenManager.SpendTokens(currentTokenCost);
        currentTokenCost += tokenCostGrowth;
        researchLevel++;
        return awardBase;
    }

    private void GetMyManager(TokenManager.TokenType costType)
    {
        foreach(TokenManager tokenManager in tokenManagers)
        {
            if (tokenManager.ReturnTokenType() == costType)
            {
                myTokenManager = tokenManager;
            }
        }
    }
}
