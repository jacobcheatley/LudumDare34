using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private float lifetime = 7f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
