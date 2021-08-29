using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //config params
    [Header("Enemy Config")]
    [SerializeField] List<GameObject> enemies;
    [SerializeField] float spawnEMinStart;
    [SerializeField] float spawnEMaxStart;
    [SerializeField] float spawnERateIncrease;
    [SerializeField] float spawnEMinFloor;
    [SerializeField] float spawnEMaxFloor;
    [SerializeField] float screenEPadding;

    [Header("Obstacle Config")]
    [SerializeField] List<GameObject> hazards;
    [SerializeField] float spawnOMinStart;
    [SerializeField] float spawnOMaxStart;
    [SerializeField] float spawnORateIncrease;
    [SerializeField] float spawnOMinFloor;
    [SerializeField] float spawnOMaxFloor;
    [SerializeField] float screenOPadding;

    [SerializeField] DistanceTracker distanceTracker;

    //cached references
    float spawnETimer;
    float spawnOTimer;
    float xEMin;
    float xEMax;
    float xOMin;
    float xOMax;
    bool blastOff;
    int spawnEIndex;
    GameObject enemyToSpawn;
    int spawnOIndex;
    GameObject obstacleToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnETimer = spawnEMinStart;
        spawnOTimer = spawnOMinStart;
        SetUpSpawnBoundaries();
        blastOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blastOff) { return; }
        SpawnEnemy();
        SpawnObstacle();
    }
    private void SetUpSpawnBoundaries()
    {
        Camera gameCamera = Camera.main;
        xEMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenEPadding;
        xEMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenEPadding;
        xOMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenOPadding;
        xOMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenOPadding;
    }
    private void SpawnEnemy()
    {
        if (spawnETimer > 0)
        {
            spawnETimer -= Time.deltaTime;
        }
        else
        {
            if(distanceTracker.tier3complete )
            {
                spawnEIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, enemies.Count));
                enemyToSpawn = enemies[spawnEIndex];
            }
            if (distanceTracker.tier2complete)
            {
                spawnEIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, 2));
                enemyToSpawn = enemies[spawnEIndex];
            }
            else if (distanceTracker.tier1complete)
            {
                enemyToSpawn = enemies[0];
            }
            else if (!distanceTracker.tier1complete)
            {
                spawnETimer = UnityEngine.Random.Range(spawnEMinStart, spawnEMaxStart);
                return; 
            }
            var spawnX = UnityEngine.Random.Range(xEMin, xEMax);
            var spawnLocation = new Vector2(spawnX, transform.position.y);
            Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            //if (spawnEMinStart > spawnEMinFloor) { spawnEMinStart *= spawnERateIncrease; }
            //if (spawnEMaxStart > spawnEMaxFloor) { spawnEMaxStart *= spawnERateIncrease; }
            spawnETimer = UnityEngine.Random.Range(spawnEMinStart, spawnEMaxStart);
        }
    }

    private void ForceSpawnDragon()
    {
        var enemyToSpawn = enemies[0];
        var spawnX = UnityEngine.Random.Range(xEMin, xEMax);
        var spawnLocation = new Vector2(spawnX, transform.position.y);
        Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
    }

    private void ForceSpawnBrain()
    {
        var enemyToSpawn = enemies[1];
        var spawnX = UnityEngine.Random.Range(xEMin, xEMax);
        var spawnLocation = new Vector2(spawnX, transform.position.y);
        Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
    }

    private void ForceSpawnRock()
    {
        var randomSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0f, hazards.Count - 1));
        var enemyToSpawn = hazards[randomSpawn];
        var spawnX = UnityEngine.Random.Range(xOMin, xOMax);
        var spawnLocation = new Vector2(spawnX, transform.position.y);
        Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
    }

    private void SpawnObstacle()
    {
        if (spawnOTimer > 0)
        {
            spawnOTimer -= Time.deltaTime;
        }
        else
        {
            if (distanceTracker.tier4complete)
            {
                spawnOIndex = Mathf.RoundToInt(UnityEngine.Random.Range(6, 12));
            }
            else if (distanceTracker.tier3complete)
            {
                spawnOIndex = Mathf.RoundToInt(UnityEngine.Random.Range(4, 10));
            }
            else if (distanceTracker.tier2complete)
            {
                spawnOIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, 8));
            }
            else if (distanceTracker.tier1complete)
            {
                spawnOIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, 6));
            }
            else
            {
                spawnOIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, 6));
            }
            obstacleToSpawn = hazards[spawnOIndex];
            var spawnX = UnityEngine.Random.Range(xOMin, xOMax);
            var spawnLocation = new Vector2(spawnX, transform.position.y);
            Instantiate(obstacleToSpawn, spawnLocation, Quaternion.identity);
            spawnOTimer = UnityEngine.Random.Range(spawnOMinStart, spawnOMaxStart);
        }
    }

    public void IncreaseDifficulty()
    {
        if (spawnEMinStart > spawnEMinFloor) { spawnEMinStart *= spawnERateIncrease; }
        if (spawnEMaxStart > spawnEMaxFloor) { spawnEMaxStart *= spawnERateIncrease; }
        if (spawnOMinStart > spawnOMinFloor) { spawnOMinStart *= spawnORateIncrease; }
        if (spawnOMaxStart > spawnOMaxFloor) { spawnOMaxStart *= spawnORateIncrease; }
    }

    public void StopSpawns()
    {
        blastOff = false;
    }

    public void BlastOff()
    {
        blastOff = true;
    }
}
