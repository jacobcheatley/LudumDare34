using System.Collections;
using UnityEngine;

public class SunSprite : MonoBehaviour
{
    [SerializeField] private int numberOfRepositions = 3;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float radiusAroundPlayer = 2f;
    [SerializeField] private float maxRepositionTime = 2f;
    [SerializeField] private float heightAbovePlayer = 2f;
    [SerializeField] private float fadeOutTime = 1f;
    
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(GoToSemiRandomPosition());
    }

    private IEnumerator GoToSemiRandomPosition()
    {
        for (int i = 0; i < numberOfRepositions; i++)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + heightAbovePlayer);
            Vector2 randomChange = Random.insideUnitCircle.normalized;
            targetPosition += new Vector3(randomChange.x, randomChange.y) * radiusAroundPlayer;
            Vector3 direction = targetPosition - transform.position;
            float elapsedTime = 0;
            while (elapsedTime < maxRepositionTime)
            {
                transform.position += direction * speed * Time.deltaTime;
                yield return null;
            }
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeOutTime)
        {

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
