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
    public int healthMax;
    public int coinsBanked;
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
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
