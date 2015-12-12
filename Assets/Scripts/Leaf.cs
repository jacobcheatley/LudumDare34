using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Leaf : MonoBehaviour
{
    [SerializeField] private Sprite[] leafSprites;
    [SerializeField] private float lifeTime = 3f;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = leafSprites[Random.Range(0, leafSprites.Length)];
        Destroy(gameObject, lifeTime);
    }
}
