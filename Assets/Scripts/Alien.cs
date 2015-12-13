using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float lifetime = 7f;
    [SerializeField] private float averageSpeed = 4f;
    [SerializeField] private float rangeSpeed = 0.5f;

    [Header("Audio")]
    public AudioClip[] DeathSounds;

    private float minX, maxX;
    private float speed;

    void Start()
    {
        Destroy(gameObject, lifetime);
        speed = averageSpeed + Random.Range(-rangeSpeed, rangeSpeed);
    }

    public void Setup(float minX, float maxX, float initialDirection)
    {
        this.minX = minX;
        this.maxX = maxX;
        StartMove(initialDirection);
    }
    
    void StartMove(float direction)
    {
        if (direction < 0)
            StartCoroutine(MoveRight());
        else
            StartCoroutine(MoveLeft());
    }

    IEnumerator MoveLeft()
    {
        while (transform.position.x > minX)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0);
            yield return null;
        }
        StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        while (transform.position.x < maxX)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
            yield return null;
        }
        StartCoroutine(MoveLeft());
    }
}
