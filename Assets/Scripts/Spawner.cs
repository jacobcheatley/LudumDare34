using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("All")]
    [SerializeField] private float distanceAbove = 1f;
    [SerializeField] private float boundaryWidth = 1.5f;
    private float minSpawnX, maxSpawnX;

    [Header("Flies")]
    [SerializeField] private GameObject fly;
    [SerializeField] private float averageDistanceBetweenFlySpawns = 4f;
    [SerializeField] private float rangeDistanceBetweenFlies = 1f;
    private float nextFlySpawnHeight;

    [Header("Clouds")]
    [SerializeField] private GameObject cloud;
    [SerializeField] private float averageDistanceBetweenClouds = 3.0f;
    [SerializeField] private float rangeDistanceBetweenClouds = 0.5f;
    private float nextCloudSpawnHeight;

    private float screenWorldWidth;
    private float screenWorldHeight;
    private BackgroundStage bg;

    void Start()
    {
        bg = GetComponent<BackgroundStage>();

        nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlySpawns;
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
            if (transform.position.y > nextCloudSpawnHeight)
                SpawnCloud();
        }
    }

    void SpawnFly()
    {
        nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlySpawns + Random.Range(-rangeDistanceBetweenFlies, rangeDistanceBetweenFlies);
        float flySpawnX = transform.position.x + Random.Range(minSpawnX, maxSpawnX);
        float flySpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(fly, new Vector3(flySpawnX, flySpawnY), Quaternion.identity);
    }

    void SpawnCloud()
    {
        nextCloudSpawnHeight = transform.position.y + averageDistanceBetweenClouds + Random.Range(-rangeDistanceBetweenClouds, rangeDistanceBetweenClouds);
        float cloudSpawnX = transform.position.x + minSpawnX * 2;
        float cloudSpawnY = transform.position.y + screenWorldHeight + distanceAbove;
        Instantiate(cloud, new Vector3(cloudSpawnX, cloudSpawnY, 1), Quaternion.identity);
        //That 1 up there is important !
    }
}
