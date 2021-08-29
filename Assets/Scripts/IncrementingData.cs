using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementingData : MonoBehaviour
{

    [Header("Currency Banks")]
    [SerializeField] Text coinsBank; //base resource - funding from the hippogeum
    [SerializeField] Text coinsBankShadow; //base resource - funding from the hippogeum


    //cached references
    public int experimentsRun;
    public int healthMax;
    public int coinBanked;
    public int coinDivider;
    public int coinMultiplier;
    public int scaleBanked;
    public int scaleMax;
    public int fireBanked;
    public int fireMax;
    public int rainBanked;
    public int rainMax;
    public int sparkBanked;
    public int sparkMax;
    public float rocketSpeed;
    public int fuelMax;
    public float fuelEfficiency;
    public float flyDistanceBest;
    public int[] researchLevels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] researchCoinCost = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] researchTokenCost = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] researchAward = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool[] researchUnlocked = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };





    private void Awake()
    {
        int numberOfManagers = FindObjectsOfType<IncrementingData>().Length;
        if (numberOfManagers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            LoadGame();
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadGame()
    {
        experimentsRun = PlayerPrefs.GetInt("experimentsRun", 0);
        healthMax = PlayerPrefs.GetInt("healthMax", 0);
        coinBanked = PlayerPrefs.GetInt("coinBanked", 0);
        coinDivider = PlayerPrefs.GetInt("coinDivider", 0);
        coinMultiplier = PlayerPrefs.GetInt("coinMultiplier", 0);
        scaleBanked = PlayerPrefs.GetInt("scaleBanked", 0);
        scaleMax = PlayerPrefs.GetInt("scaleMax", 0);
        fireBanked = PlayerPrefs.GetInt("fireBanked", 0);
        fireMax = PlayerPrefs.GetInt("fireMax", 0);
        rainBanked = PlayerPrefs.GetInt("rainBanked", 0);
        rainMax = PlayerPrefs.GetInt("rainMax", 0);
        sparkBanked = PlayerPrefs.GetInt("sparkBanked", 0);
        sparkMax = PlayerPrefs.GetInt("sparkMax", 0);
        rocketSpeed = PlayerPrefs.GetFloat("rocketSpeed", 0);
        fuelMax = PlayerPrefs.GetInt("fuelMax", 0);
        fuelEfficiency = PlayerPrefs.GetFloat("fuelEfficiency", 0);
        flyDistanceBest = PlayerPrefs.GetFloat("flyDistanceBest", 0);
        researchLevels[0] = PlayerPrefs.GetInt("levelID0", 0);
        researchLevels[1] = PlayerPrefs.GetInt("levelID1", 0);
        researchLevels[2] = PlayerPrefs.GetInt("levelID2", 0);
        researchLevels[3] = PlayerPrefs.GetInt("levelID3", 0);
        researchLevels[4] = PlayerPrefs.GetInt("levelID4", 0);
        researchLevels[5] = PlayerPrefs.GetInt("levelID5", 0);
        researchLevels[6] = PlayerPrefs.GetInt("levelID6", 0);
        researchLevels[7] = PlayerPrefs.GetInt("levelID7", 0);
        researchLevels[8] = PlayerPrefs.GetInt("levelID8", 0);
        researchLevels[9] = PlayerPrefs.GetInt("levelID9", 0);
        researchLevels[10] = PlayerPrefs.GetInt("levelID10", 0);
        researchLevels[11] = PlayerPrefs.GetInt("levelID11", 0);
        researchLevels[12] = PlayerPrefs.GetInt("levelID12", 0);
        researchLevels[13] = PlayerPrefs.GetInt("levelID13", 0);
        researchLevels[14] = PlayerPrefs.GetInt("levelID14", 0);
        researchLevels[15] = PlayerPrefs.GetInt("levelID15", 0);
        researchLevels[16] = PlayerPrefs.GetInt("levelID16", 0);
        researchLevels[17] = PlayerPrefs.GetInt("levelID17", 0);
        researchLevels[18] = PlayerPrefs.GetInt("levelID18", 0);
        researchLevels[19] = PlayerPrefs.GetInt("levelID19", 0);
        researchCoinCost[0] = PlayerPrefs.GetInt("coinCostID0", 0);
        researchCoinCost[1] = PlayerPrefs.GetInt("coinCostID1", 0);
        researchCoinCost[2] = PlayerPrefs.GetInt("coinCostID2", 0);
        researchCoinCost[3] = PlayerPrefs.GetInt("coinCostID3", 0);
        researchCoinCost[4] = PlayerPrefs.GetInt("coinCostID4", 0);
        researchCoinCost[5] = PlayerPrefs.GetInt("coinCostID5", 0);
        researchCoinCost[6] = PlayerPrefs.GetInt("coinCostID6", 0);
        researchCoinCost[7] = PlayerPrefs.GetInt("coinCostID7", 0);
        researchCoinCost[8] = PlayerPrefs.GetInt("coinCostID8", 0);
        researchCoinCost[9] = PlayerPrefs.GetInt("coinCostID9", 0);
        researchCoinCost[10] = PlayerPrefs.GetInt("coinCostID10", 0);
        researchCoinCost[11] = PlayerPrefs.GetInt("coinCostID11", 0);
        researchCoinCost[12] = PlayerPrefs.GetInt("coinCostID12", 0);
        researchCoinCost[13] = PlayerPrefs.GetInt("coinCostID13", 0);
        researchCoinCost[14] = PlayerPrefs.GetInt("coinCostID14", 0);
        researchCoinCost[15] = PlayerPrefs.GetInt("coinCostID15", 0);
        researchCoinCost[16] = PlayerPrefs.GetInt("coinCostID16", 0);
        researchCoinCost[17] = PlayerPrefs.GetInt("coinCostID17", 0);
        researchCoinCost[18] = PlayerPrefs.GetInt("coinCostID18", 0);
        researchCoinCost[19] = PlayerPrefs.GetInt("coinCostID19", 0);
        researchTokenCost[0] = PlayerPrefs.GetInt("tokenCostID0", 0);
        researchTokenCost[1] = PlayerPrefs.GetInt("tokenCostID1", 0);
        researchTokenCost[2] = PlayerPrefs.GetInt("tokenCostID2", 0);
        researchTokenCost[3] = PlayerPrefs.GetInt("tokenCostID3", 0);
        researchTokenCost[4] = PlayerPrefs.GetInt("tokenCostID4", 0);
        researchTokenCost[5] = PlayerPrefs.GetInt("tokenCostID5", 0);
        researchTokenCost[6] = PlayerPrefs.GetInt("tokenCostID6", 0);
        researchTokenCost[7] = PlayerPrefs.GetInt("tokenCostID7", 0);
        researchTokenCost[8] = PlayerPrefs.GetInt("tokenCostID8", 0);
        researchTokenCost[9] = PlayerPrefs.GetInt("tokenCostID9", 0);
        researchTokenCost[10] = PlayerPrefs.GetInt("tokenCostID10", 0);
        researchTokenCost[11] = PlayerPrefs.GetInt("tokenCostID11", 0);
        researchTokenCost[12] = PlayerPrefs.GetInt("tokenCostID12", 0);
        researchTokenCost[13] = PlayerPrefs.GetInt("tokenCostID13", 0);
        researchTokenCost[14] = PlayerPrefs.GetInt("tokenCostID14", 0);
        researchTokenCost[15] = PlayerPrefs.GetInt("tokenCostID15", 0);
        researchTokenCost[16] = PlayerPrefs.GetInt("tokenCostID16", 0);
        researchTokenCost[17] = PlayerPrefs.GetInt("tokenCostID17", 0);
        researchTokenCost[18] = PlayerPrefs.GetInt("tokenCostID18", 0);
        researchTokenCost[19] = PlayerPrefs.GetInt("tokenCostID19", 0);
        researchUnlocked[1] = PlayerPrefs.GetInt("unlockID1") == 1 ? true : false;
        researchUnlocked[2] = PlayerPrefs.GetInt("unlockID2") == 1 ? true : false;
        researchUnlocked[3] = PlayerPrefs.GetInt("unlockID3") == 1 ? true : false;
        researchUnlocked[4] = PlayerPrefs.GetInt("unlockID4") == 1 ? true : false;
        researchUnlocked[5] = PlayerPrefs.GetInt("unlockID5") == 1 ? true : false;
        researchUnlocked[6] = PlayerPrefs.GetInt("unlockID6") == 1 ? true : false;
        researchUnlocked[7] = PlayerPrefs.GetInt("unlockID7") == 1 ? true : false;
        researchUnlocked[8] = PlayerPrefs.GetInt("unlockID8") == 1 ? true : false;
        researchUnlocked[9] = PlayerPrefs.GetInt("unlockID9") == 1 ? true : false;
        researchUnlocked[10] = PlayerPrefs.GetInt("unlockID10") == 1 ? true : false;
        researchUnlocked[11] = PlayerPrefs.GetInt("unlockID11") == 1 ? true : false;
        researchUnlocked[12] = PlayerPrefs.GetInt("unlockID12") == 1 ? true : false;
        researchUnlocked[13] = PlayerPrefs.GetInt("unlockID13") == 1 ? true : false;
        researchUnlocked[14] = PlayerPrefs.GetInt("unlockID14") == 1 ? true : false;
        researchUnlocked[15] = PlayerPrefs.GetInt("unlockID15") == 1 ? true : false;
        researchUnlocked[16] = PlayerPrefs.GetInt("unlockID16") == 1 ? true : false;
        researchUnlocked[17] = PlayerPrefs.GetInt("unlockID17") == 1 ? true : false;
        researchUnlocked[18] = PlayerPrefs.GetInt("unlockID18") == 1 ? true : false;
        researchUnlocked[19] = PlayerPrefs.GetInt("unlockID19") == 1 ? true : false;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("experimentsRun", experimentsRun);
        PlayerPrefs.SetInt("healthMax", healthMax);
        PlayerPrefs.SetInt("coinBanked", coinBanked);
        PlayerPrefs.SetInt("coinDivider", coinDivider);
        PlayerPrefs.SetInt("coinMultiplier", coinMultiplier);
        PlayerPrefs.SetInt("scaleBanked", scaleBanked);
        PlayerPrefs.SetInt("scaleMax", scaleMax);
        PlayerPrefs.SetInt("fireBanked", fireBanked);
        PlayerPrefs.SetInt("fireMax", fireMax);
        PlayerPrefs.SetInt("rainBanked", rainBanked);
        PlayerPrefs.SetInt("rainMax", rainMax);
        PlayerPrefs.SetInt("sparkBanked", sparkBanked);
        PlayerPrefs.SetInt("sparkMax", sparkMax);
        PlayerPrefs.SetFloat("rocketSpeed", rocketSpeed);
        PlayerPrefs.SetInt("fuelMax", fuelMax);
        PlayerPrefs.SetFloat("fuelEfficiency", fuelEfficiency);
        PlayerPrefs.SetFloat("flyDistanceBest", flyDistanceBest);
        PlayerPrefs.SetInt("levelID0", researchLevels[0]);
        PlayerPrefs.SetInt("levelID1", researchLevels[1]);
        PlayerPrefs.SetInt("levelID2", researchLevels[2]);
        PlayerPrefs.SetInt("levelID3", researchLevels[3]);
        PlayerPrefs.SetInt("levelID4", researchLevels[4]);
        PlayerPrefs.SetInt("levelID5", researchLevels[5]);
        PlayerPrefs.SetInt("levelID6", researchLevels[6]);
        PlayerPrefs.SetInt("levelID7", researchLevels[7]);
        PlayerPrefs.SetInt("levelID8", researchLevels[8]);
        PlayerPrefs.SetInt("levelID9", researchLevels[9]);
        PlayerPrefs.SetInt("levelID10", researchLevels[10]);
        PlayerPrefs.SetInt("levelID11", researchLevels[11]);
        PlayerPrefs.SetInt("levelID12", researchLevels[12]);
        PlayerPrefs.SetInt("levelID13", researchLevels[13]);
        PlayerPrefs.SetInt("levelID14", researchLevels[14]);
        PlayerPrefs.SetInt("levelID15", researchLevels[15]);
        PlayerPrefs.SetInt("levelID16", researchLevels[16]);
        PlayerPrefs.SetInt("levelID17", researchLevels[17]);
        PlayerPrefs.SetInt("levelID18", researchLevels[18]);
        PlayerPrefs.SetInt("levelID19", researchLevels[19]);
        PlayerPrefs.SetInt("coinCostID0", researchCoinCost[0]);
        PlayerPrefs.SetInt("coinCostID1", researchCoinCost[1]);
        PlayerPrefs.SetInt("coinCostID2", researchCoinCost[2]);
        PlayerPrefs.SetInt("coinCostID3", researchCoinCost[3]);
        PlayerPrefs.SetInt("coinCostID4", researchCoinCost[4]);
        PlayerPrefs.SetInt("coinCostID5", researchCoinCost[5]);
        PlayerPrefs.SetInt("coinCostID6", researchCoinCost[6]);
        PlayerPrefs.SetInt("coinCostID7", researchCoinCost[7]);
        PlayerPrefs.SetInt("coinCostID8", researchCoinCost[8]);
        PlayerPrefs.SetInt("coinCostID9", researchCoinCost[9]);
        PlayerPrefs.SetInt("coinCostID10", researchCoinCost[10]);
        PlayerPrefs.SetInt("coinCostID11", researchCoinCost[11]);
        PlayerPrefs.SetInt("coinCostID12", researchCoinCost[12]);
        PlayerPrefs.SetInt("coinCostID13", researchCoinCost[13]);
        PlayerPrefs.SetInt("coinCostID14", researchCoinCost[14]);
        PlayerPrefs.SetInt("coinCostID15", researchCoinCost[15]);
        PlayerPrefs.SetInt("coinCostID16", researchCoinCost[16]);
        PlayerPrefs.SetInt("coinCostID17", researchCoinCost[17]);
        PlayerPrefs.SetInt("coinCostID18", researchCoinCost[18]);
        PlayerPrefs.SetInt("coinCostID19", researchCoinCost[19]);
        PlayerPrefs.SetInt("tokenCostID0", researchTokenCost[0]);
        PlayerPrefs.SetInt("tokenCostID1", researchTokenCost[1]);
        PlayerPrefs.SetInt("tokenCostID2", researchTokenCost[2]);
        PlayerPrefs.SetInt("tokenCostID3", researchTokenCost[3]);
        PlayerPrefs.SetInt("tokenCostID4", researchTokenCost[4]);
        PlayerPrefs.SetInt("tokenCostID5", researchTokenCost[5]);
        PlayerPrefs.SetInt("tokenCostID6", researchTokenCost[6]);
        PlayerPrefs.SetInt("tokenCostID7", researchTokenCost[7]);
        PlayerPrefs.SetInt("tokenCostID8", researchTokenCost[8]);
        PlayerPrefs.SetInt("tokenCostID9", researchTokenCost[9]);
        PlayerPrefs.SetInt("tokenCostID10", researchTokenCost[10]);
        PlayerPrefs.SetInt("tokenCostID11", researchTokenCost[11]);
        PlayerPrefs.SetInt("tokenCostID12", researchTokenCost[12]);
        PlayerPrefs.SetInt("tokenCostID13", researchTokenCost[13]);
        PlayerPrefs.SetInt("tokenCostID14", researchTokenCost[14]);
        PlayerPrefs.SetInt("tokenCostID15", researchTokenCost[15]);
        PlayerPrefs.SetInt("tokenCostID16", researchTokenCost[16]);
        PlayerPrefs.SetInt("tokenCostID17", researchTokenCost[17]);
        PlayerPrefs.SetInt("tokenCostID18", researchTokenCost[18]);
        PlayerPrefs.SetInt("tokenCostID19", researchTokenCost[19]);
        PlayerPrefs.SetInt("unlockID1", researchUnlocked[1] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID2", researchUnlocked[2] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID3", researchUnlocked[3] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID4", researchUnlocked[4] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID5", researchUnlocked[5] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID6", researchUnlocked[6] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID7", researchUnlocked[7] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID8", researchUnlocked[8] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID9", researchUnlocked[9] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID10", researchUnlocked[10] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID11", researchUnlocked[11] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID12", researchUnlocked[12] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID13", researchUnlocked[13] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID14", researchUnlocked[14] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID15", researchUnlocked[15] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID16", researchUnlocked[16] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID17", researchUnlocked[17] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID18", researchUnlocked[18] ? 1 : 0);
        PlayerPrefs.SetInt("unlockID19", researchUnlocked[19] ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
