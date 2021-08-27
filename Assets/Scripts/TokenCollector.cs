using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{

    [SerializeField] TokenManager scaleManager;
    [SerializeField] TokenManager fireManager;
    [SerializeField] TokenManager rainManager;
    [SerializeField] TokenManager sparkManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CollectToken(GameObject collidedObject)
    {
        switch (collidedObject.tag)
        {
            case "Scale":
                bool collectedScale = scaleManager.AddTokens(1);
                if (collectedScale)
                {
                    Destroy(collidedObject);
                    return true;
                }
                else { return false; }
            case "Fire":
                bool collectedFire = fireManager.AddTokens(1);
                if (collectedFire)
                {
                    Destroy(collidedObject);
                    return true;
                }
                else { return false; }
            case "Rain":
                bool collectedRain = rainManager.AddTokens(1);
                if (collectedRain)
                {
                    Destroy(collidedObject);
                    return true;
                }
                else { return false; }
            case "Spark":
                bool collectedSpark = sparkManager.AddTokens(1);
                if (collectedSpark)
                {
                    Destroy(collidedObject);
                    return true;
                }
                else { return false; }
            default:
                Debug.Log("no tag");
                return false;
        }
    }
}
