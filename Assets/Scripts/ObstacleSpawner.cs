using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //config params
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> hazards;
    [SerializeField] float spawnMinStart;
    [SerializeField] float spawnMaxStart;
    [SerializeField] float spawnRateIncrease;
    [SerializeField] float spawnMinFloor;
    [SerializeField] float spawnMaxFloor;
    [SerializeField] float screenPadding;

    //cached references
    public float spawnTimer;
    float xMin;
    float xMax;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnMinStart;
        SetUpSpawmBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObstacle();
    }
    private void SetUpSpawmBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenPadding;
    }
    private void SpawnObstacle()
    {
        if(spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            var randomSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0f, enemies.Count - 1));
            var enemyToSpawn = enemies[randomSpawn];
            var spawnX = UnityEngine.Random.Range(xMin, xMax);
            var spawnLocation = new Vector2(spawnX, transform.position.y);
            Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
            if (spawnMinStart > spawnMinFloor) { spawnMinStart *= spawnRateIncrease; }
            if (spawnMaxStart > spawnMaxFloor) { spawnMaxStart *= spawnRateIncrease; }
            spawnTimer = UnityEngine.Random.Range(spawnMinStart, spawnMaxStart);
        }
    }
}
