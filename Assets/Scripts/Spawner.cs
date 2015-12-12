using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float screenWorldWidth;
    private float screenWorldHeight;

    [Header("All")]
    [SerializeField] private float distanceAbove = 1f;
    [SerializeField] private float boundaryWidth = 0.5f;
    private float minSpawnX, maxSpawnX;

    [Header("Flies")]
    [SerializeField] private GameObject fly;
    [SerializeField] private float timeBetweenFlySpawns = 0.5f;
    private float nextFlySpawnTime;

    void Start()
    {
        nextFlySpawnTime = Time.time + timeBetweenFlySpawns;
        screenWorldWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        screenWorldHeight = screenWorldWidth * Screen.height / Screen.width;
        minSpawnX = -screenWorldWidth + boundaryWidth;
        maxSpawnX = -minSpawnX;
    }

    void Update()
    {
        if (Time.time > nextFlySpawnTime)
        {
            nextFlySpawnTime = Time.time + timeBetweenFlySpawns;
            float flySpawnX = transform.position.x + Random.Range(minSpawnX, maxSpawnX);
            float flySpawnY = transform.position.y + screenWorldHeight + distanceAbove;
            Instantiate(fly, new Vector3(flySpawnX, flySpawnY), Quaternion.identity);
        }
    }
}
