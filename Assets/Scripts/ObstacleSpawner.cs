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

    //cached references
    float spawnETimer;
    float spawnOTimer;
    float xEMin;
    float xEMax;
    float xOMin;
    float xOMax;

    // Start is called before the first frame update
    void Start()
    {
        spawnETimer = spawnEMinStart;
        spawnOTimer = spawnOMinStart;
        SetUpSpawmBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
        SpawnObstacle();
    }
    private void SetUpSpawmBoundaries()
    {
        Camera gameCamera = Camera.main;
        xEMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenEPadding;
        xEMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenEPadding;
        xOMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenEPadding;
        xOMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenEPadding;
    }
    private void SpawnEnemy()
    {
        if(spawnETimer > 0)
        {
            spawnETimer -= Time.deltaTime;
        }
        else
        {
            var randomSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0f, enemies.Count - 1));
            var enemyToSpawn = enemies[randomSpawn];
            var spawnX = UnityEngine.Random.Range(xEMin, xEMax);
            var spawnLocation = new Vector2(spawnX, transform.position.y);
            Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            if (spawnEMinStart > spawnEMinFloor) { spawnEMinStart *= spawnERateIncrease; }
            if (spawnEMaxStart > spawnEMaxFloor) { spawnEMaxStart *= spawnERateIncrease; }
            spawnETimer = UnityEngine.Random.Range(spawnEMinStart, spawnEMaxStart);
        }
    }

    private void SpawnObstacle()
    {
        if (spawnOTimer > 0)
        {
            spawnOTimer -= Time.deltaTime;
        }
        else
        {
            var randomSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0f, hazards.Count - 1));
            var enemyToSpawn = hazards[randomSpawn];
            var spawnX = UnityEngine.Random.Range(xOMin, xOMax);
            var spawnLocation = new Vector2(spawnX, transform.position.y);
            Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            if (spawnOMinStart > spawnOMinFloor) { spawnOMinStart *= spawnORateIncrease; }
            if (spawnOMaxStart > spawnOMaxFloor) { spawnOMaxStart *= spawnORateIncrease; }
            spawnOTimer = UnityEngine.Random.Range(spawnOMinStart, spawnOMaxStart);
        }
    }
}
