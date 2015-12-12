using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float lifetime = 12f;
    [SerializeField] private float diveThreshold = 3f;
    [SerializeField] private float diveSpeed = 4.5f;
    [SerializeField] private float diveAbove = 3f;

    private GameObject plant;
    private float sqrDiveThreshold;
    private bool diving;

    void Start()
    {
        Destroy(gameObject, lifetime);
        sqrDiveThreshold = diveThreshold * diveThreshold;
        plant = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 difference = plant.transform.position - transform.position;
        if (Vector3.SqrMagnitude(difference) < sqrDiveThreshold && !diving)
        {
            diving = true;
            StartCoroutine(Dive(difference));
        }
    }

    IEnumerator Dive(Vector3 difference)
    {
        Vector3 direction = new Vector3(difference.x, difference.y + diveAbove).normalized;
        while (true)
        {
            transform.position += direction * diveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
