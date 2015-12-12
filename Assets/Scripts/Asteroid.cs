using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float speed = 7.5f;
    [SerializeField] private float minSpreadAngle = 10f;
    [SerializeField] private float maxSpreadAngle = 25f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Sprite[] asteroidSprites;

    float direction;
    private Vector3 velocity;

    void Start()
    {
        Destroy(gameObject, lifetime);
        GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        direction = transform.position.x < 0 ? 1 : -1;
        transform.rotation = transform.position.x > 0 ?
            Quaternion.AngleAxis(Random.Range(minSpreadAngle, maxSpreadAngle), Vector3.forward) :
            Quaternion.AngleAxis(Random.Range(-maxSpreadAngle, -minSpreadAngle), Vector3.forward);
        velocity = transform.rotation * Vector3.right * speed * direction;
        rotationSpeed *= Random.value < 0.5 ? -1 : 1;
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
