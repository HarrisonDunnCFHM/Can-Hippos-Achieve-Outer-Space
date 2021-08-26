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
    TokenManager tokenManager;
    CoinManager coinManager;
    int currentCoinCost;
    int currentTokenCost;
    int researchLevel;
  

    // Start is called before the first frame update
    void Start()
    {
        tokenManager = FindObjectOfType<TokenManager>();
        coinManager = FindObjectOfType<CoinManager>();
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
        levelText.text = researchLevel.ToString();
        if (currentCoinCost == 0) { coinCostText.text = ""; }
        else
        {
            coinCostText.text = currentCoinCost.ToString() + " coins";
        }
        if (currentTokenCost == 0) { tokenCostText.text = ""; }
        else if (currentTokenCost == 1)
        {
            tokenCostText.text = currentCoinCost.ToString() + " " + tokenCostType.ToString();
        }
        else
        {
            tokenCostText.text = currentCoinCost.ToString() + " " + tokenCostType.ToString() + "s";
        }
        awardText.text = awardDescription;
    }

    public void BuyResearch()
    {
        if (!coinManager.CheckAvailable(currentCoinCost)) { return; }
        if (!tokenManager.CheckAvailable(currentTokenCost)) { return; }
        coinManager.SpendCoins(currentCoinCost);
        currentCoinCost = Mathf.RoundToInt(coinCostGrowth * currentCoinCost);
        tokenManager.SpendTokens(currentTokenCost);
        currentTokenCost += tokenCostGrowth;


    }
}
