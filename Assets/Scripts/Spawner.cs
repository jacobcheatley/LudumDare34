using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float screenWorldWidth;
    private float screenWorldHeight;

    [Header("All")]
    [SerializeField] private float distanceAbove = 1f;
    [SerializeField] private float boundaryWidth = 1.5f;
    private float minSpawnX, maxSpawnX;

    [Header("Flies")]
    [SerializeField] private GameObject fly;
    [SerializeField] private float averageDistanceBetweenFlySpawns = 4f;
    [SerializeField] private float rangeDistanceBetweenFlySpawns = 1f;
    private float nextFlySpawnHeight;

    void Start()
    {
        nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlySpawns;
        screenWorldWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        screenWorldHeight = screenWorldWidth * Screen.height / Screen.width;
        minSpawnX = -screenWorldWidth + boundaryWidth;
        maxSpawnX = -minSpawnX;
    }

    void Update()
    {
        if (transform.position.y > nextFlySpawnHeight)
        {
            nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlySpawns + Random.Range(-rangeDistanceBetweenFlySpawns, rangeDistanceBetweenFlySpawns);
            float flySpawnX = transform.position.x + Random.Range(minSpawnX, maxSpawnX);
            float flySpawnY = transform.position.y + screenWorldHeight + distanceAbove;
            Instantiate(fly, new Vector3(flySpawnX, flySpawnY), Quaternion.identity);
        }
    }
}
