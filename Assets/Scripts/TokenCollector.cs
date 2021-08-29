using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{

    [SerializeField] TokenManager tokenManager;
    HippoProfile hippoProfile;



    // Start is called before the first frame update
    void Start()
    {
        hippoProfile = FindObjectOfType<HippoProfile>();
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
                bool collectedScale = tokenManager.AddTokens(TokenManager.TokenType.Scale);
                if (collectedScale)
                {
                    Destroy(collidedObject);
                    StartCoroutine(ShortLaugh());
                    return true;
                }
                else { return false; }
            case "Fire":
                bool collectedFire = tokenManager.AddTokens(TokenManager.TokenType.Fire);
                if (collectedFire)
                {
                    Destroy(collidedObject);
                    StartCoroutine(ShortLaugh());
                    return true;
                }
                else { return false; }
            case "Rain":
                bool collectedRain = tokenManager.AddTokens(TokenManager.TokenType.Rain);
                if (collectedRain)
                {
                    Destroy(collidedObject);
                    StartCoroutine(ShortLaugh());
                    return true;
                }
                else { return false; }
            case "Spark":
                bool collectedSpark = tokenManager.AddTokens(TokenManager.TokenType.Spark);
                if (collectedSpark)
                {
                    Destroy(collidedObject);
                    StartCoroutine(ShortLaugh());
                    return true;
                }
                else { return false; }
            default:
                Debug.Log("no tag");
                return false;
        }
    }

    private IEnumerator ShortLaugh()
    {
        hippoProfile.SetLaugh(true);
        yield return new WaitForSeconds(1f);
        hippoProfile.SetLaugh(false);
    }
}
