using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("All")]
    [SerializeField] private float distanceAbove = 1f;
    [SerializeField] private float boundaryWidth = 1.0f;
    private float minSpawnX, maxSpawnX;

    [Header("Flies")]
    [SerializeField] private GameObject fly;
    [SerializeField] private float averageDistanceBetweenFlies = 4f;
    [SerializeField] private float rangeDistanceBetweenFlies = 1f;
    private float nextFlySpawnHeight;

    [Header("Birds")]
    [SerializeField] private GameObject bird;
    [SerializeField] private float averageDistanceBetweenBirds = 15f;
    [SerializeField] private float rangeDistanceBetweenBirds = 2f;
    [SerializeField] private float birdXJitter = 0.2f;
    private float nextBirdSpawnHeight;

    [Header("Clouds")]
    [SerializeField] private GameObject cloud;
    [SerializeField] private float averageDistanceBetweenClouds = 3.0f;
    [SerializeField] private float rangeDistanceBetweenClouds = 0.5f;
    private float nextCloudSpawnHeight;

    [Header("Asteroids")]
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float averageDistanceBetweenAsteroids = 15f;
    [SerializeField] private float rangeDistanceBetweenAsteroids = 5f;
    private float nextAsteroidSpawnHeight;

    private float screenWorldWidth;
    private float screenWorldHeight;
    private BackgroundStage bg;

    void Start()
    {
        bg = GetComponent<BackgroundStage>();

        nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlies;
        nextBirdSpawnHeight = transform.position.y + averageDistanceBetweenBirds * 2f;
        screenWorldWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        screenWorldHeight = screenWorldWidth * Screen.height / Screen.width;
        minSpawnX = -screenWorldWidth + boundaryWidth;
        maxSpawnX = -minSpawnX;
    }

    void Update()
    {
        if (bg.stage == BackgroundStage.Stage.Sky)
        {
            if (transform.position.y > nextFlySpawnHeight)
                SpawnFly();
            if (transform.position.y > nextBirdSpawnHeight)
                SpawnBird();
            if (transform.position.y > nextCloudSpawnHeight)
                SpawnCloud();
        }
        else if (bg.stage == BackgroundStage.Stage.Space)
        {
            if (transform.position.y > nextAsteroidSpawnHeight)
                SpawnAsteroid();
        }
    }

    void SpawnFly()
    {
        nextFlySpawnHeight = transform.position.y + AroundValue(averageDistanceBetweenFlies, rangeDistanceBetweenFlies);
        float flySpawnX = transform.position.x + Random.Range(minSpawnX, maxSpawnX);
        float flySpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(fly, new Vector3(flySpawnX, flySpawnY), Quaternion.identity);
    }

    void SpawnBird()
    {
        nextBirdSpawnHeight = transform.position.y + AroundValue(averageDistanceBetweenBirds, rangeDistanceBetweenBirds);
        float birdSpawnX = transform.position.x + Random.value < 0.5 ? minSpawnX : maxSpawnX + AroundValue(0, birdXJitter);
        float birdSpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(bird, new Vector3(birdSpawnX, birdSpawnY), Quaternion.identity);
    }

    void SpawnCloud()
    {
        nextCloudSpawnHeight = transform.position.y + AroundValue(averageDistanceBetweenClouds, rangeDistanceBetweenClouds);
        float cloudSpawnX = transform.position.x + minSpawnX * 4f;
        float cloudSpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(cloud, new Vector3(cloudSpawnX, cloudSpawnY, 1), Quaternion.identity);
        //That 1 up there is important for not blocking stuff!
    }

    void SpawnAsteroid()
    {
        nextAsteroidSpawnHeight = transform.position.y + AroundValue(averageDistanceBetweenAsteroids, rangeDistanceBetweenAsteroids);
        float asteroidSpawnX = transform.position.x + Random.value < 0.5 ? minSpawnX * 2f : maxSpawnX * 2f;
        float asteroidSpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(asteroid, new Vector3(asteroidSpawnX, asteroidSpawnY), Quaternion.identity);
    }

    float AroundValue(float value, float range)
    {
        return value + Random.Range(-range, range);
    }
}
