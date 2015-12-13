using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float lifetime = 12f;
    [SerializeField] private float diveThreshold = 2.5f;
    [SerializeField] private float diveSpeed = 6.5f;
    [SerializeField] private float diveAbove = 2f;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] birdCalls;
    [SerializeField] private AudioClip[] screeches;

    private GameObject plant;
    private float sqrDiveThreshold;
    private bool diving;
    private AudioSource audioSource;

    void Start()
    {
        Destroy(gameObject, lifetime);
        sqrDiveThreshold = diveThreshold * diveThreshold;
        plant = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 difference = plant.transform.position - transform.position;
        if (Vector3.SqrMagnitude(difference) < sqrDiveThreshold && !diving)
        {
            diving = true;
            StartCoroutine(Dive(difference));
            audioSource.PlayOneShot(Utility.ChooseOne(birdCalls));
            audioSource.PlayOneShot(Utility.ChooseOne(screeches));
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
