using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float lifetime = 6f;
    [SerializeField] private float averageSpeed = 0.1f;
    [SerializeField] private float rangeSpeed = 0.05f;
    [SerializeField] private Sprite[] cloudsSprites;

    private float speed;

    void Start()
    {
        Destroy(gameObject, lifetime);
        speed = averageSpeed + Random.Range(-rangeSpeed, rangeSpeed);
        GetComponent<SpriteRenderer>().sprite = cloudsSprites[Random.Range(0, cloudsSprites.Length)];
    }

    void Update()
    {
        transform.position += new Vector3(speed, 0);
    }
}
